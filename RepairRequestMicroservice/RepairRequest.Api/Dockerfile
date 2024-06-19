FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RepairRequestMicroservice/RepairRequest.Api/RepairRequest.Api.csproj", "RepairRequest.Api/"]
COPY ["RepairRequest.Application/RepairRequest.Application.csproj", "RepairRequest.Application/"]
COPY ["RepairRequestMicroservice/RepairRequest.Domain/RepairRequest.Domain.csproj", "RepairRequest.Domain/"]
COPY ["RepairRequestMicroservice/RepairRequest.Percistance/RepairRequest.Percistance.csproj", "RepairRequest.Percistance/"]
RUN dotnet restore "RepairRequestMicroservice/RepairRequest.Api/RepairRequest.Api.csproj"
COPY . .
WORKDIR "/src/RepairRequest.Api"
RUN dotnet build "RepairRequest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RepairRequest.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RepairRequest.Api.dll"]
