using System;
using System.Text;

namespace Utilities.HttpClient
{

    public class ApiClientConfigurationExpression
    {

        private readonly ApiClientContext _apiClientContext;
        private readonly ApiMultipleClientContext _apiMultipleClientContext;
        internal ApiClientConfigurationExpression(ApiClientContext apiClientContext)
        {

            if (apiClientContext == null)
            {

                throw new ArgumentNullException("apiClientContext");
            }

            _apiClientContext = apiClientContext;
        }
        internal ApiClientConfigurationExpression(ApiMultipleClientContext apiClientContext)
        {

            if (apiClientContext == null)
            {

                throw new ArgumentNullException("apiClientContext");
            }

            _apiMultipleClientContext = apiClientContext;
        }
        public ApiClientConfigurationExpression ConnectTo(string baseUri)
        {

            if (string.IsNullOrEmpty(baseUri))
            {

                throw new ArgumentNullException("baseUri");
            }
            if (_apiClientContext != null)
            {
                _apiClientContext.BaseUri = new Uri(baseUri);
            }
            if (_apiMultipleClientContext != null)
            {
                _apiMultipleClientContext.BaseUri = new Uri(baseUri);
            }

            return this;
        }

        private static string EncodeToBase64(string value)
        {

            byte[] toEncodeAsBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(toEncodeAsBytes);
        }
    }
}