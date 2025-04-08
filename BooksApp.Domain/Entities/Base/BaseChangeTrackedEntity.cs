using BooksApp.Domain.Abstractions;

namespace BooksApp.Domain.Entities.Base
{
	public abstract class BaseChangeTrackedEntity<T> : BaseEntity<T>, IChangeTrackedEntity
		where T : struct
	{
		public DateTimeOffset CreationDate { get; private init; } = DateTimeOffset.Now;
		public DateTimeOffset LastModificationDate { get; private init; } = DateTimeOffset.Now;
    }
}
