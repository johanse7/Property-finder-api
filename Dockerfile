FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY PropertyApp.Api/PropertyApp.Api.csproj PropertyApp.Api/
COPY PropertyApp.Application/PropertyApp.Application.csproj PropertyApp.Application/
COPY PropertyApp.Domain/PropertyApp.Domain.csproj PropertyApp.Domain/
COPY PropertyApp.Infrastructure/PropertyApp.Infrastructure.csproj PropertyApp.Infrastructure/
COPY PropertyApp.Tests/PropertyApp.Tests.csproj PropertyApp.Tests/

RUN dotnet restore PropertyApp.Api/PropertyApp.Api.csproj

COPY . .
WORKDIR /src/PropertyApp.Api
RUN dotnet publish -c Release -o /app


FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "PropertyApp.Api.dll"]
