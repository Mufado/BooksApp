
using BooksApp.Domain.ValueObjects.Base;

namespace BooksApp.Domain.ValueObjects
{
    // Funny fact:
    // Google Books documentation comments about these properties in their data,
    // but I couldn't find a single example of a book with these properties filled,
	// so you probably won't see this being really useful... Sorry haha.
    public class BookDimensions : BaseValueObject
	{
		public float? Height { get; private set; }
		public float? Width { get; private set; }
		public float? Thickness { get; private set; }

		public BookDimensions() { }

		public BookDimensions(float? height, float? width, float? thickness)
		{
			Height = height;
			Width = width;
			Thickness = thickness;
		}

		protected override IEnumerable<object?> GetEqualityComponents()
		{
			yield return Height;
			yield return Width;
			yield return Thickness;
		}
	}
}
