
using System.Text.Json;
using Book_Shelf.Models;
namespace Book_Shelf.Services;
public class OpenDbService
{
    private HttpClient _httpClient;
    private string _baseUrl = "https://api.openbd.jp/v1/get?isbn=";

    public OpenDbService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<SearchedBook>?> SearchBooks(string isbin)
    {
        var searchResults = new List<SearchedBook>();
        var response = await _httpClient.GetStringAsync(_baseUrl + isbin);
        using JsonDocument doc = JsonDocument.Parse(response);
        JsonElement root = doc.RootElement;
        foreach (JsonElement item in root.EnumerateArray())
        {
            if (item.ValueKind.ToString() == "Null")
            {
                return null;
            }
            var title = item.GetProperty("summary").GetProperty("title").GetString();
            var author = item.GetProperty("summary").GetProperty("author").GetString();
            var cover = item.GetProperty("summary").GetProperty("cover").GetString();
            searchResults.Add(new SearchedBook
            {
                Title = title,
                Author = author,
                CoverImage = string.IsNullOrEmpty(cover) ? "cover_not_found.png" : cover,
                Isbn = isbin,
            });
        }
        return searchResults;
    }
}