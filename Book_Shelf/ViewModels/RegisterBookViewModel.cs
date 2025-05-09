using Book_Shelf.Services;
using Book_Shelf.Models;
using Book_Shelf.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
namespace Book_Shelf.ViewModels;
public class RegisterBookPageViewModel : ObservableObject
{
    private OpenDbService _openDbService;
    private ManagedBookRepository _managedBookRepository;
    private string _searchedBookTitle = string.Empty;
    private string _searchedBookAuthor = string.Empty;

    public string SearchedBookTitle
    {
        get => _searchedBookTitle;
        set
        {
            _searchedBookTitle = value;
            OnPropertyChanged();
        }
    }

    public string SearchedBookAuthor
    {
        get => _searchedBookAuthor;
        set
        {
            _searchedBookAuthor = value;
            OnPropertyChanged();
        }
    }

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

    public async Task RegisterBook(ManagedBook book)
    {
        await _managedBookRepository.AddBook(book);
    }
}