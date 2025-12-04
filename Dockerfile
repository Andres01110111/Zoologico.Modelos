# 1. Usamos la imagen del SDK para construir TODO
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Copiamos TODO el repositorio (API y Modelos)
COPY . .

# 3. Restauramos y publicamos ESPECÍFICAMENTE la API
# Ajusta la ruta si tu carpeta se llama diferente a "ZoologicoAPI"
RUN dotnet restore "ZoologicoAPI/ZoologicoAPI.csproj"
RUN dotnet publish "ZoologicoAPI/ZoologicoAPI.csproj" -c Release -o /app/publish

# 4. Imagen final para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# 5. Configuración de puertos para Render
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# 6. Ejecutar la API
ENTRYPOINT ["dotnet", "ZoologicoAPI.dll"]