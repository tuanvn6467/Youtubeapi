using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace YouTubeChannelViewJob
{
    public class CrawlerYoutubeChannel
    {
        private string ApiKey = ConfigurationManager.AppSettings["ApiKey"];
        public void CrawlerChannel()
        {
            Logging.Debug("-----------------------Start Crawler Channel Yotube----------------- at " + DateTime.Now);
            try
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

                        var dic = new Dictionary<int, string>(); //(videoid, videoencryptedid)

                        foreach (var playlistItem in playlistItemsListResponse.Items)
                        {
                            var videoid = SaveVideo(playlistItem);
                            if (!dic.ContainsKey(videoid))
                                dic.Add(videoid, playlistItem.Snippet.ResourceId.VideoId);
                            //lstResult.Add(playlistItem);
                        }

                        //get detail video with videoid
                        var videosListRequest = yt.Videos.List("statistics");
                        videosListRequest.Id = string.Join(",", playlistItemsListResponse.Items.Select(t => t.Snippet.ResourceId.VideoId));
                        var videosListResponse = videosListRequest.Execute();

                        foreach (var video in videosListResponse.Items)
                        {
                            var videoid = dic.FirstOrDefault(t => t.Value == video.Id).Key;
                            SaveVideoNumberCount(videoid, video);
                        }

                        nextPageToken = playlistItemsListResponse.NextPageToken;
                    }
                    //var a = 1;
                }
            }
            catch (Exception ex)
            {
                Logging.Error("Error: ", ex);
            }
            Logging.Debug("-----------------------End Crawler Channel Yotube----------------- at " + DateTime.Now);
        }

        private List<Channel> GetListChannel()
        {
            var db = new youtubeEntities();
            var lstChannel = db.Database.SqlQuery<Channel>("yt_Get_Channel").ToList();
            return lstChannel;
        }

        private int SaveVideo(PlaylistItem playlistItem)
        {
            var db = new youtubeEntities();
            ObjectParameter objParam = new ObjectParameter("ReturnId", typeof(int));
            var videoid = db.yt_Save_Video(
                playlistItem.Snippet.ChannelId,
                playlistItem.Snippet.ResourceId.VideoId,
                playlistItem.Snippet.Title,
                playlistItem.Snippet.Thumbnails.Default__.Url, objParam);
            return Convert.ToInt32(objParam.Value);
        }

        private int SaveVideoNumberCount(int videoid, Google.Apis.YouTube.v3.Data.Video video)
        {
            var db = new youtubeEntities();
            ObjectParameter objParam = new ObjectParameter("Id", typeof(int));

            var videoNumberCountId = db.yt_Save_Video_NumberCount(
                videoid,
                ParseData.GetLong(video.Statistics.ViewCount),
                ParseData.GetLong(video.Statistics.LikeCount),
                ParseData.GetLong(video.Statistics.DislikeCount),
                ParseData.GetLong(video.Statistics.FavoriteCount),
                ParseData.GetLong(video.Statistics.CommentCount)
                , objParam);
            return Convert.ToInt32(objParam.Value);
        }


    }
}
