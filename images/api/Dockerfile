FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/StorEsc.Presentation/StorEsc.Presentation.csproj", "StorEsc.Presentation/"]
RUN dotnet restore "src/StorEsc.Presentation/StorEsc.Presentation.csproj"
COPY . .
WORKDIR "/src/StorEsc.Presentation"
RUN dotnet build "StorEsc.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StorEsc.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StorEsc.Presentation.dll"]
