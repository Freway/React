using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Linq;

namespace BR.POINTER.LASTPOSITION.API.Helpers
{
    public class Log
    {
        public static readonly ILog log;
        public static List<RequestData> Request = new List<RequestData>();

        static Log()
        {

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod(int frame = 3)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(frame);

            return $"{sf.GetMethod().ReflectedType.Name.ToUpper()}.{sf.GetMethod().Name.ToUpper()}";
        }

        public static void AddLogging(string obj, string connectionId)
        {
            GetRequest(connectionId).Logging += obj + "; ";
        }

        public static RequestData GetRequest(string connectionId)
        {            
            return Request.Where(p => p.ConnectionId == connectionId).FirstOrDefault();
        }

        public static string CreateLog(object obj, string connectionId)
        {
            var request = GetRequest(connectionId);
            return $"[{request.Request.Method}] [{request.IpAddress}] [{request.Port}] [{request.Request.Path}] [{GetCurrentMethod(3)}] [{request.Duration.ToString("ss\\.fff")}] - BEGIN; {obj} END";
        }

        public static string CreateLog(string connectionId)
        {
            var request = GetRequest(connectionId);
            return $"[{request.Request.Method}] [{request.IpAddress}] [{request.Port}] [{request.Request.Path}] [{GetCurrentMethod(3)}] [{request.Duration.ToString("ss\\.fff")}] - BEGIN; {request.Logging} END";
        }

        //  INFO
        public static void Info(object obj) { log.Info($"[{GetCurrentMethod()}] - {obj}"); }
        public static void Info(object obj, string connectionId) { log.Info(CreateLog(obj, connectionId)); }
        public static void Info(string connectionId) { log.Info(CreateLog(connectionId)); }
        //  ERROR
        public static void Error(object obj) { log.Error($"[{GetCurrentMethod()}] - {obj}"); }
        public static void Error(object obj, string connectionId) { log.Error(CreateLog(obj, connectionId)); }
        public static void Error(string connectionId) { log.Error(CreateLog(connectionId)); }
        //  WARN
        public static void Warn(object obj) { log.Warn($"[{GetCurrentMethod()}] - {obj}"); }
        public static void Warn(object obj, string connectionId) { log.Warn(CreateLog(obj, connectionId)); }
        public static void Warn(string connectionId) { log.Warn(CreateLog(connectionId)); }

    }
}
