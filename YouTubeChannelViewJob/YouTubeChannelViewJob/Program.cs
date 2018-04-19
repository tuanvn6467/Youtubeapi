
using log4net;

namespace YouTubeChannelViewJob
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            var crawlerYTChannel = new CrawlerYoutubeChannel();
            crawlerYTChannel.CrawlerChannel();
        }
    }
}
