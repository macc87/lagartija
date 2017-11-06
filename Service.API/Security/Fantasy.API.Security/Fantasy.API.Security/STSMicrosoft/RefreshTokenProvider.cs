using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Threading.Tasks;
using Fantasy.API.Utilities.Extensions;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace Fantasy.API.Security.STSMicrosoft
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        private static readonly ConcurrentDictionary<string, AuthenticationTicket> RefreshTokens = new ConcurrentDictionary<string, AuthenticationTicket>();

        #region creates refresh token

        public void Create(AuthenticationTokenCreateContext context)
        {

        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var guid = Guid.NewGuid().ToString();

            var refreshTokenExpiration = ConfigurationManager.AppSettings.Get("RefreshTokenExpiration");
            if (String.IsNullOrEmpty(refreshTokenExpiration)) { throw new Exception("RefreshTokenExpiration key can't be found"); }
            var refreshTokenProperties = new AuthenticationProperties(context.Ticket.Properties.Dictionary)
            {
                IssuedUtc = context.Ticket.Properties.IssuedUtc,
                ExpiresUtc = refreshTokenExpiration.GetDateTimeSpan()
            };
            var refreshTokenTicket = new AuthenticationTicket(context.Ticket.Identity, refreshTokenProperties);
            RefreshTokens.TryAdd(guid, refreshTokenTicket);
            context.SetToken(guid);
            await Task.FromResult<object>(null);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
        }

        #endregion

        #region recreates refresh token
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            AuthenticationTicket ticket;
            if (RefreshTokens.TryRemove(context.Token, out ticket))
            {
                context.SetTicket(ticket);
            }
            await Task.FromResult<object>(null);
        }
        #endregion

    }
}
