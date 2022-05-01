Time spent:

ASP.NET:

```
Initial project setup: 1.5 hours
Configuring seed data: 1.5 hours
Data access setup: 1 hour
Defining controllers: 6.5 hours
Background jobs: 1 hours
Testing the API: 3 hours
Deployment: 5 hours

Total 19.5 hours
```

Laravel:

```
Initial project setup: 1.5 hours
Configuring seed data: 0.5 hours
Data access setup: 0.5 hours
Defining controllers: 6 hours
Background jobs: 0 hours
Testing the API: 0 hours
Deployment: 5 hours

Total 13.5 hours
```

API can be experienced at:

[Laravel API](https://laravel.compare.rocks/api/v1/demo)

[ASP.NET API](https://dotnet.compare.rocks/api/v1/demo)

<br>

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
