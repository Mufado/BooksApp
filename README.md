# BooksApp
Clean Architecture + DDD project that saves and displays data from Google Books API.
## Prerequisites

Your machine has to have installed:
- [Node Package Manager (NPM)](https://docs.npmjs.com/downloading-and-installing-node-js-and-npm)
- [.NET Framework](https://dotnet.microsoft.com/en-us/download) (version 10.0 or newer)

> You can choose a package manager other than NPM if you want, but the [setup](#project-setup) can be different

## Project Setup
You should be good to go by just running the `run.cmd` file, located at the root of the project.
> All this batch file does is setup the database before executing all project applications

After successfully running the batch file, you should see:

**Web API**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7161
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5135
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: E:\Desenvolvimento\.NET\BooksApp\BooksApp.WebAPI
```
>[!IMPORTANT]
> The only URL that will be used by the Web project to create requests to the endpoint will be `https://localhost:7161`, so check if it is listed above.

**Worker Service**
```
info: Hangfire.SqlServer.SqlServerObjectsInstaller[0]
      Start installing Hangfire SQL objects...
info: Hangfire.SqlServer.SqlServerObjectsInstaller[0]
      Hangfire SQL objects installed.
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7162
info: Hangfire.BackgroundJobServer[0]
      Starting Hangfire Server using job storage: 'SQL Server: (localdb)\MSSQLLocalDB@BooksApp'
info: Hangfire.BackgroundJobServer[0]
      Using the following options for SQL Server job storage: Queue poll interval: 00:00:00.
info: Hangfire.BackgroundJobServer[0]
      Using the following options for Hangfire Server:
          Worker count: 20
          Listening queues: 'default'
          Shutdown timeout: 00:00:15
          Schedule polling interval: 00:00:15
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: E:\Desenvolvimento\.NET\BooksApp\BooksApp.WorkerService
info: Hangfire.Server.BackgroundServerProcess[0]
      Server desktop-gqe8956:20564:7b80931c successfully announced in 13,6952 ms
info: Hangfire.Server.BackgroundServerProcess[0]
      Server desktop-gqe8956:20564:7b80931c is starting the registered dispatchers: ServerWatchdog, ServerJobCancellationWatcher, ExpirationManager, CountersAggregator, SqlServerHeartbeatProcess, Worker, DelayedJobScheduler, RecurringJobScheduler...
info: Hangfire.Server.BackgroundServerProcess[0]
      Server desktop-gqe8956:20564:7b80931c all the dispatchers started
```

**Web project**
```
[vite] (client) Re-optimizing dependencies because vite config has changed

  VITE v6.2.5  ready in 1653 ms

  ➜  Local:   http://localhost:65125/
  ➜  Network: use --host to expose
  ➜  press h + enter to show help
```
>[!WARNING]
> In case you have problems with the Web project, you may need to manually install the project dependencies.
> To do that, open a CLI of your choice, track it to the `/booksapp.web` directory inside the project and run the command: `npm install` (if you're using NPM).
> After that, you should be able to execute `run.cmd` normally, or `dotnet run --project "booksapp.web/booksapp.web.esproj"` if you want to execute the Web project only (data won't be displayed if you don't run the Web API).

### Applications settings

To ensure the applications are integrated correctly between them and the database, you can check the `appsettings.json` files. There are two setting files, [one](BooksApp.WorkerService/appsettings.json) for the Worker Service and [other](BooksApp.WorkerService/appsettings.json) for the Web API:
```json
// Web API
{
  "ConnectionStrings": {
    "Default": "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=BooksApp;Integrated Security=True;"
  },
  "AllowedOrigins": [
    "http://localhost:65125" // Allows the web project to send requests
  ]
}

// Worker Service
{
  "ConnectionStrings": {
    "Default": "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=BooksApp;Integrated Security=True;"
  },
  "BookApi": {
    "UpsertJobUrl": "https://www.googleapis.com/books/v1/volumes", // API where the books are caming from, in this case Google Books
    "SearchParameters": [ // Search strings that the job will get randomly to create the final URI to the Google Books API
      "Harry", "Potter", "Secrets", "Phoenix",
      "Percy", "Jackson", "Titans", "Curse",
      "Clean", "Code", "Handbook", "Agile", "Software", "Craftsmanship",
      "Clean", "Coder", "Code", "Conduct",
      "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o",
      "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
      "the", "of", "and", "to", "in", "for", "a", "is", "on", "with", "as", "by", "an", "at", "this",
      "food", "sports", "travel", "health", "fitness", "lifestyle", "business", "finance", "technology", "science",
      "Elon", "Musk", "Steve", "Jobs", "Bill", "Gates", "Mark", "Zuckerberg", "Jeff", "Bezos",
      "Pedro", "Pascal", "Dwayne", "Johnson", "Tom", "Holland", "Chris", "Hemsworth", "Robert", "Pattinson"
    ]
  }
}
```

>[!NOTE]
> The job that runs hourly will fetch data from Google Books API by using a random set of strings from `SearchParameters` to search books by title.
> You can add/remove/change those parameters if you want to search different books informations.

<br/>

<sub>_Thank you for checking this out. Have a great life!_<sub/>
