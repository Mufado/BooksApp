namespace BooksApp.Jobs.Abstractions
{
    public interface IUpsertJob
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
