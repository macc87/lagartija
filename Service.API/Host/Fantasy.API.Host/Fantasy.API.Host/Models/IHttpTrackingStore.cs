using System;
using System.Threading.Tasks;
using Fantasy.API.Utilities.LogError;

namespace Fantasy.API.Host.Models
{
    /// <summary>
    /// Interface for tracking details about HTTP calls.
    /// </summary>
    public interface IHttpTrackingStore
    {
        /// <summary>
        /// Persist details of an HTTP call into durable storage.
        /// </summary>
        /// <param name="record">Current request track</param>
        /// <returns></returns>
        Task InsertRecordAsync(AdminAccLog record);

        void InsertRecord(AdminAccLog record);
    }

    public class HttpTrackingStore : IHttpTrackingStore
    {
        public async Task InsertRecordAsync(AdminAccLog record)
        {
            var enviroment = Environment.GetEnvironmentVariable("ASSURNETENV");
            if (!string.IsNullOrEmpty(enviroment) && enviroment.ToLower() == "local")
            {
                enviroment = "TEST";
            }
            var path = @"\\msp0nvnc01.cead.prd\ICS" + enviroment + @"\Logs\Web\" + "Admin-" + record.CallerIdentity.ApplicationName + "-" + DateTime.Now.Date.ToString("yyyy-MM-dd") + @".txt";
           // var httpEntries = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(record, Formatting.Indented));
            var logger = new Logger(path);
            logger.Setup();
            logger.LogMessage(Logger.LogType.Info, record);
            logger.Clear();
        }
        public void InsertRecord(AdminAccLog record)
        {
            var enviroment = Environment.GetEnvironmentVariable("ASSURNETENV");
            if (!string.IsNullOrEmpty(enviroment) && enviroment.ToLower() == "local")
            {
                enviroment = "TEST";
            }
            var path = @"\\msp0nvnc01.cead.prd\ICS" + enviroment + @"\Logs\Web\" + "Admin-" + record.CallerIdentity.ApplicationName + "-" + DateTime.Now.Date.ToString("yyyy-MM-dd") + @".txt";
           // var httpEntries = JsonConvert.SerializeObject(record, Formatting.Indented);
            var logger = new Logger(path);
            logger.Setup();
            logger.LogMessage(Logger.LogType.Info, record);
            logger.Clear();
        }
    }
}