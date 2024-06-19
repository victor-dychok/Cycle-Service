FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ServiceCenterMicroservice/ServiceCenter.Api/ServiceCenter.Api.csproj", "ServiceCenter.Api/"]
COPY ["ServiceCenterMicroservice/ServiceCenter.Application/ServiceCenter.Application.csproj", "ServiceCenter.Application/"]
COPY ["ServiceCenterMicroservice/ServiceCenter.Domain/ServiceCenter.Domain.csproj", "ServiceCenter.Domain/"]
COPY ["ServiceCenterMicroservice/ServiceCenter.Percistanse/ServiceCenter.Percistanse.csproj", "ServiceCenter.Percistanse/"]
RUN dotnet restore "ServiceCenterMicroservice/ServiceCenter.Api/ServiceCenter.Api.csproj"
COPY . .
WORKDIR "/src/ServiceCenter.Api"
RUN dotnet build "ServiceCenter.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceCenter.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceCenter.Api.dll"]
