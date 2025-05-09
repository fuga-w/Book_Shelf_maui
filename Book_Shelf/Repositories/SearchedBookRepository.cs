using Book_Shelf.Models;
namespace Book_Shelf.Repositories;

internal interface IBookSearchService
{
    Task<SearchedBook?> SearchBooks(string isbin);
}
internal class SearchedBookRepository
{
    private IBookSearchService _bookSearchService;

    public SearchedBookRepository(IBookSearchService bookSearchService)
    {
        _bookSearchService = bookSearchService;
    }
    async Task<SearchedBook?> FindByIsbin(string isbin)
    {
        var book = await _bookSearchService.SearchBooks(isbin);
        return book;
    }
}