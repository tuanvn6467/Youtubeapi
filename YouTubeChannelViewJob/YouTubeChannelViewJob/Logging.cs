using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouTubeChannelViewJob
{
    public static class Logging
    {
        private static readonly ILog Log = LogManager.GetLogger("ForAllApplication");

        public static void Error(string message, Exception e)
        {
            Console.WriteLine(message, e.ToString());
            Log.Error(message, e);
        }
        public static void Error(string message)
        {
            Console.WriteLine(message);
            Log.Error(message);
        }
        public static void Debug(string message)
        {
            Console.WriteLine(message);
            Log.Debug(message);
        }
    }
}
