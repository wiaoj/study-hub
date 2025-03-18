#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SocialMedia/Posts.Commands/SocialMedia.Posts.Commands.Api/SocialMedia.Posts.Commands.Api.csproj", "SocialMedia/Posts.Commands/SocialMedia.Posts.Commands.Api/"]
COPY ["CQRS.EventSourcing.Core/CQRS.EventSourcing.Core.csproj", "CQRS.EventSourcing.Core/"]
COPY ["SocialMedia/Posts.Commands/SocialMedia.Posts.Commands.Domain/SocialMedia.Posts.Commands.Domain.csproj", "SocialMedia/Posts.Commands/SocialMedia.Posts.Commands.Domain/"]
COPY ["SocialMedia/Posts.Common/SocialMedia.Posts.Common/SocialMedia.Posts.Common.csproj", "SocialMedia/Posts.Common/SocialMedia.Posts.Common/"]
COPY ["SocialMedia/Posts.Commands/SocialMedia.Posts.Commands.Infrastructure/SocialMedia.Posts.Commands.Infrastructure.csproj", "SocialMedia/Posts.Commands/SocialMedia.Posts.Commands.Infrastructure/"]
RUN dotnet restore "./SocialMedia/Posts.Commands/SocialMedia.Posts.Commands.Api/SocialMedia.Posts.Commands.Api.csproj"
COPY . .
WORKDIR "/src/SocialMedia/Posts.Commands/SocialMedia.Posts.Commands.Api"
RUN dotnet build "./SocialMedia.Posts.Commands.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SocialMedia.Posts.Commands.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialMedia.Posts.Commands.Api.dll"]