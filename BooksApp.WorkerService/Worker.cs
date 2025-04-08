using BooksApp.Jobs.Abstractions;
using Hangfire;

namespace BooksApp.WorkerService
{
    public class Worker(IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider) : BackgroundService
    {
        private readonly IRecurringJobManager _recurringJobManager = recurringJobManager;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _recurringJobManager.AddOrUpdate("UpsertJob", () => ExecuteUpsertJob(cancellationToken), Cron.Hourly);

            return Task.CompletedTask;
        }

        public async Task ExecuteUpsertJob(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var upsertJob = scope.ServiceProvider.GetRequiredService<IUpsertJob>();

            await upsertJob.RunAsync(cancellationToken);
        }
    }
}
