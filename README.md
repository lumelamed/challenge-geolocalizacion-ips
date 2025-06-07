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
- **Redis** (caché)
- **AutoMapper**
- **Serilog** (logging estructurado)
- **Docker**
- **NUnit** (testing)

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
  "ip": "83.44.196.93",
  "country": {
    "name": "España",
    "isoCode": "ES"
  },
  "languages": ["Español (es)"],
  "currentTimes": ["21:01:23 (UTC+01:00)"],
  "currency": {
    "code": "EUR",
    "rate": 1.0631
  },
  "distanceFromBuenosAires": 10270,
  "coordinates": {
    "latitude": 40,
    "longitude": -4
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
  "maxDistance": 15420.5,
  "minDistance": 203.1,
  "averageDistance": 5254.3,
  "totalRequests": 150
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

# Con reporte detallado
dotnet test --logger "console;verbosity=detailed"

# Con cobertura
dotnet test --collect:"XPlat Code Coverage"
```

### Estructura de Tests
- **Tests Unitarios**: Casos de uso y lógica de dominio
- **Tests de Integración**: Controllers y servicios externos
- **Mocks**: Para APIs externas y bases de datos

## 📊 Logging

La aplicación utiliza **Serilog** para logging estructurado con:

- **Logs en consola** durante desarrollo
- **Logs en archivos** para producción (rolling files)
- **Información contextual** (IP, tiempo de respuesta, errores)
- **Niveles configurables** (Debug, Info, Warning, Error)

Ejemplo de configuración en `appsettings.json`:
```json
{
  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      { "Name": "Console" },
      { 
        "Name": "File", 
        "Args": { 
          "path": "logs/api-.txt",
          "rollingInterval": "Day"
        }
      }
    ]
  }
}
```

## 📊 Características Técnicas

### Performance
- **Caché en Redis** para información de países (datos estáticos)
- **Connection pooling** para APIs externas
- **Operaciones asíncronas** para mejorar throughput

### Escalabilidad
- **Arquitectura desacoplada** facilita escalar componentes independientemente
- **Estadísticas optimizadas** para soportar alto volumen de consultas
- **Contenedores Docker** para despliegue cloud-native

## 🤝 Contribución

Este proyecto fue desarrollado como ejercicio técnico. Las mejoras y sugerencias son bienvenidas.

## 📄 Licencia

Este proyecto es de uso educativo y demostrativo.
