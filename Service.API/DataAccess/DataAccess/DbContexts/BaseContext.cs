using Fantasy.API.Utilities.Extensions;
using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Fantasy.API.DataAccess.DbContexts
{

	/// <summary>
	/// Abstract class to serve as an entry point by all dbcontexts
	/// </summary>
	/// <typeparam name="T">The context class implementing 'BaseContext' </typeparam>
    public abstract class BaseContext<T> : DbContext where T : DbContext
	{
		/// <summary>
		/// <span>This static method will run before class get initialized
		/// and will prevent the DbContext from initializing the database</span>
		/// </summary> 
		static BaseContext()
		{
			Database.SetInitializer<T>(null);
		}
		/// <summary>
		/// Constructor:
		/// Get entity connection based on the server environment 
		/// </summary>
		/// <param name="connectionString">the connection string</param>
		protected BaseContext(string connectionString)
			: base(connectionString)
		{
			this.Configuration.LazyLoadingEnabled = false;
		}
		/// <summary>
		/// Initialize with a DbConnection object
		/// </summary>
		/// <param name="connection">The Dbconnection</param>
		protected BaseContext(DbConnection connection)
			: base(connection,false)
		{
			this.Configuration.LazyLoadingEnabled = false;
		}
        /// <summary>
		/// Overrides DbContext base SaveChanges() in order to catch DbEntityValidation exceptions
		/// <para>
		/// at a base class level</para>
		/// </summary> 
		public override int SaveChanges()
		{
			try
			{
				return base.SaveChanges();
			}
            catch (DbEntityValidationException ex)
			{
				// Retrieve the error messages as a list of strings.
				var errorMessages = ex.EntityValidationErrors
						.SelectMany(x => x.ValidationErrors)
						.Select(x => x.ErrorMessage);
				// Join the list to a single string.
				var fullErrorMessage = string.Join("; ", errorMessages);

				// Combine the original exception message with the new one.
				var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

				// Throw a new DbEntityValidationException with the improved exception message.
				throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
			}
		}

	}

    public static class CustomConfigurationManager
    {
        public static ConnectionStringsIndexer ConnectionStrings = new ConnectionStringsIndexer();

        public class ConnectionStringsIndexer
        {
    

            public string this[string index]
            {
                get
                {
                    var connectionString = ConfigurationManager.ConnectionStrings[index];
                    return new ConnectionStringSettings(connectionString.Name, connectionString.ConnectionString.Decrypt(), connectionString.ProviderName).ToString();
                }
            }
        }

    }
}
