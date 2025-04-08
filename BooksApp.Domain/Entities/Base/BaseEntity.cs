using BooksApp.Domain.Abstractions;

namespace BooksApp.Domain.Entities.Base
{
	public abstract class BaseEntity<T> : IEntity
		where T : struct // Grants that T is a value type, so it won't be null when exiting the constructor
	{
		protected BaseEntity(T id) => Id = id;

		protected BaseEntity() { }

		public T Id { get; private init; } = typeof(T) == typeof(Guid) ? (T)(object)Guid.NewGuid() : default;
	}
}
