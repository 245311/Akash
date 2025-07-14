# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8000

# Build stage: Uses the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the WBS.csproj file and restore dependencies
COPY ["WBS_API.csproj", "./"]

RUN dotnet restore "./WBS_API.csproj"

# Copy the rest of the source code into the container
COPY . .

# Build the project with the given configuration (Release by default)
WORKDIR "/src"
RUN dotnet build "./WBS_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage: Prepares the app for production
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WBS_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage: Uses the ASP.NET runtime image to run the application in production
FROM base AS final
WORKDIR /app

# Copy the published app from the publish stage into the final image
COPY --from=publish /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "WBS_API.dll"]
