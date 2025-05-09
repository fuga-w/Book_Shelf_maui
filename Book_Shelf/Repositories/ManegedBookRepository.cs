using Book_Shelf.Models;
namespace Book_Shelf.Repositories;

public interface IManagedBookService
{
    Task<List<ManagedBook>> GetAllBooks();
    Task AddBook(ManagedBook book);
}
public class ManagedBookRepository
{
    private IManagedBookService _managedBookService;

    public ManagedBookRepository(IManagedBookService managedBookService)
    {
        _managedBookService = managedBookService;
    }
    public async Task<List<ManagedBook>> GetAllBooks()
    {
        return await _managedBookService.GetAllBooks();
    }

    public async void AddBook(ManagedBook book)
    {
        await _managedBookService.AddBook(book);
    }
}