namespace FSPBook.Core.Models
{
    public class NewsApiResponseModel
    {
        public Meta meta { get; set; }
        public List<NewsArticle> data { get; set; }
    }
    public class Meta
    {
        public int found { get; set; }
        public int returned { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
    }

    public class NewsArticle
    {
        public string? uuid { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? url { get; set; }
        public string? image_url { get; set; }
        public string? language { get; set; }
        public DateTime? publishedAt { get; set; }
        public string? source { get; set; }
        public List<string> categories { get; set; }
    }
}
