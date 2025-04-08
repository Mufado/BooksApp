using BooksApp.Domain.ValueObjects;

namespace BooksApp.Domain.DTOs
{
    public record GoogleBookVolumeInfoDto(
        string Title,
        string? SubTitle,
        int PageCount,
        string? MainCategory,
        List<string>? Categories,
        BookDimensions? Dimensions,
        float? AverageRating,
        long? RatingsCount);
}
