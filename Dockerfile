# Use the official .NET 9.0 runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the official .NET 9.0 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj file and restore dependencies
COPY ["src/Api/RbbSolucoes.Website.Backend.Api.csproj", "Api/"]
RUN dotnet restore "Api/RbbSolucoes.Website.Backend.Api.csproj"

# Copy all source code
COPY src/ .

# Build the application
WORKDIR "/src/Api"
RUN dotnet build "RbbSolucoes.Website.Backend.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "RbbSolucoes.Website.Backend.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage - runtime image
FROM base AS final
WORKDIR /app

# Copy published application
COPY --from=publish /app/publish .

# Create a non-root user for security
RUN addgroup --system --gid 1001 apiuser && \
    adduser --system --uid 1001 --ingroup apiuser apiuser

# Change ownership of the app directory
RUN chown -R apiuser:apiuser /app
USER apiuser

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "RbbSolucoes.Website.Backend.Api.dll"]