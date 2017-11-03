using System;
using System.Xml.Linq;

namespace DataAccess.Configurations
{


    ///<summary>
    /// Represent an oracle properties object
    /// <para>
    /// This class is used to load schema, connectionstring, etc.. from the configuration file
    /// </para>
    ///</summary> 
    public class OracleProperties : BaseDbProperties
    {
        ///<summary>
        /// Default constructor
        ///</summary>
        private OracleProperties(string schema, string connectionStringName)
            : base(schema, connectionStringName) { }
        /// <summary>
        /// Parse Db2 Properties from an XElement 
        /// </summary>
        /// <param name="element">the xelement to be parsed</param>
        /// <returns>If XElement has attributes return a IceOracleProperties. Otherwise returns null</returns>
        /// <exception cref="ArgumentNullException">Throws exception if parameter passed is null</exception>
        internal static OracleProperties ParseOraPropertiesFromXElement(XElement element)
        {
            OracleProperties result = null;
            if (element == null)
                throw new ArgumentNullException("oracle element");
            if (element.HasAttributes)
            {
                string name = element.Attribute("name").Value;
                string schema = element.Attribute("schema").Value;
                result = new OracleProperties(schema, name);
            }
            return result;
        }

    }
}
