using BooksApp.Application.Abstractions.Repository;
using BooksApp.Domain.Entities;
using BooksApp.Domain.Exceptions;
using MediatR;

namespace BooksApp.Application.Books.Queries
{
	public sealed record GetBookByIdOrGoogleBookIdQuery(string Id) : IRequest<Book>;

    public sealed class GetBookByIdOrGoogleBookIdQueryHandler(IBooksRepository booksRepository) : IRequestHandler<GetBookByIdOrGoogleBookIdQuery, Book>
    {
        private readonly IBooksRepository _booksRepository = booksRepository;

        public async Task<Book> Handle(GetBookByIdOrGoogleBookIdQuery query, CancellationToken cancellationToken)
        {
            var book = await _booksRepository.GetByIdOrGoogleBookIdAsync(query.Id, cancellationToken)
                ?? throw new BookNotFoundException(query.Id);

            return book; // Would use some pattern to return data to other layers in a real app
        }
    }
}
