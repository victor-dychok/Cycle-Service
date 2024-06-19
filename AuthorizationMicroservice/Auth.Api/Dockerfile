FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AuthorizationMicroservice/Auth.Api/Auth.Api.csproj", "Auth.Api/"]
COPY ["AuthorizationMicroservice/Authorization.Application/Authorization.Application.csproj", "Authorization.Application/"]
COPY ["AuthorizationMicroservice/Authorization.Domain/Authorization.Domain.csproj", "Authorization.Domain/"]
COPY ["AuthorizationMicroservice/Authorization.Percistance/Authorization.Percistance.csproj", "Authorization.Percistance/"]
RUN dotnet restore "AuthorizationMicroservice/Auth.Api/Auth.Api.csproj"
COPY . .
WORKDIR "/src/Auth.Api"
RUN dotnet build "Auth.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.Api.dll"]
