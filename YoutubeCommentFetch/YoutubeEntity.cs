using System;

namespace YoutubeCommentFetch
{
    public class YoutubeEntity
    {
        public YoutubeEntity
            (string apiKey, string videoId, string title, 
            string author, string comment, string originalComment,
            DateTime? publishedDate, DateTime? updatedDate, long? likeCount, string authorChannelUrl)
        {
            ApiKey = apiKey;
            VideoId = videoId;
            Title = title;
            Author = author;
            Comment = comment;
            OriginalComment = originalComment;
            PublishedDate = publishedDate;
            UpdatedDate = updatedDate;
            LikeCount = likeCount;
            AuthorChannelUrl = authorChannelUrl;
        }
        public string ApiKey { get; }

        public string VideoId { get; }

        public string Title { get; }

        public string Author { get; }

        public string Comment { get; }

        public string OriginalComment { get; }

        public DateTime? PublishedDate { get; }

        public DateTime? UpdatedDate { get; }

        public long? LikeCount { get; }

        public string AuthorChannelUrl { get; }
    }
}
