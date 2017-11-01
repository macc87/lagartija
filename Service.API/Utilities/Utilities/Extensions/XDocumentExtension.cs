using System.Linq;
using System.Xml.Linq;

namespace Fantasy.API.Utilities.Extensions
{
    public static class XDocumentExtension
    {

            public static XElement GetSoapBody(this XDocument doc)
            {
                var body = doc.Descendants().FirstOrDefault(x => x.Name.LocalName.ToLowerInvariant() == "body");
                return body;
            }
        
    }
}
