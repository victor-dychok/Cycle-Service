FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AuthorizationMicroservice/Auth.Api/Auth.Api.csproj", "AuthorizationMicroservice/Auth.Api/"]
COPY ["AuthorizationMicroservice/Authorization.Application/Authorization.Application.csproj", "AuthorizationMicroservice/Authorization.Application/"]
COPY ["AuthorizationMicroservice/Authorization.Domain/Authorization.Domain.csproj", "AuthorizationMicroservice/Authorization.Domain/"]
COPY ["AuthorizationMicroservice/Authorization.Percistance/Authorization.Percistance.csproj", "AuthorizationMicroservice/Authorization.Percistance/"]
RUN dotnet restore "./AuthorizationMicroservice/Auth.Api/Auth.Api.csproj"
COPY . .
WORKDIR "/src/AuthorizationMicroservice/Auth.Api"
RUN dotnet build "./Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.Api.dll"]