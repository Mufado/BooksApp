namespace BooksApp.Domain.DTOs
{
    // Facilitates some mappings since I'm not using any map library or some pattern

    public record BookDetailsDto(
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
        long? RatingsCount);
}
