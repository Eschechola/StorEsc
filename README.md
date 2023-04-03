# StorEsc
A full e-commerce api built in <strong>"Construindo um ecommerce com .NET + Azure 2023"</strong> course by <strong>Lucas Eschechola!</strong>

<br>

You can contact me <a href="https://linkedin.com/in/lucas-eschechola">here!</a>
<br>
Or send an email to <a href="mailto:lucas.eschechola@outlook.com">lucas.eschechola@outlook.com</a>

<br>

## How to run

<br>

1. Clone the repository
	<br>
	<br>
	```bash
	$ git clone https://github.com/Eschechola/StorEsc.git
	$ cd StorEsc
	$ dotnet clean
	$ dotnet restore
	```
	
<br><br>

2. Create custom database
	<br>
	<br>
	```bash
	$ cd environment/database
	$ docker build -t sqlserver_storesc .
	$ docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YOUR_PASSWORD>" -p 1433:1433 -d sqlserver_storesc
	```

<br><br>

3. Add environment variables
	<br><br>3.1 SQL Server
	```bash
	$ dotnet user-secrets set "Databases:ConnectionStrings:SqlServer" "Server=127.0.0.1;User Id=sa;Password=<YOUR_PASSWORD>;Database=StorEsc" --project src/StorEsc.Api/StorEsc.Api.csproj
	```

	<br>3.2 JWT (8 characters)
	```bash
	$ dotnet user-secrets set "Token:SecretKey" "<YOUR_SECRET_KEY>" --project src/StorEsc.Api/StorEsc.Api.csproj
	```

	<br>3.3 Argon2Id Salt (16 characters)
	```bash
	$ dotnet user-secrets set "Hash:Argon2iD:Salt" "<YOUR_SALT>" --project src/StorEsc.Api/StorEsc.Api.csproj
	```

<br><br>

4. Run migrations to create database
	<br>
	<br>
	```bash
	$ dotnet ef database update --connection "Server=127.0.0.1;User Id=sa;Password=<YOUR_PASSWORD>;Database=StorEsc" --context StorEscContext --project src/StorEsc.Infrastructure/StorEsc.Infrastructure.csproj
	```

<br><br>

5. Run the project!
	<br>
	<br>
	```bash
	$ dotnet run
	```

<br><br>


6. Util Commands - Create Local Migration
	<br>
	<br>
	```bash
	$ dotnet ef migrations add InitialMigration --project src/RagnaLog.Infrastructure.Data/RagnaLog.Infrastructure.Data.csproj
	```

<br><br>


7. Util Commands - Apply Local Migration 
	<br>
	<br>
	```bash
	$ dotnet ef database update --connection "Server=127.0.0.1;Database=RagnaLog;User Id=<YOUR_USER>;Password=<YOUR_PASSWORD>" --context RagnaLogContext --project src/RagnaLog.Infrastructure.Data/RagnaLog.Infrastructure.Data.csproj
	```

<br><br>

<p align="center">2023</p>
