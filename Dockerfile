# --- Etapa 1: Compilación (Usa la imagen del SDK completa) ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# Copia los archivos de proyecto y restaura las dependencias primero para aprovechar el cache de Docker
COPY *.sln .
COPY src/PlantillaMicroservicio.Api/*.csproj ./src/PlantillaMicroservicio.Api/
COPY src/PlantillaMicroservicio.Application/*.csproj ./src/PlantillaMicroservicio.Application/
COPY src/PlantillaMicroservicio.Domain/*.csproj ./src/PlantillaMicroservicio.Domain/
COPY src/PlantillaMicroservicio.Infrastructure/*.csproj ./src/PlantillaMicroservicio.Infrastructure/
COPY tests/PlantillaMicroservicio.Application.Tests/*.csproj ./tests/PlantillaMicroservicio.Application.Tests/

RUN dotnet restore

# Copia el resto del código fuente y publica la aplicación
COPY . .
WORKDIR /source/src/PlantillaMicroservicio.Api
RUN dotnet publish -c Release -o /app/publish

# --- Etapa 2: Imagen Final (Usa la imagen de runtime, mucho más ligera) ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Puerto que la aplicación expondrá dentro del contenedor (en .NET 8 es 8080 por defecto)
EXPOSE 8080

# Comando para iniciar la aplicación cuando el contenedor se ejecute
ENTRYPOINT ["dotnet", "PlantillaMicroservicio.Api.dll"]