
namespace YouTubeChannelViewJob
{
    class Program
    {
        static void Main(string[] args)
        {
            var crawlerYTChannel = new CrawlerYoutubeChannel();
            crawlerYTChannel.CrawlerChannel();
        }
    }
}
