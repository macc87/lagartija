using System;
using System.Reflection;
using Fantasy.API.Utilities.Validation;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Fantasy.API.Utilities.LogError
{
    /// <summary>
    /// \\msp0nvnc01.cead.prd\ICSTEST\Logs\Web
    /// </summary>
    public class Logger
    {

        public string Path { get; private set; }

        private readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Logger(string path)
        {
            Path = path;
        }
        public void Setup()
        {

            Check.NotEmpty(Path, "Path");
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%message%newline";
            patternLayout.ActivateOptions();
            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = true;
            roller.File = Path;
            roller.Layout = patternLayout;
            roller.MaximumFileSize = "500MB";
            roller.LockingModel = new FileAppender.MinimalLock();
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);
            hierarchy.Configured = true;

        }
        public void Clear()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.ResetConfiguration();
        }
        public void LogMessage<T>(LogType logType, T message)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            var sr = new JsonTextWriter(sw);
            using (sr)
            {
                var serializer = new JsonSerializer
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented,

                };

                serializer.Serialize(sr, message);
            }
            var msg = sb.ToString();
            if (!string.IsNullOrWhiteSpace(msg))
            {
                switch (logType)
                {
                    case LogType.Debug:
                        Log.Debug(msg);
                        break;
                    case LogType.Info:
                        Log.Info(msg);
                        break;
                    case LogType.Error:
                        Log.Error(msg);
                        break;
                    case LogType.Fatal:
                        Log.Fatal(msg);
                        break;
                }
            }

        }
        public enum LogType
        {
            Debug,
            Info,
            Error,
            Fatal
        }
    }
    

}
