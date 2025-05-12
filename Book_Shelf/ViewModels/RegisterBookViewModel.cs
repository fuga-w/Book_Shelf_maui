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
    private string _searchedBookCoverImage = string.Empty;
    private bool _inDisplaySearchResult = false;
    private string _isbin = string.Empty;
    private bool _isBookRegistered = false;

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

    public string SearchedBookCoverImage
    {
        get => _searchedBookCoverImage;
        set
        {
            _searchedBookCoverImage = value;
            OnPropertyChanged();
        }
    }

    public bool InDisplaySearchResult
    {
        get => _inDisplaySearchResult;
        set
        {
            _inDisplaySearchResult = value;
            OnPropertyChanged();
        }
    }

    public string Isbin
    {
        get => _isbin;
        set
        {
            _isbin = value;
            OnPropertyChanged();
        }
    }

    public bool IsBookRegistered
    {
        get => _isBookRegistered;
        set
        {
            _isBookRegistered = value;
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

    public async Task CheckBookRegistered(string isbn)
    {
        var books = await _managedBookRepository.GetAllBooks();
        IsBookRegistered = books.Any(b => b.Isbn == isbn);
    }

    public async Task UnregisterBook(string isbn)
    {
        var books = await _managedBookRepository.GetAllBooks();
        var book = books.FirstOrDefault(b => b.Isbn == isbn);
        if (book != null)
        {
            await _managedBookRepository.RemoveBook(book);
        }
        IsBookRegistered = false;
    }
}