using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeChannelViewJob
{
    public class CrawlerYoutubeChannel
    {
        private string ApiKey = ConfigurationManager.AppSettings["ApiKey"];
        public void CrawlerChannel()
        {
            
            var lstResult = new List<PlaylistItem>();
            var lstChannel = GetListChannel();

            var yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = ApiKey });
            var channelsListRequest = yt.Channels.List("contentDetails");
            channelsListRequest.Id = string.Join(",", lstChannel.Select(t => t.ChannelId)); 
            var channelsListResponse = channelsListRequest.Execute();
            foreach (var channel in channelsListResponse.Items)
            {
                // of videos uploaded to the authenticated user's channel.
                var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;
                var nextPageToken = "";
                while (nextPageToken != null)
                {
                    var playlistItemsListRequest = yt.PlaylistItems.List("snippet");
                    playlistItemsListRequest.PlaylistId = uploadsListId;
                    playlistItemsListRequest.MaxResults = 50;
                    playlistItemsListRequest.PageToken = nextPageToken;
                    // Retrieve the list of videos uploaded to the authenticated user's channel.
                    var playlistItemsListResponse = playlistItemsListRequest.Execute();
                    foreach (var playlistItem in playlistItemsListResponse.Items)
                    {
                        SaveVideo(playlistItem);
                        lstResult.Add(playlistItem);
                    }
                    nextPageToken = playlistItemsListResponse.NextPageToken;
                }
                //var a = 1;
            }
        }

        private List<Channel> GetListChannel() {
            var db = new youtubeEntities();
            var lstChannel = db.Database.SqlQuery<Channel>("yt_Get_Channel").ToList();
            return lstChannel;
        }

        private int SaveVideo (PlaylistItem playlistItem)
        {
            var db = new youtubeEntities();
            ObjectParameter objParam = new ObjectParameter("Id", typeof(int));

            SqlParameter param1 = new SqlParameter("@ChannelId", playlistItem.Snippet.ChannelId);
            SqlParameter param2  = new SqlParameter("@VideoEncryptedId", playlistItem.Snippet.ResourceId.VideoId);
            SqlParameter param3 = new SqlParameter("@VideoTitle", playlistItem.Snippet.Title);
            SqlParameter param4 = new SqlParameter("@VideoImageUrl", playlistItem.Snippet.Thumbnails.High.Url);

            var videoid = db.yt_Save_Video(playlistItem.Snippet.ChannelId, playlistItem.Snippet.ResourceId.VideoId, playlistItem.Snippet.Title, playlistItem.Snippet.Thumbnails.High.Url, objParam);
            return videoid;
        }
    }
}
