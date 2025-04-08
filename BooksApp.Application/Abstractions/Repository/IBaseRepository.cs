using BooksApp.Domain.Abstractions;

namespace BooksApp.Application.Abstractions.Repository
{
	public interface IBaseRepository<TEntity>
		where TEntity : IEntity
	{
	}
}
