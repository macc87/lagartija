using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fantasy.API.Host.Models
{
    public class LogParser
    {
        #region Operational Methods

        // Read logs from the log folder.
        public IList<T> ReadLog<T>(LogType logType, DateTime dateFrom, DateTime dateTo) 
        {
            String logPath = String.Empty;
            String logFileContent = String.Empty;
            List<T> logList = new List<T>();

            String enviroment = Environment.GetEnvironmentVariable("ASSURNETENV"); 
            var server =  @"\\msp0nvnc01.cead.prd\ICS";

            if (enviroment.ToLower() == "local") { enviroment = "TEST"; }

            if (Convert.ToInt16(logType) < 100)
            {
                for (DateTime date = dateFrom; date.Date <= dateTo.Date; date = date.AddDays(1))
                {
                    //logPath = String.Format(@"{0}{1}\Logs\Web\{2}-{3}.txt", server, enviroment, EnumHelper.GetDescription(logType), date.ToString("yyyy-MM-dd"));
                    var dateF = date.ToString("yyyy-MM-dd");
                    logPath = $"{server}{enviroment}{Path.DirectorySeparatorChar}Logs{Path.DirectorySeparatorChar}Web{Path.DirectorySeparatorChar}{ EnumHelper.GetDescription(logType)}-{dateF}.txt";
                    if (File.Exists(logPath))
                    {
                        using (StreamReader reader = new StreamReader(logPath))
                        {
                            logFileContent = reader.ReadToEnd();
                        }
                        logList.AddRange(Deserialize<T>(logFileContent, logType));
                    }
                }
            }
            else
            {
                for (DateTime date = dateFrom; date.Date <= dateTo.Date; date = date.AddDays(1))
                {
                    //logPath = String.Format(@"{0}{1}\Logs\Web\Info\{2}-{3}.txt", server, enviroment, EnumHelper.GetDescription(logType), date.ToString("yyyy-MM-dd"));
                    var dateF = date.ToString("yyyy-MM-dd");
                    logPath = $"{server}{enviroment}{Path.DirectorySeparatorChar}Logs{Path.DirectorySeparatorChar}Web{Path.DirectorySeparatorChar}Info{Path.DirectorySeparatorChar}{ EnumHelper.GetDescription(logType)}-{dateF}.txt";

                    if (File.Exists(logPath))
                    {
                        using (StreamReader reader = new StreamReader(logPath))
                        {
                            logFileContent = reader.ReadToEnd();
                        }
                        logList.AddRange(Deserialize<T>(logFileContent, logType));
                    }
                }

            }

            return logList;
        }

        //Deserialize the log file into an object.
        private IList<T> Deserialize<T>(String logFileContent, LogType logType) 
        {
            if (String.IsNullOrEmpty(logFileContent)) { return new List<T>(); }

            var result = new List<T>();
            var stringValue = "";
            switch (logType)
            {
                case LogType.Fantasy:
                    stringValue = String.Format("[ {0} ]", logFileContent.Replace("\r\n}\r\n{\r\n  \"UserId\"", "\r\n},\r\n{\r\n  \"UserId\""));
                    using (var sr = new StringReader(stringValue))
                    {
                        using (var reader = new JsonTextReader(sr))
                        {
                            var serializer = new JsonSerializer
                            {
                                MissingMemberHandling = MissingMemberHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            };
                            result = serializer.Deserialize<IEnumerable<T>>(reader).ToList();
                        }
                    }
                    break;
            }
            return result;
        }
    }
        #endregion

}
