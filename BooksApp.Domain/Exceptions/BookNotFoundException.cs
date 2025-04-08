namespace BooksApp.Domain.Exceptions
{
    public sealed class BookNotFoundException(string bookId) : Exception($"The book with identifier {bookId} was not found.")
    {
    }
}
