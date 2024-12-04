using FSPBook.Core.Models;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using FSPBook.Core.Interfaces.Utilities;

namespace FSPBook.Core.Services.Utilities
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public NewsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            // Retrieve keys from appsettings.json
            _apiKey = _configuration["NewsApi:ApiKey"];
            _baseUrl = _configuration["NewsApi:BaseUrl"];
        }

        public async Task<List<NewsArticleModel>> GetLatestTechnologyNewsAsync(int count, string sources)
        {
            List<NewsArticleModel> articles = new List<NewsArticleModel>();
            try
            {
                var url = $"{_baseUrl}?api_token={_apiKey}&categories=tech&locale=us&limit={count}&sources={sources}";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var newsApiResponse = JsonSerializer.Deserialize<NewsApiResponseModel>(jsonResponse);


                if (newsApiResponse != null && newsApiResponse.data != null)
                {
                    articles = newsApiResponse.data.Select(news => new NewsArticleModel
                    {
                        Title = news.title,
                        Url = news.url,
                        ImageUrl = news.image_url,
                        Source = news.source,
                        PublishedAt = news.publishedAt
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return articles ?? new List<NewsArticleModel>();
        }
    }
}
