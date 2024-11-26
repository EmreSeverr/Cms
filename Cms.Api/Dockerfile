# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

RUN mkdir -p /app/wwwroot/uploads
RUN chmod -R 777 /app/wwwroot/uploads

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cms.Api/Cms.Api.csproj", "Cms.Api/"]
COPY ["Cms.Data/Cms.Data.csproj", "Cms.Data/"]
COPY ["Cms.Entity/Cms.Entity.csproj", "Cms.Entity/"]
COPY ["Cms.Common/Cms.Common.csproj", "Cms.Common/"]
RUN dotnet restore "./Cms.Api/Cms.Api.csproj"
COPY . .
WORKDIR "/src/Cms.Api"
RUN dotnet build "./Cms.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cms.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cms.Api.dll"]