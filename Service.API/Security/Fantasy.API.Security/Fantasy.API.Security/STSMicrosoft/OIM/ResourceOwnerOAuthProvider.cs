using System.Collections.Generic;
using System.Threading.Tasks;
using Fantasy.API.Security.STSMicrosoft.OIM.Flows;
using Fantasy.API.Utilities.ServicesHandler;
using Microsoft.Owin.Security.OAuth;

namespace Fantasy.API.Security.STSMicrosoft.OIM
{
    public class ResourceOwnerOAuthProvider : OAuthAuthorizationServerProvider
    {
        private static string _requestClientId;
        private static string _requestClientSecret;
        private static bool _isChecked;

        public ResourceOwnerOAuthProvider()
        {            
        }

        /// <summary>
        /// Validate Application's credentials
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // OAuth2 supports the notion of client authentication
            // this is not used here
            _requestClientId = context.Parameters.Get("client_id");
            _requestClientSecret = context.Parameters.Get("client_secret");
            if (_requestClientId != null && _requestClientSecret != null)
            {
                //TODO validate clientID and secrect with table of valid values example: var check = await SomeClass.ValidateClient(_requestClientId,_requestClientSecret);
                var check = _requestClientId == _requestClientSecret;//temp remove after create the table client validation
                if (check)
                {
                    _isChecked = true;
                    context.Validated();
                }
                else
                {
                    _isChecked = false;
                    context.Rejected();
                }
            }
            else
            {
                context.Rejected();
            }

            await Task.FromResult<object>(null);
        }
        /// <summary>
        /// Validate User's credentials
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (!_isChecked)
            {
                context.SetError("ERR_INVALID_LOGIN", "Invalid user or password");
                return;
            }
            IAuthenticationFlow authenticationFlow = null;
            switch (_requestClientId.ToUpper())
            {
                case Applications.Acc:
                    authenticationFlow = new AccAuthenticationFlow(context);
                    break;
                case Applications.Ice:
                    authenticationFlow = new IceAuthenticationFlow(context);
                    break;
                case Applications.Chase:
                    authenticationFlow = new ChaseAuthenticationFlow(context);
                    break;
                case Applications.FloodSolutions:
                    authenticationFlow = new FloodSolutionsAuthenticationFlow(context);
                    break;
                case Applications.ClientLetter:
                    authenticationFlow = new ClientLetterAuthenticationFlow(context);
                    break;
                case Applications.EFTBatch:
                    authenticationFlow = new EftAuthenticationFlow(context);
                    break;
                case Applications.EFTClient:
                    authenticationFlow = new EftClientAuthenticationFlow(context);
                    break;
                default:
                    context.Rejected();
                    break;
            }
            if (authenticationFlow != null)
            {
                await authenticationFlow.ExecuteWorkflowAsync();
            }
        }

        public override async Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            await Task.FromResult<object>(null);
        }

        public override Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
            return base.AuthorizeEndpoint(context);
        }

        public override Task AuthorizationEndpointResponse(OAuthAuthorizationEndpointResponseContext context)
        {
            return base.AuthorizationEndpointResponse(context);
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            return base.TokenEndpointResponse(context);
        }

        public override Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            return base.ValidateAuthorizeRequest(context);
        }

        public override Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context)
        {
            return base.GrantAuthorizationCode(context);
        }

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            return base.GrantClientCredentials(context);
        }

        public override Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
            return base.GrantCustomExtension(context);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            return base.GrantRefreshToken(context);
        }

        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            return base.MatchEndpoint(context);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            return base.ValidateClientRedirectUri(context);
        }

        public override Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            return base.ValidateTokenRequest(context);
        }
    }
}
