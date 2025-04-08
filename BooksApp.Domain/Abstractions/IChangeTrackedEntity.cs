namespace BooksApp.Domain.Abstractions
{
    public interface IChangeTrackedEntity
    {
        public DateTimeOffset CreationDate { get; }
        public DateTimeOffset LastModificationDate { get; }
    }
}
