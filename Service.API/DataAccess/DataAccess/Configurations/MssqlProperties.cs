using System;
using System.Xml.Linq;

namespace Fantasy.API.DataAccess.Configurations
{


    ///<summary>
    /// Represent an oracle properties object
    /// <para>
    /// This class is used to load schema, connectionstring, etc.. from the configuration file
    /// </para>
    ///</summary> 
    public class MssqlProperties : BaseDbProperties
    {
        ///<summary>
        /// Default constructor
        ///</summary>
        private MssqlProperties(string schema, string connectionStringName)
            : base(schema, connectionStringName) { }
        /// <summary>
        /// Parse Db2 Properties from an XElement 
        /// </summary>
        /// <param name="element">the xelement to be parsed</param>
        /// <returns>If XElement has attributes return a IceOracleProperties. Otherwise returns null</returns>
        /// <exception cref="ArgumentNullException">Throws exception if parameter passed is null</exception>
        internal static MssqlProperties ParseMssqlPropertiesFromXElement(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            if (!element.HasAttributes) return null;
            var name = element.Attribute("name").Value;
            var schema = element.Attribute("schema").Value;
            return new MssqlProperties(schema, name);
        }

    }
}
