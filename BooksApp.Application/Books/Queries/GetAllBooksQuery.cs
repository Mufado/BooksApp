using BooksApp.Application.Abstractions.Repository;
using BooksApp.Domain.Entities;
using MediatR;

namespace BooksApp.Application.Books.Queries
{
    public sealed record GetAllBooksQuery() : IRequest<IEnumerable<Book>>;

    public sealed class GetAllBooksQueryHandler(IBooksRepository booksRepository) : IRequestHandler<GetAllBooksQuery, IEnumerable<Book>>
    {
        private readonly IBooksRepository _booksRepository = booksRepository;

        public async Task<IEnumerable<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _booksRepository.GetAllBooksAsync(cancellationToken); // Would use some pattern to return data to other layers in a real app
        }
    }
}
