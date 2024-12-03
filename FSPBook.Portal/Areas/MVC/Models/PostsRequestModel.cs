namespace FSPBook.Portal.Areas.MVC.Models
{
    public class PostsRequestModel
    {
        public int? PageNumber { get; set; }
        public int? LatestPostId { get; set; }
        public int? UserId { get; set; }
        public int? PageSize { get; set; } = Constants.PageSize;
    }
}
