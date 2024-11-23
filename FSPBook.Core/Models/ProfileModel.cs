namespace FSPBook.Core.Models
{
    public class ProfileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public List<PostModel> Posts { get; set; } = new List<PostModel>();
    }
}
