# BooksApp

## Prerequisites

Your machine has to have installed:
- [Node Package Manager(NPM)](https://docs.npmjs.com/downloading-and-installing-node-js-and-npm)
- [.NET Framework](https://dotnet.microsoft.com/en-us/download) (version 10.0 or newer)

> You can choose a package manager other than NPM if you want, but the [setup](#project-setup) can be different

## Project Setup
You should be good to go by just running the `run.cmd` file, located at the root of the project.
> All this batch file does is setup the database before executing all project applications

After executing successfully running the batch file, you should see:

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
> To do that, open a CLI of your choice, track it to the `/booksapp.web` directory inside the project and run the command: `npm install`.
> After that, you should be able to execute `run.cmd` normally, or `dotnet run --project "booksapp.web/booksapp.web.esproj"` if you want to execute the Web project only.

<br/>

<sub>_Thank you for checking this out. Have a great life!_<sub/>
