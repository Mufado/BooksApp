using BooksApp.Application.Abstractions.Repository;
using BooksApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Persistence.Implementations.Repositories
{
    public class BooksRepository(BooksAppDbContext context) : IBooksRepository
    {
        private readonly BooksAppDbContext _context = context;

        public Task CreateAsync(Book book, CancellationToken cancellationToken)
        {
            return _context.Books.AddAsync(book, cancellationToken).AsTask();
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken)
        {
            return (await _context.Books.ToListAsync(cancellationToken)).AsEnumerable();
        }

        public Task<Book?> GetByIdOrGoogleBookIdAsync(string id, CancellationToken cancellationToken)
        {
            return Guid.TryParse(id, out var guidId) ?
                _context.Books.FindAsync([guidId], cancellationToken).AsTask() :
                _context.Books.FirstOrDefaultAsync(b => b.GoogleBooksId == id, cancellationToken);
        }

        public async Task<IEnumerable<Book>> SearchPaginatedAsync(string title, string? mainCategory, int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
            var query = _context.Books.Where(b => EF.Functions.Like(b.Title, $"%{title}%"));

            if (!string.IsNullOrWhiteSpace(mainCategory))
            {
                query = query.Where(b => EF.Functions.Like(b.MainCategory, $"%{mainCategory}%"));
            }

            var result = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return result.AsEnumerable();
        }

        public Task SaveChangesAsync(Book book, CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
