﻿namespace TheDevBlog_API.Models.DTO
{
    public class AddPostRequest
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Summary { get; set; }
        public string? UrlHandler { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public bool Visible { get; set; }
        public string? Author { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}