using Book_Shelf.Models;
using Book_Shelf.Repositories;

namespace Book_Shelf.Services;
internal class DummyManagedBookService : IManagedBookService
{
    private List<ManagedBook> _books = [];

    public DummyManagedBookService() { }

    public Task<List<ManagedBook>> GetAllBooks()
    {
        return Task.FromResult(_books);
    }

    public Task AddBook(ManagedBook book)
    {
        _books.Add(book);
        return Task.CompletedTask;
    }

    public Task RemoveBook(ManagedBook book)
    {
        _books.RemoveAll(b => b.Isbn == book.Isbn);
        return Task.CompletedTask;
    }
}