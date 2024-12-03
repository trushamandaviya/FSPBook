namespace FSPBook.Core.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset DateTimePosted { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
