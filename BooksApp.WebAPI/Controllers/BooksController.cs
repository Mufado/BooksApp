using BooksApp.Application.Books.Commands;
using BooksApp.Application.Books.Queries;
using BooksApp.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BooksApp.WebAPI.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class BooksController(ISender sender) : ControllerBase
	{
		private readonly ISender _sender = sender;

		// The queries shouldn't return any kind of entity type to avoid leaking sensitive information
		// To fix this, I would create a Result class or use some existent from some extension

		[HttpGet("{id}")]
		public async Task<IActionResult> GetBookByIdOrGoogleBookId(string id, CancellationToken cancellationToken)
		{
			var book = await _sender.Send(new GetBookByIdOrGoogleBookIdQuery(id), cancellationToken);

			return Ok(book);
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllBooks(CancellationToken cancellationToken) // I would limit the access to this endpoint in production for performance reasons
        {
            var books = await _sender.Send(new GetAllBooksQuery(), cancellationToken);

            return Ok(books);
        }

        [HttpGet]
        public async Task<IActionResult> SearchBooksPaginated([FromQuery] SearchBooksPaginatedQuery query, CancellationToken cancellationToken)
        {
            var books = await _sender.Send(query, cancellationToken);

			return Ok(books);
        }

        [HttpPut]
		public async Task<IActionResult> UpsertBook(BookDetailsDto request, CancellationToken cancellationToken)
		{
			await _sender.Send(new UpsertBookCommand(request, false), cancellationToken);

			return NoContent(); // Could return the updated/inserted book Id if wanted, though the CQRS design pattern says the opposite
		}
    }
}
