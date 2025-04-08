using BooksApp.Application.Abstractions.Repository;
using BooksApp.Application.Mapping;
using BooksApp.Domain.DTOs;
using BooksApp.Domain.Entities;
using MediatR;

namespace BooksApp.Application.Books.Commands
{
    public sealed record UpsertBookCommand(
        string Id,
        string Title,
        string? SubTitle,
        int PageCount,
        string? MainCategory,
        List<string>? Categories,
        float? Height,
        float? Width,
        float? Thickness,
        float? AverageRating,
        long? RatingsCount,
        bool IsGoogleBookId) : IRequest
    {
        public UpsertBookCommand(BookDetailsDto booksDetailsDto, bool isGoogleBookId)
            : this(
                booksDetailsDto.Id,
                booksDetailsDto.Title,
                booksDetailsDto.SubTitle,
                booksDetailsDto.PageCount,
                booksDetailsDto.MainCategory,
                booksDetailsDto.Categories,
                booksDetailsDto.Height,
                booksDetailsDto.Width,
                booksDetailsDto.Thickness,
                booksDetailsDto.AverageRating,
                booksDetailsDto.RatingsCount,
                isGoogleBookId)
        { }
    }

	public sealed class UpsertBookCommandHandler(IBooksRepository booksRepository) : IRequestHandler<UpsertBookCommand>
	{
		private readonly IBooksRepository _booksRepository = booksRepository;

		public async Task Handle(UpsertBookCommand command, CancellationToken cancellationToken)
		{
			var book = await _booksRepository.GetByIdOrGoogleBookIdAsync(command.Id, cancellationToken);

			if (book is null)
			{
                book = new Book();
                await _booksRepository.CreateAsync(book, cancellationToken);
            }

            book.MapFromCommand(command);

            await _booksRepository.SaveChangesAsync(book, cancellationToken);
		}
	}
}
