using System.Collections.ObjectModel;
using Book_Shelf.Models;
using Book_Shelf.Repositories;
namespace Book_Shelf.ViewModels;
public class ManagedBooksViewModel
{
    private ObservableCollection<ManagedBook> _books;
    private ManagedBookRepository _repository;
    public ObservableCollection<ManagedBook> Books
    {
        get => _books;
    }

    public ManagedBooksViewModel(ManagedBookRepository repository)
    {
        _repository = repository;
        _books = new ObservableCollection<ManagedBook>();
        loadAllBooks();
    }

    public async Task LoadBooksAsync()
    {
        var books = await _repository.GetAllBooks();
        _books.Clear();
        foreach (var book in books)
        {
            _books.Add(book);
        }
    }

    private async void loadAllBooks()
    {
        var books = await _repository.GetAllBooks();
        _books.Clear();
        foreach (var book in books)
        {
            _books.Add(book);
        }
    }
}