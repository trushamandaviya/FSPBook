using FSPBook.Core.Models;

namespace FSPBook.Core.Interfaces.Utilities
{
    public interface INewsService
    {
        Task<List<NewsArticleModel>> GetLatestTechnologyNewsAsync(int count, string sources);
    }
}
