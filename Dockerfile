FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /storesc
COPY ["src/StorEsc.Api/StorEsc.Api.csproj", "StorEsc.Api/"]
COPY ["src/StorEsc.ApplicationServices/StorEsc.ApplicationServices.csproj", "StorEsc.ApplicationServices/"]
COPY ["src/StorEsc.DomainServices/StorEsc.DomainServices.csproj", "StorEsc.DomainServices/"]
COPY ["src/StorEsc.Domain/StorEsc.Domain.csproj", "StorEsc.Domain/"]
COPY ["src/StorEsc.Infrastructure/StorEsc.Infrastructure.csproj", "StorEsc.Infrastructure/"]
COPY ["src/StorEsc.Infrastructure.ExternalServices/StorEsc.Infrastructure.ExternalServices.csproj", "StorEsc.Infrastructure.ExternalServices/"]
COPY ["src/StorEsc.Core/StorEsc.Core.csproj", "StorEsc.Core/"]
COPY ["src/StorEsc.IoC/StorEsc.IoC.csproj", "StorEsc.IoC/"]
COPY ["src/StorEsc.Tests/StorEsc.Tests.csproj", "StorEsc.Tests/"]

RUN dotnet restore "StorEsc.Api/StorEsc.Api.csproj"
WORKDIR "/storesc/api"
COPY . .
RUN dotnet build "src/StorEsc.Api/StorEsc.Api.csproj" -c Release -o build
RUN dotnet publish "src/StorEsc.Api/StorEsc.Api.csproj" -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /published-app
COPY --from=build-env /storesc/api/publish .
ENTRYPOINT ["dotnet", "StorEsc.Api.dll"]