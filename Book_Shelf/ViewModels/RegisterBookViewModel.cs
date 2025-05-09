using Book_Shelf.Services;
using Book_Shelf.Models;
using Book_Shelf.Repositories;
namespace Book_Shelf.ViewModels;
public class RegisterBookPageViewModel
{
    private OpenDbService _openDbService;
    private ManagedBookRepository _managedBookRepository;

    public RegisterBookPageViewModel(OpenDbService openDbService, ManagedBookRepository managedBookRepository)
    {
        _openDbService = openDbService;
        _managedBookRepository = managedBookRepository;
    }

    public async Task<SearchedBook?> LookupBookByIsbin(string isbin)
    {
        var result = await _openDbService.SearchBooks(isbin);
        return result == null ? null : result.FirstOrDefault();
    }
}