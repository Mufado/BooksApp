using BooksApp.Domain.Entities.Base;
using BooksApp.Domain.ValueObjects;

namespace BooksApp.Domain.Entities
{
    public class Book : BaseChangeTrackedEntity<Guid>
    {
        public string? GoogleBooksId { get; set; } // I could also create a new entity with Google Books details if wanted
        public string Title { get; set; } = null!;
        public string? SubTitle { get; set; }
        public int PageCount { get; set; }
        public string? MainCategory { get; set; }
        public List<string>? Categories { get; set; }
        public BookDimensions Dimensions { get; set; } = null!;
        public float? AverageRating { get; set; }
        public long? RatingsCount { get; set; }
    }
}
