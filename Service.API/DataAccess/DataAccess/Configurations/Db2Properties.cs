using System;
using System.Xml.Linq;

namespace DataAccess.Configurations
{
    /// <summary>
    /// Db2 Property class
    /// </summary>
    public class Db2Properties : BaseDbProperties
    {
        /// <summary>
        /// Db2 properties 
        /// </summary>
        /// <param name="schema">the db2 qualifier name</param>
        /// <param name="connectionStringName">connection string name</param>
        private Db2Properties(string schema, string connectionStringName)
            : base(schema, connectionStringName) { }


        /// <summary>
        /// Parse Db2 Properties from an XElement 
        /// </summary>
        /// <param name="element">the xelement to be parsed</param>
        /// <returns>If XElement has attributes return a IceDb2Properties. Otherwise returns null</returns>
        /// <exception cref="ArgumentNullException">Throws exception if parameter passed is null</exception>
        internal static Db2Properties ParseDb2PropertiesFromXElement(XElement element)
        {
            Db2Properties result = null;
            if (element == null)
                throw new ArgumentNullException("db2 element");
            if (element.HasAttributes)
            {
                string name = element.Attribute("name").Value;
                string schema = element.Attribute("schema").Value;
                result = new Db2Properties(schema, name);
            }
            return result;
        }
    }

}