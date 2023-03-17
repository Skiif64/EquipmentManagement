FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY EquipmentManagement.UI.csproj .
RUN dotnet restore EquipmentManagement.UI.csproj
COPY . .
RUN dotnet build EquipmentManagement.UI.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish EquipmentManagement.UI.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx/nginx.conf /etc/nginx/nginx.conf