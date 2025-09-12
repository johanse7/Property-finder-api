# Imagen base de .NET 8 SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY PropertyApi.sln ./
COPY PropertyApp.Api/PropertyApp.Api.csproj PropertyApp.Api/
COPY PropertyApp.Application/PropertyApp.Application.csproj PropertyApp.Application/
COPY PropertyApp.Domain/PropertyApp.Domain.csproj PropertyApp.Domain/
COPY PropertyApp.Infrastructure/PropertyApp.Infrastructure.csproj PropertyApp.Infrastructure/
COPY PropertyApp.Tests/PropertyApp.Tests.csproj PropertyApp.Tests/

RUN dotnet restore


COPY . .
WORKDIR /src/PropertyApp.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PropertyApp.Api.dll"]
