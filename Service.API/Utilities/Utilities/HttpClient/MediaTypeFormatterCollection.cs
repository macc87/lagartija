using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Formatting;

namespace Utilities.HttpClient
{
    internal sealed class MediaTypeFormatterCollection : ReadOnlyCollection<MediaTypeFormatter>
    {

        private static readonly Lazy<MediaTypeFormatterCollection> lazy = new Lazy<MediaTypeFormatterCollection>(() => new MediaTypeFormatterCollection());

        public static IEnumerable Instance
        {
            get { return lazy.Value; }
        }

        private MediaTypeFormatterCollection()
            : base(new List<MediaTypeFormatter> {new JsonMediaTypeFormatter()})
        {
        }
    }
}
