using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeChannelViewJob
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new youtubeEntities();
            var lstChannel = db.Database.SqlQuery<Channel>("yt_Get_Channel").ToList();
            //YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = "AIzaSyBB66yN3hjS9O3kJxbdyXLt7LJaMR4DTWo" });

            var lstResult = new List<PlaylistItem>();

            var yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = "AIzaSyBB66yN3hjS9O3kJxbdyXLt7LJaMR4DTWo" });
            var channelsListRequest = yt.Channels.List("contentDetails");
            channelsListRequest.Id = string.Join(",", lstChannel.Select(t => t.ChannelID)); //"UCk8GzjMOrta8yxDcKfylJYw";
            //channelsListRequest. = "kkrofficial";
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
                        // Print information about each video.
                        //Console.WriteLine("Video Title= {0}, Video ID ={1}", playlistItem.Snippet.Title, playlistItem.Snippet.ResourceId.VideoId);
                        //var qry = (from s in ObjEdbContext.ObjTubeDatas where s.Title == playlistItem.Snippet.Title select s).FirstOrDefault();
                        //if (qry == null)
                        //{
                        //    objYouTubeData.VideoId = "https://www.youtube.com/embed/" + playlistItem.Snippet.ResourceId.VideoId;
                        //    objYouTubeData.Title = playlistItem.Snippet.Title;
                        //    objYouTubeData.Descriptions = playlistItem.Snippet.Description;
                        //    objYouTubeData.ImageUrl = playlistItem.Snippet.Thumbnails.High.Url;
                        //    objYouTubeData.IsValid = true;
                        //    ObjEdbContext.ObjTubeDatas.Add(objYouTubeData);
                        //    ObjEdbContext.SaveChanges();
                        //    ModelState.Clear();

                        //}
                        lstResult.Add(playlistItem);
                    }
                    nextPageToken = playlistItemsListResponse.NextPageToken;
                }
                //var a = 1;
                
                

            }
        }
    }
}
