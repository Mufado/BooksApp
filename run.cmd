dotnet ef database update --project ./BooksApp.Persistence/ --startup-project ./BooksApp.WebAPI/BooksApp.WebAPI.csproj
start dotnet run --project "BooksApp.WebAPI/BooksApp.WebAPI.csproj"
start dotnet run --project "booksapp.web/booksapp.web.esproj"
start dotnet run --project "BooksApp.WorkerService/BooksApp.WorkerService.csproj"