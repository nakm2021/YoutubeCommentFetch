using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoutubeCommentFetch
{
    internal static class Program
    {
        [Obsolete]
        internal static async Task Main(string[] args)
        {
            var result = await GetData();
            result.ForEach(x =>
            {
                Console.WriteLine(x.Title);
                Console.WriteLine(x.Author);
                Console.WriteLine(x.OriginalComment);
                Console.WriteLine(x.LikeCount);
                Console.WriteLine(x.PublishedDate);
                Console.WriteLine(x.UpdatedDate);
            });
            Console.ReadKey();
        }

        [Obsolete]
        internal static async Task<List<YoutubeEntity>> GetData()
        {
            // https://www.youtube.com/watch?v=xxxxx
            // xxxxx の部分を指定
            string videoId = "boGoP-jvDcc";

            var baseClientService = new BaseClientService.Initializer();
            baseClientService.ApiKey = Common.API_KEY;
            baseClientService.ApplicationName = "YoutubeCommentFetcher";
            var youtubeService = new YouTubeService(baseClientService);

            var title = await GetVideoTitle(youtubeService, videoId);
            var result = await FetchComment(youtubeService, videoId);
            var list = new List<YoutubeEntity>();

            result.Items.ToList().ForEach(x =>
            {
                var comment = x.Snippet.TopLevelComment.Snippet;
                list.Add(new YoutubeEntity(Common.API_KEY, videoId, title, comment.AuthorDisplayName,
                    comment.TextDisplay, comment.TextOriginal, comment.PublishedAt, comment.UpdatedAt,
                    comment.LikeCount, comment.AuthorProfileImageUrl));
            });

            return list;
        }
        internal static async Task<CommentThreadListResponse> FetchComment(YouTubeService youtubeService, string videoId)
        {
            var request = youtubeService.CommentThreads.List("snippet");
            request.VideoId = videoId;
            // コメント取得件数
            request.MaxResults = 999999;

            var response = await request.ExecuteAsync();
            return response;
        }

        internal static async Task<string> GetVideoTitle(YouTubeService youTubeService, string videoId)
        {
            var request = youTubeService.Videos.List("snippet");
            request.Id = videoId;

            var response = await request.ExecuteAsync();
            if (response.Items.Count > 0)
            {
                return response.Items[0].Snippet.Title;
            }
            return "Title not found.";
        }
    }
}
