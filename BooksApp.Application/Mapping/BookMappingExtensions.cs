using BooksApp.Application.Books.Commands;
using BooksApp.Domain.DTOs;
using BooksApp.Domain.Entities;
using BooksApp.Domain.ValueObjects;

namespace BooksApp.Application.Mapping
{
    public static class BookMappingExtensions
    {
        public static Book MapFromCommand(this Book book, UpsertBookCommand command)
        {
            book.GoogleBooksId = command.IsGoogleBookId ? command.Id : null;
            book.Title = command.Title;
            book.SubTitle = command.SubTitle;
            book.PageCount = command.PageCount;
            book.MainCategory = command.MainCategory;
            book.Categories = command.Categories;
            book.Dimensions = new BookDimensions(command.Height, command.Width, command.Thickness);
            book.AverageRating = command.AverageRating;
            book.RatingsCount = command.RatingsCount;

            return book;
        }

        public static void MapFromDto(this Book book, BookDetailsDto dto, string? googleBookId = null)
        {
            book.GoogleBooksId = googleBookId;
            book.Title = dto.Title;
            book.SubTitle = dto.SubTitle;
            book.PageCount = dto.PageCount;
            book.MainCategory = dto.MainCategory;
            book.Categories = dto.Categories;
            book.Dimensions = new BookDimensions(dto.Height, dto.Width, dto.Thickness);
            book.AverageRating = dto.AverageRating;
            book.RatingsCount = dto.RatingsCount;
        }
    }
}