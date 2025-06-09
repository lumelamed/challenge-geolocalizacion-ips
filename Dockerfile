# Usar imagen base de .NET 8 SDK para build
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Instalar herramientas necesarias solo en build
RUN apt-get update && apt-get install -y curl iputils-ping dnsutils

# Copiar solution file y archivos de proyecto
COPY *.sln .
COPY src/Domain/*.csproj ./src/Domain/
COPY src/Application/*.csproj ./src/Application/
COPY src/Infrastructure/*.csproj ./src/Infrastructure/
COPY src/WebApi/*.csproj ./src/WebApi/
COPY tests/Tests/*.csproj ./tests/Tests/

# Restaurar dependencias (aprovecha Docker layer caching)
RUN dotnet restore

# Copiar todo el código fuente
COPY . .

# Compilar y publicar la aplicación
WORKDIR /src/src/WebApi
RUN dotnet build WebApi.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish WebApi.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Copiar los archivos publicados
COPY --from=publish /app/publish .

# Exponer el puerto
EXPOSE 80
EXPOSE 443

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "WebApi.dll"]