#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SocialMedia/Posts.Queries/SocialMedia.Posts.Queries.Api/SocialMedia.Posts.Queries.Api.csproj", "SocialMedia/Posts.Queries/SocialMedia.Posts.Queries.Api/"]
COPY ["CQRS.EventSourcing.Core/CQRS.EventSourcing.Core.csproj", "CQRS.EventSourcing.Core/"]
COPY ["SocialMedia/Posts.Queries/SocialMedia.Posts.Queries.Domain/SocialMedia.Posts.Queries.Domain.csproj", "SocialMedia/Posts.Queries/SocialMedia.Posts.Queries.Domain/"]
COPY ["SocialMedia/Posts.Queries/SocialMedia.Posts.Queries.Infrastructure/SocialMedia.Posts.Queries.Infrastructure.csproj", "SocialMedia/Posts.Queries/SocialMedia.Posts.Queries.Infrastructure/"]
RUN dotnet restore "./SocialMedia/Posts.Queries/SocialMedia.Posts.Queries.Api/SocialMedia.Posts.Queries.Api.csproj"
COPY . .
WORKDIR "/src/SocialMedia/Posts.Queries/SocialMedia.Posts.Queries.Api"
RUN dotnet build "./SocialMedia.Posts.Queries.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SocialMedia.Posts.Queries.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialMedia.Posts.Queries.Api.dll"]