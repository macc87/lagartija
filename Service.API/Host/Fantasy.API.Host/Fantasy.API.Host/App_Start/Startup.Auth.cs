using Assurant.ASP.Api.Security.STSMicrosoft;
using Assurant.ASP.Api.Security.STSMicrosoft.OIM;
using ClaimManagementCenter.Filter;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Concurrent;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(ClaimManagementCenter.Startup))]
namespace ClaimManagementCenter
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            //SetupDependencyResolver();

            app.UseExternalSignInCookie( DefaultAuthenticationTypes.ExternalCookie );

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
               // Provider = new ResourceOwnerOAuthProvider(ldapUtilityextended, ldapUtility, unitOfWorkSql),
                Provider = new ResourceOwnerOAuthProvider(),
                RefreshTokenProvider = new RefreshTokenProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
                AllowInsecureHttp = true,
                // Authorization code provider which creates and receives the authorization code.
                AuthorizationCodeProvider = new AuthenticationTokenProvider
                {
                    OnCreate = CreateAuthenticationCode,
                    OnReceive = ReceiveAuthenticationCode,
                },

                //// Refresh token provider which creates and receives refresh token.
                //RefreshTokenProvider = new AuthenticationTokenProvider
                //{
                //    OnCreate = CreateRefreshToken,
                //    OnReceive = ReceiveRefreshToken,
                //}
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/Login"),
                CookieName = "ACCAPI0594332211230",
                ExpireTimeSpan = TimeSpan.FromHours(24),
                Provider = new CookieAuthenticationProvider { OnValidateIdentity = MyCustomValidateIdentity }
            });

            app.UseStageMarker(PipelineStage.Authorize);
            app.Use(typeof(OwinMiddlewareTracking));
        }

        private static Task MyCustomValidateIdentity(CookieValidateIdentityContext context)
        {
            var error = context.OwinContext.Request.Cookies["_error"];
            var user = context.OwinContext.Request.Cookies["ACCAPI0594332211230"];

            var identity = context.Identity;
            if (!identity.HasClaim(c => c.Type == ClaimTypes.Expiration)) return Task.FromResult(0);

            var existingClaim = identity.FindFirst(ClaimTypes.Expiration);
            var isPersistent = bool.Parse(identity.FindFirst(ClaimTypes.IsPersistent).Value.ToString());

            if (string.IsNullOrEmpty(existingClaim.Value)) return Task.FromResult(0);

            DateTime claimExpiration;
            DateTime.TryParse(existingClaim.Value, out claimExpiration);
            var currentDate = DateTime.Now;
            var isValid = claimExpiration > currentDate;

            if (!isPersistent && !isValid)
            {
                context.RejectIdentity();
            }

            if (!isValid || error == "Unauthorized" || string.IsNullOrEmpty(user))
            {
                context.RejectIdentity();
            }

            return Task.FromResult(0);
        }

        private readonly ConcurrentDictionary<string, string> _authenticationCodes =
            new ConcurrentDictionary<string, string>( StringComparer.Ordinal );

        private void CreateAuthenticationCode( AuthenticationTokenCreateContext context )
        {
            context.SetToken( Guid.NewGuid().ToString( "n" ) + Guid.NewGuid().ToString( "n" ) );
            _authenticationCodes[context.Token] = context.SerializeTicket();
        }

        private void ReceiveAuthenticationCode( AuthenticationTokenReceiveContext context )
        {
            string value;
            if ( _authenticationCodes.TryRemove( context.Token, out value ) )
            {
                context.DeserializeTicket( value );
            }
        }

        private void CreateRefreshToken( AuthenticationTokenCreateContext context )
        {
            context.SetToken( context.SerializeTicket() );
        }

        private void ReceiveRefreshToken( AuthenticationTokenReceiveContext context )
        {
            context.DeserializeTicket( context.Token );
        }

     
    }
}
