#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/EquipmentManagement.WebApi/EquipmentManagement.WebApi.csproj", "src/EquipmentManagement.WebApi/"]
RUN dotnet restore "src/EquipmentManagement.WebApi/EquipmentManagement.WebApi.csproj"
COPY . .
WORKDIR "/src/src/EquipmentManagement.WebApi"
RUN dotnet build "EquipmentManagement.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EquipmentManagement.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EquipmentManagement.WebApi.dll"]