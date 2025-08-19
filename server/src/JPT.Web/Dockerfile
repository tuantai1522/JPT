# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Certificates
COPY ["src/JPT.Web/Certificates/aspnetapp.pfx", "/app/certificates/aspnetapp.pfx"]


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/JPT.Web/JPT.Web.csproj", "src/JPT.Web/"]
COPY ["src/JPT.Infrastructure/JPT.Infrastructure.csproj", "src/JPT.Infrastructure/"]
COPY ["src/JPT.Core/JPT.Core.csproj", "src/JPT.Core/"]
COPY ["src/JPT.UseCases/JPT.UseCases.csproj", "src/JPT.UseCases/"]
RUN dotnet restore "./src/JPT.Web/JPT.Web.csproj"
COPY . .
WORKDIR "/src/src/JPT.Web"
RUN dotnet build "./JPT.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./JPT.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JPT.Web.dll"]