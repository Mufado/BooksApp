using BooksApp.Domain.Entities;

namespace BooksApp.Application.Abstractions.Repository
{
	public interface IBooksRepository : IBaseRepository<Book>
	{
		Task CreateAsync(Book book, CancellationToken cancellationToken);

		Task SaveChangesAsync(Book book, CancellationToken cancellationToken);

		Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken);

		Task<Book?> GetByIdOrGoogleBookIdAsync(string id, CancellationToken cancellationToken);

		Task<IEnumerable<Book>> SearchPaginatedAsync(string title, string? mainCategory, int pageSize, int pageNumber, CancellationToken cancellationToken);
	}
}
