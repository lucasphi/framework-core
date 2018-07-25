using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// Setup log4net library. To change file destination, create an AppSetting "LogFile" within web.config
    /// </summary>
    public class FWLogger
    {
        static FWLogger()
        {
           
        }

        /// <summary>
        /// Creates a new logger.
        /// </summary>
        /// <returns>The logger.</returns>
        public static FWLogger Create()
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            //loggerFactory.

            var logger = new FWLogger();

            return logger;
        }

        /// <summary>
        /// Logs a message object with an error level.
        /// </summary>
        /// <param name="message">The message object reference.</param>
        public static void WriteError(object message)
        {
            //Log.Error(message);
        }

        /// <summary>
        /// Logs a message object with an error level, including the stacktrace.
        /// </summary>
        /// <param name="message">The message object reference.</param>
        /// <param name="exception">The exception to log, including it's stacktrace. </param>
        public static void WriteError(object message, Exception exception)
        {
            //Log.Error(message, exception);
        }

        /// <summary>
        /// Logs a message object with an info level.
        /// </summary>
        /// <param name="message">The message object reference.</param>
        public static void WriteInfo(object message)
        {
            //Log.Info(message);
        }

        /// <summary>
        /// Logs a message object with a debug level.
        /// </summary>
        /// <param name="message">A string containing zero or more format items.</param>
        /// <param name="args">The arguments to format the string.</param>
        public static void WriteDebug(string message, params object[] args)
        {
            //Log.DebugFormat(message, args);
        }
    }
}
