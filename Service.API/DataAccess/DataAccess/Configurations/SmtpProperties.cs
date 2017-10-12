using System;
using System.Xml.Linq;

namespace DataAccess.Configurations
{


    ///<summary>
    /// Represent an oracle properties object
    /// <para>
    /// This class is used to load url. from the configuration file
    /// </para>
    ///</summary> 
    public class SmtpProperties
    {
        private readonly string _url;
        private readonly string _name;

        ///<summary>
        /// Default constructor
        ///</summary>
        private SmtpProperties(string name, string url)
        {
            _name = name;
            _url = url;
        }
        /// <summary>
        /// Url to Ldap service
        /// </summary>
        public string Url { get { return _url; } }
        /// <summary>
        /// Name of the property
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Parse Filenet service properties from an XElement 
        /// </summary>
        /// <param name="element">the xelement to be parsed</param>
        /// <returns>If XElement has attributes return a FileNetProperties. Otherwise returns null</returns>
        /// <exception cref="ArgumentNullException">Throws exception if parameter passed is null</exception>
        internal static SmtpProperties ParseSmtpPropertiesFromXElement(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            if (!element.HasAttributes) return null;
            var name = element.Attribute("name").Value;
            var url = element.Attribute("url").Value;
            return new SmtpProperties(name, url);
        }

    }
}
