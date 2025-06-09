# IP Geolocation API

API REST para obtener información geográfica y contextual de direcciones IP.

## 📋 Funcionalidades

Dada una dirección IP, la API proporciona:

- **Información del País**: Nombre y código ISO
- **Idiomas Oficiales**: Lista de idiomas del país
- **Zona Horaria**: Hora(s) actual(es) en el país
- **Distancia**: Distancia estimada desde Buenos Aires en km
- **Moneda**: Moneda local y cotización actual en USD (si está disponible)
- **Estadísticas**: Métricas de uso del servicio (distancia promedio, máxima y mínima)

## 🏗️ Arquitectura

El proyecto implementa **Clean Architecture** con las siguientes capas:

```
├── Domain/                 # Entidades y contratos del dominio
├── Application/            # Casos de uso y lógica de aplicación
├── Infrastructure/         # Acceso a datos y servicios externos
├── WebApi/                # API REST endpoints y Swagger
└── Tests/                 # Tests unitarios y de integración
```

## 🛠️ Tecnologías Utilizadas

- **.NET 8.0**
- **ASP.NET Core Web API**
- **Swagger/OpenAPI** (documentación interactiva)
- **Entity Framework Core**
- **SQL Server**
- **Docker**
- **NUnit** (testing)

A futuro podría agregarse
- **Redis** (caché)
- **AutoMapper**

## 🔌 APIs Externas

- **Geolocalización IP**: `https://ip2country.info/`
- **Información de Países**: `http://restcountries.eu/`
- **Cotizaciones**: `http://fixer.io/`

## 🚀 Instalación y Ejecución

### Prerrequisitos

- .NET 8.0 SDK
- SQL Server (LocalDB o instancia completa)
- Redis (opcional, para caché)
- Docker (para containerización)

### Configuración Local

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/lumelamed/challenge-geolocalizacion-ips.git
   ```

2. **Configurar .env**
Basado en el .env.example

3. **Restaurar dependencias y ejecutar migraciones**
   ```bash
   dotnet restore
   dotnet ef database update --project Infrastructure --startup-project WebApi
   ```

4. **Ejecutar la aplicación**
   ```bash
   dotnet run --project WebApi
   ```

   La API estará disponible en:
   - API: `https://localhost:8080`
   - Swagger UI: `https://localhost:8080/swagger`

### 🐳 Ejecución con Docker

1. **Construir la imagen**
   ```bash
   docker build -t ip-geolocation-api .
   ```

2. **Ejecutar con Docker Compose**
   ```bash
   docker-compose up -d
   ```

Esto levantará:
- La API en el puerto 8080
- Swagger UI en `/swagger`
- SQL Server en el puerto 1433
- Redis en el puerto 6379

## 📖 Uso de la API

Accede a la documentación interactiva en **Swagger UI**: `http://localhost:8080/swagger`

### Endpoints Principales

**Consultar información de IP:**
```bash
GET /api/iplocation/{ip}
```

**Ejemplo de respuesta:**
```json
{
  "isSuccess": true,
  "error": null,
  "data": {
    "ip": "2800:2201:4000:727:2069:b178:5f57:77e1",
    "currentDate": "2025-06-09T06:34:45.032Z",
    "countryName": "Argentina",
    "isoCode": "AR",
    "languages": [
      "es"
    ],
    "currency": "ARS",
    "currentTimes": [
      "UTC−03:00"
    ],
    "exchangeRateToUSD": 1.0631,
    "distanceToBuenosAiresKm": 0,
    "latitude": -34,
    "longitude": -64
  }
}
```

**Obtener estadísticas:**
```bash
GET /api/statistics
```

**Ejemplo de respuesta:**
```json
{
  "isSuccess": true,
  "error": null,
  "data": {
    "maxDistanceCountry": {
      "countryName": "España (ES)",
      "distanceToBuenosAiresKm": 10000,
      "invocationTimes": 1
    },
    "minDistanceCountry": {
      "countryName": "Argentina (AR)",
      "distanceToBuenosAiresKm": 0,
      "invocationTimes": 2
    },
    "averageDistanceInvocations": 3333
  }
}
```

### Swagger UI

La documentación interactiva está disponible en `/swagger` donde se puede:
- Explorar todos los endpoints disponibles
- Probar las APIs directamente desde el navegador
- Ver esquemas de request/response
- Validar parámetros automáticamente

## 🧪 Testing

```bash
# Ejecutar todos los tests
dotnet test
```

## 🤝 Contribución

Este proyecto fue desarrollado como ejercicio técnico. Las mejoras y sugerencias son bienvenidas.

## 📄 Licencia

Este proyecto es de uso educativo y demostrativo.
