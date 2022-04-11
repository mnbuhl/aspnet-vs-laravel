Time spent:

ASP.NET: 1 hours

Laravel: hours

.NET migration commands:

```zsh
dotnet ef migrations add "MigrationName" -s .\Orders.Api\ -p .\Orders.Infrastructure\ (-o .\Data\Migrations)
```

```zsh
dotnet ef database update -s .\Orders.Api\ -p .\Orders.Infrastructure\
```

```zsh
dotnet ef migrations remove -s .\Orders.Api\ -p .\Orders.Infrastructure\
```

Run local PostgreSQL database:

```zsh
docker run --name postgresql -p 5432:5432 -e POSTGRES_PASSWORD=secret -d postgres
```
