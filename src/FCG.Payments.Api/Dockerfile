# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

# Install curl for healthcheck
USER root
RUN apt-get update && apt-get install -y curl

USER $APP_UID
WORKDIR /app
EXPOSE 8081

# Add healthcheck
HEALTHCHECK --interval=30s --timeout=10s --start-period=10s --retries=3 \
    CMD curl --fail http://localhost:8081/health || exit 1

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FCG.Payments.Api/FCG.Payments.Api.csproj", "FCG.Payments.Api/"]
COPY ["FCG.Payments.Infra.Ioc/FCG.Payments.Infra.Ioc.csproj", "FCG.Payments.Infra.Ioc/"]
COPY ["FCG.Payments.Domain/FCG.Payments.Domain.csproj", "FCG.Payments.Domain/"]
COPY ["FCG.Payments.Application/FCG.Payments.Application.csproj", "FCG.Payments.Application/"]
COPY ["FCG.Payments.Infra.Data/FCG.Payments.Infra.Data.csproj", "FCG.Payments.Infra.Data/"]
COPY ["FCG.Payments.Infra.Messaging/FCG.Payments.Infra.Messaging.csproj", "FCG.Payments.Infra.Messaging/"]
RUN dotnet restore "./FCG.Payments.Api/FCG.Payments.Api.csproj"
COPY . .
WORKDIR "/src/FCG.Payments.Api"
RUN dotnet build "./FCG.Payments.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FCG.Payments.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FCG.Payments.Api.dll"]