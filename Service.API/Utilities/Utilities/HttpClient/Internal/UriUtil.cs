﻿using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fantasy.API.Utilities.HttpClient.Internal
{

    internal static class UriUtil
    {

        private static readonly Regex _uriParamRegex = new Regex(@"(?<=\{)(.*?)(?=\})", RegexOptions.Compiled);

        internal static string BuildRequestUri(string baseUri, string uriTemplate, object uriParameters = null)
        {

            var trimedUriTemplate = uriTemplate.TrimEnd('/').TrimStart('/');//.ToLowerInvariant();
            QueryStringCollection queryStringCollection = null;
            if (uriParameters != null)
            {
                queryStringCollection = new QueryStringCollection(uriParameters);
            }

            string resolvedUriTemplate = ResolveUriTemplate(trimedUriTemplate, queryStringCollection);
            string appendedUriTemplate = string.Format("{0}/{1}", baseUri, resolvedUriTemplate);

            return appendedUriTemplate;
        }

        internal static string ResolveUriTemplate(string uriTemplate, QueryStringCollection queryStringCollection)
        {

            var createdUriPath = uriTemplate;
            MatchCollection matchedParamNames = _uriParamRegex.Matches(uriTemplate);
            int matchedParamNameAmount = matchedParamNames.Count;
            if (matchedParamNameAmount != 0)
            {

                // TODO: Regex match operration + this operation's result can be cached.
                // Build a key from the base address and the uriTemplate and look that up.

                // Get the match values under an array.
                string[] matchedParameterNameArray = new string[matchedParamNameAmount];
                for (int i = 0; i < matchedParamNameAmount; i++)
                {

                    matchedParameterNameArray[i] = matchedParamNames[i].Value;
                }

                // Check here that enough number of parameters are available.
                // We assume here that we have the queryStringCollection.Count and id parameter if queryStringCollection is not null
                if (((queryStringCollection != null) ? queryStringCollection.Count : 0) < matchedParamNameAmount)
                {

                    throw new InvalidOperationException("Value for requested parameter named  is not available.");
                }

                foreach (var paramName in matchedParameterNameArray)
                {

                    string paramValueFromQueryStringCollection = null;
                    if (queryStringCollection == null)
                    {

                        throw new InvalidOperationException(
                               string.Format("Passed paramater value amount is lower than the required amount by the uriTemplate. {0}", paramName.Count()));
                    }
                    else
                    {

                        var paramFromQueryStringCollection = queryStringCollection.FirstOrDefault(x => x.Key.Equals(paramName, StringComparison.InvariantCultureIgnoreCase));
                        paramValueFromQueryStringCollection = paramFromQueryStringCollection.Value;
                        if (paramValueFromQueryStringCollection == null)
                        {

                            throw new InvalidOperationException(
                                string.Format("Passed paramater value amount is lower than the required amount by the uriTemplate. {0}", paramName.Count()));
                        }

                        // Remove the selected one because it will be 
                        // used for querystring composition.
                        queryStringCollection.Remove(paramFromQueryStringCollection);
                    }

                    string paramValue = paramValueFromQueryStringCollection;
                    createdUriPath = createdUriPath.Replace("{" + paramName + "}", paramValue);
                }
            }

            return ((queryStringCollection != null) && queryStringCollection.Count > 0)
                ? string.Format("{0}?{1}", createdUriPath, queryStringCollection.ToString()
                //.ToLowerInvariant()
                ) : createdUriPath;
        }
    }
}