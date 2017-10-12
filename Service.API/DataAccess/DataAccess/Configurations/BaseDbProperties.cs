namespace DataAccess.Configurations
{


    ///<summary>
    /// Base class for Database properties
    ///</summary>
    public class BaseDbProperties
    {
        ///<summary>
        /// Default constructor
        ///</summary>
        protected BaseDbProperties(string dbSchema, string connectionStringName)
        {
            DbSchema = dbSchema;
            ConnectionStringName = connectionStringName;
        }

        #region - properties
        /// <summary>
        /// Db connection string name 
        /// </summary>
        /// <remarks>Connection must exists in the App/Web config file</remarks>
        public string ConnectionStringName
        {
            get;
            private set;
        }

        /// <summary>
        /// Database schema used for table qualifier
        /// </summary>
        public string DbSchema
        {
            get;
            private set;
        }
        #endregion

    }
}
