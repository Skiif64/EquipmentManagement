#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/EquipmentManagement.UI/Server/EquipmentManagement.UI.Server.csproj", "src/EquipmentManagement.UI/Server/"]
COPY ["src/EquipmentManagement.UI/Client/EquipmentManagement.UI.Client.csproj", "src/EquipmentManagement.UI/Client/"]
COPY ["src/EquipmentManagement.UI/Shared/EquipmentManagement.UI.Shared.csproj", "src/EquipmentManagement.UI/Shared/"]
RUN dotnet restore "src/EquipmentManagement.UI/Server/EquipmentManagement.UI.Server.csproj"
COPY . .
WORKDIR "/src/src/EquipmentManagement.UI/Server"
RUN dotnet build "EquipmentManagement.UI.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EquipmentManagement.UI.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EquipmentManagement.UI.Server.dll"]