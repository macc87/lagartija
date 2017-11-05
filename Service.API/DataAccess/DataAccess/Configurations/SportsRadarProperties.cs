using System;
using System.Xml.Linq;

namespace Fantasy.API.DataAccess.Configurations
{
    public class SportsRadarProperties
    {
        private readonly string _url;
        private readonly string _name;
        private readonly string _apiKey;

        ///<summary>
        /// Default constructor
        ///</summary>
        private SportsRadarProperties(string name, string url, string apiKey)
        {
            _name = name;
            _url = url;
            _apiKey = apiKey;
        }

        /// <summary>
        /// Url to service
        /// </summary>
        public string Url
        {
            get { return _url; }
        }

        /// <summary>
        /// Name of the property
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// APIKey of the property
        /// </summary>
        public string ApiKey
        {
            get { return _apiKey; }
        }

        /// <summary>
        /// Parse Filenet service properties from an XElement 
        /// </summary>
        /// <param name="element">the xelement to be parsed</param>
        /// <returns>If XElement has attributes return a FileNetProperties. Otherwise returns null</returns>
        /// <exception cref="ArgumentNullException">Throws exception if parameter passed is null</exception>
        internal static SportsRadarProperties ParseSportsRadarPropertiesFromXElement(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            if (!element.HasAttributes) return null;
            return new SportsRadarProperties
                (element.Attribute("name").Value,
                    element.Attribute("url").Value,
                    element.Attribute("api_key").Value);
        }
    }
}
