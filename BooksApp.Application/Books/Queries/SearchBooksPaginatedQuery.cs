using BooksApp.Application.Abstractions.Repository;
using BooksApp.Domain.Entities;
using MediatR;

namespace BooksApp.Application.Books.Queries
{
    public sealed record SearchBooksPaginatedQuery(
        string Title,
        string? MainCategory) : IRequest<IEnumerable<Book>>
    {
        public int PageSize { get; init; } = 10;
        public int PageNumber { get; init; } = 1;
    }

    public sealed class SearchBooksPaginatedQueryHandler(IBooksRepository booksRepository) : IRequestHandler<SearchBooksPaginatedQuery, IEnumerable<Book>>
    {
        private readonly IBooksRepository _booksRepository = booksRepository;

        public async Task<IEnumerable<Book>> Handle(SearchBooksPaginatedQuery request, CancellationToken cancellationToken)
        {
            return await _booksRepository.SearchPaginatedAsync(request.Title, request.MainCategory, request.PageSize, request.PageNumber, cancellationToken);
        }
    }
}
