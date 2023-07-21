FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /storesc
COPY ["src/StorEsc.API/StorEsc.API.csproj", "StorEsc.API/"]
COPY ["src/StorEsc.ApplicationServices/StorEsc.ApplicationServices.csproj", "StorEsc.ApplicationServices/"]
COPY ["src/StorEsc.DomainServices/StorEsc.DomainServices.csproj", "StorEsc.DomainServices/"]
COPY ["src/StorEsc.Domain/StorEsc.Domain.csproj", "StorEsc.Domain/"]
COPY ["src/StorEsc.Infrastructure/StorEsc.Infrastructure.csproj", "StorEsc.Infrastructure/"]
COPY ["src/StorEsc.Infrastructure.ExternalServices/StorEsc.Infrastructure.ExternalServices.csproj", "StorEsc.Infrastructure.ExternalServices/"]
COPY ["src/StorEsc.Core/StorEsc.Core.csproj", "StorEsc.Core/"]
COPY ["src/StorEsc.IoC/StorEsc.IoC.csproj", "StorEsc.IoC/"]
COPY ["src/StorEsc.Tests/StorEsc.Tests.csproj", "StorEsc.Tests/"]

RUN dotnet restore "StorEsc.API/StorEsc.API.csproj"
WORKDIR "/storesc/api"
COPY . .
RUN dotnet build "src/StorEsc.API/StorEsc.API.csproj" -c Release -o build
RUN dotnet publish "src/StorEsc.API/StorEsc.API.csproj" -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /published-app
COPY --from=build-env /storesc/api/publish .

EXPOSE 443
EXPOSE 80

ENTRYPOINT ["dotnet", "StorEsc.API.dll"]