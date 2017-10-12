using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace DataAccess.Configurations
{
    /// <summary>
    /// Class represents a data layer environment 
    /// </summary>
    public sealed class DataLayerEnvironment
    {
        #region - variables
        /// <summary>
        /// The DataLayerEnvironment instance
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static volatile DataLayerEnvironment _instance;
        /// <summary>
        /// The syncRoot object
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly object SyncRoot = new Object();

        /// <summary>
        /// XDocument to hold environment values from the configuration file
        /// </summary>
        /// <remarks>
        /// XDocument is loaded and gets parsed only once when the class is initialized
        /// </remarks>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly XDocument _xDocument;

        private readonly bool _isConfigLoaded;
        #endregion

        #region - constructors

        /// <summary>
        /// Private constructor
        /// </summary>
        private DataLayerEnvironment(string serverEnviromentForTest = null)
        {
            //get environment first
            if (serverEnviromentForTest == null)
            {
                var environmentVariable = ConfigurationManager.AppSettings["Environment"];
                if (environmentVariable != null)
                {
                    ServerEnvironment = environmentVariable.ToLower();
                }
                else
                {
                    throw new Exception("Unable to determine server environment.");
                }                
            }
            else
            {
                ServerEnvironment = serverEnviromentForTest;
            }
           
            var configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            _xDocument = XDocument.Load(configPath);
            Initialize();
            _isConfigLoaded = true;
        }

        #endregion

        #region - properties

        /// <summary>
        /// Server environment
        /// </summary>
        public string ServerEnvironment { get; set; }

        /// <summary>
        /// Check if Configuration was successfully loaded
        /// </summary>
        public bool IsConfigLoaded { get { return _isConfigLoaded; } }

        /// <summary>
        /// Adjuster Db properties
        /// </summary>
        public SportsRadarProperties SportsRadarProperties { get; private set; }
        /// <summary>
        /// Mailbox Db properties
        /// </summary>
        public MssqlProperties FantasyMssqlProperties { get; private set; }

        #endregion

        #region - private methods

        /// <summary>
        /// Initialize property values
        /// </summary> 
        private void Initialize()
        {
            var xEnv = _xDocument.Descendants("DataLayerEnvironment").Elements("environments");//get environments node
            var atts = xEnv.Descendants().Attributes("id"); //get evironment id's
            XElement envElement = null;

            switch (ServerEnvironment)
            {
                case "local":
                case "test":
                    envElement = atts.Where(x => x.Value == DbEnvironments.test.ToString()).Select(att => att.Parent).First();
                    break;
                case "modl":
                case "train":
                    envElement = atts.Where(x => x.Value == DbEnvironments.modl.ToString()).Select(att => att.Parent).First();
                    break;
                case "prod":
                case "emer":
                    envElement = atts.Where(x => x.Value == DbEnvironments.prod.ToString()).Select(att => att.Parent).First();
                    break;
                default:
                    throw new Exception("Unable to determine server environment.");

            }
            ParseEnvironmentElements(envElement);
        }
        /// <summary>
        /// Function parses environment elements
        /// </summary>
        /// <param name="element"></param>
        private void ParseEnvironmentElements(XContainer element)
        {
            SportsRadarProperties = SportsRadarProperties.ParseSportsRadarPropertiesFromXElement(element.Descendants("sportsRadar").First());
            FantasyMssqlProperties = MssqlProperties.ParseMssqlPropertiesFromXElement(element.Descendants("fantasyMssql").First());
        }
      
        #endregion

        #region - public methods

        /// <summary>
        /// Allow only one instance to run
        /// </summary> 
        /// <returns>DataLayerEnvironment Instance</returns>
        public static DataLayerEnvironment GetInstance(string enviroment = null)
        {
            if (_instance != null && enviroment==null) return _instance;
            lock (SyncRoot)
            {
                _instance = new DataLayerEnvironment(enviroment);

            }
            return _instance;
        }

        #endregion
    }
}
