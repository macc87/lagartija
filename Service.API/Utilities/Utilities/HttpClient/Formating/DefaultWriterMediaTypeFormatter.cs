using System;
using System.Net.Http.Formatting;

namespace Fantasy.API.Utilities.HttpClient.Formating
{

    internal sealed class DefaultWriterMediaTypeFormatter : JsonMediaTypeFormatter
    {

        private static readonly Lazy<DefaultWriterMediaTypeFormatter> lazy =
               new Lazy<DefaultWriterMediaTypeFormatter>(() => new DefaultWriterMediaTypeFormatter());

        public static DefaultWriterMediaTypeFormatter Instance { get { return lazy.Value; } }

        private DefaultWriterMediaTypeFormatter()
        {
        }
    }
}