using Fantasy.API.DataAccess.DbContexts.MSSQL.FantasyData;
using Fantasy.API.DataAccess.Models.MSSQL.Fantasy;
using Fantasy.API.DataAccess.Repository;
using Fantasy.API.DataAccess.UnitOfWork;
using Fantasy.API.Utilities.ServicesHandler;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Security.STSMicrosoft.OIM.Flows
{
    public class FantasyUserAuthenticationFlow : IAuthenticationFlow
    {
        private readonly OAuthGrantResourceOwnerCredentialsContext _context;
        private readonly IRepository<Account> AccountRepository;
        public FantasyUserAuthenticationFlow(OAuthGrantResourceOwnerCredentialsContext context)
        {
            _context = context;
            var unitOfWorkSql = new EfUnitOfWork(new FantasyContext());
            AccountRepository = new Repository<Account>(unitOfWorkSql);
        }

        public async Task ExecuteWorkflowAsync()
        {
            try
            {
                var accountResult = await AccountRepository.FindAsync(x => x.Login == _context.UserName && x.Password == _context.Password);

                if (accountResult != null && accountResult.Count() > 0)
                {
                    var currentUser = accountResult.FirstOrDefault();
                    var oAuthIdentity = new ClaimsIdentity(new GenericIdentity(_context.UserName, OAuthDefaults.AuthenticationType));
                    var cookiesIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationType);

                    oAuthIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, _context.UserName));
                    oAuthIdentity.AddClaim(new Claim(ClaimTypes.Role, ApplicationRoles.ItAdmin));
                    oAuthIdentity.AddClaim(new Claim(ClaimTypes.Email, "cws_it_support@assurant.com"));
                    oAuthIdentity.AddClaim(new Claim("ApplicationName", Applications.Fantasy));
                    oAuthIdentity.AddClaim(new Claim("CompanyName", "Fantasy"));
                    oAuthIdentity.AddClaim(new Claim("CompanyId", Applications.Fantasy));

                    Dictionary<string, string> userProperties = new Dictionary<string, string>()
                {
                    {"UserId", currentUser.Login},
                    {"Email",  currentUser.Email},
                    {"AppAttr", Applications.Fantasy},
                    {"Udf3", Applications.Fantasy},
                    {"Udf4", Applications.Fantasy},
                    {"roles", ApplicationRoles.ItAdmin}
                };

                    var properties = await CreateProperties(_context, userProperties);
                    var ticket = new AuthenticationTicket(oAuthIdentity, properties);
                    _context.Validated(ticket);
                    _context.Request.Context.Authentication.SignIn(cookiesIdentity);
                }
                else
                {
                    _context.SetError("ERR_INVALID_LOGIN", "Invalid user");
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }            
        }

        private async Task<AuthenticationProperties> CreateProperties(OAuthGrantResourceOwnerCredentialsContext context, 
            Dictionary<string, string> userProperties)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {"userId", context.UserName},
                {"email", userProperties["Email"]},
                {"application",userProperties["AppAttr"] },
                {"companyName",userProperties["Udf3"] ??""},
                {"companyId",userProperties["Udf4"] ??""},
                {"role", userProperties["roles"]}
            };
            return await Task.FromResult(new AuthenticationProperties(data));
        }
    }
}
