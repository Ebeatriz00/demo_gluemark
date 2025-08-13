# 📘 GlueMark – Backend API (.NET 8)

> API REST desarrollada con **Clean Architecture** para un ERP modular y escalable llamado GlueMark.  
> Proyecto parte de mi **portafolio profesional**, diseñado para demostrar arquitectura, buenas prácticas y manejo de integraciones.

![.NET 8](https://img.shields.io/badge/.NET-8.0-blue)
![Status](https://img.shields.io/badge/status-WIP-orange)
![License: MIT](https://img.shields.io/badge/License-MIT-yellow)

---
## 🚀 Requisitos e instalación

### Requisitos previos
- .NET 8 SDK
- SQL Server 2019+
- Visual Studio 2022 o VS Code


### Instalación y ejecución
```bash
git clone https://github.com/Ebeatriz00/demo_gluemark.git
cd backend/GlueMark
dotnet restore
dotnet run

## 🏗️ Estructura del Proyecto

```
├── Application               # Casos de uso (CU), DTOs, validaciones, mapeos, lógica de aplicación
│   ├── DTOs                  # Data Transfer Objects para entrada/salida de datos
│   ├── MappingProfiles       # Perfiles de AutoMapper
│   ├── UseCases              # Lógica específica por acción (Create, Update, GetAll, etc.)
│   └── Validators            # Validaciones con FluentValidation
├── Core                     # Entidades y contratos (interfaces)
│   ├── Entities              # Entidades base del dominio
│   └── Interfaces            # Interfaces de acceso a datos
├── DependencyInjection      # Configuraciones para registrar los servicios (IServiceCollection Extensions)
├── GlueMark (API)           # Punto de entrada, controladores, configuración
│   ├── Controllers           # Controladores HTTP
│   ├── Middleware            # Middlewares personalizados
│   └── appsettings.json      # Configuración general del proyecto
├── Infrastructure           # Acceso a datos (Repositories, implementación de interfaces)
│   ├── Persistence           # Repositorios, conexión y ejecución de SPs
│   └── Dependency            # Inyección de dependencias específicas de infraestructura
├── SharedKernel             # Clases comunes y utilitarios reutilizables (como GlobalResponse)
├── Tests                    # Pruebas unitarias y de integración
```

---

## 🔄 Flujo general

1. **Request** llega a un controlador (por ejemplo: `DocumentTypeController`).
2. El controlador invoca un caso de uso (`CreateDocumentType`, `GetAllDocumentTypes`, etc.).
3. El caso de uso valida los datos (FluentValidation) y llama a los repositorios definidos en `Core`.
4. El repositorio se conecta a la base de datos usando `ISqlConnectionFactory`.
5. Se ejecuta un procedimiento almacenado (SP) desde `Infrastructure`.
6. Se utiliza AutoMapper (si aplica) para mapear la entidad a un DTO o viceversa.
7. Se devuelve la respuesta al cliente desde el controlador.

### 🧱 Guía de desarrollo (orden recomendado)

1. Entidad (en `Core.Entities`)
2. Interfaz del repositorio (en `Core.Interfaces`)
3. Implementación del repositorio (en `Infrastructure.Persistence.Repositories`)
4. DTOs (en `Application.DTOs`)
5. Validaciones (en `Application.Validators`)
6. MappingProfile (en `Application.MappingProfiles`)
7. UseCases (en `Application.UseCases`)
8. Inyección de dependencias (en `DependencyInjection`)
9. Controlador (en `GlueMark.Controllers`)

---

## ✏️ Convenciones de nombres

| Tipo              | Convención                             | Ejemplo                        |
| ----------------- | -------------------------------------- | ------------------------------ |
| Clases            | PascalCase                             | `DocumentTypeRepository`       |
| Interfaces        | Prefijo `I` + PascalCase               | `IDocumentTypeRepository`      |
| Métodos           | PascalCase                             | `GetAllAsync`, `ExecuteAsync`  |
| Variables         | camelCase                              | `businessId`, `documentTypeId` |
| DTOs              | Sufijo `Dto`, PascalCase               | `DocumentTypeCreateDto`        |
| Modelos           | Sufijo `Model`, PascalCase             | `DocumentTypeUpdateModel`      |
| Rutas (API)       | PascalCase para controlador y acción | `/api/DocumentType/create`     |
| Stored Procedures | Prefijo `SP_WS_`, SNAKE_CASE           | `SP_WS_LIST_DOCUMENT_TYPE`     |

---

## 🌐 Ejemplos de endpoints

```http
# Crear un tipo de documento
POST /api/DocumentType/DocumentTypeCreate
Body:
{
  "businessId": 1,
  "codeSunat": "01",
  "description": "Factura",
  "usersBy": 100
}

# Obtener todos los tipos de documento para una empresa
GET /api/DocumentType/DocumentTypeList?business_id=1

# Obtener tipo de documento por ID
GET /api/DocumentType/DocumentTypeIdList?documentTypeId=10

# Actualizar tipo de documento
PUT /api/DocumentType/DocumentTypeUpdate
Body:
{
  "businessId": 1,
  "documentTypeId": 10,
  "codeSunat": "01",
  "description": "Factura Electrónica",
  "usersByv": 100
}

# Activar/Inactivar tipo de documento
PATCH /api/DocumentType/DocumentTypeUpdateStatus
Body:
{

  "documentTypeId": 10,
  "businessId": 1,
  "status": "1",
  "usersBy": 100
}
```

---

## 📌 Notas

- Se utiliza AutoMapper para la conversión entre entidades y DTOs (Create/Update/List).
- FluentValidation asegura la validación de reglas de negocio antes de llamar a infraestructura.
- Las operaciones con base de datos se ejecutan vía stored procedures por seguridad y performance.
- La capa `Application` se encarga de la lógica de orquestación y validación, no contiene lógica de acceso a datos directamente.
- `SharedKernel` contiene utilitarios como `GlobalResponse`, usados por todas las capas.

---
## 📌  Nota sobre el alcance

Este proyecto es una demo de portafolio desarrollada de forma personal.
Por lo tanto:

- No incluye todas las funcionalidades de un ERP completo.
- Algunos módulos están simplificados para fines de demostración.
- Las integraciones y reportes pueden variar según el avance y tiempo disponible.
El objetivo principal es mostrar la arquitectura, buenas prácticas y capacidad técnica.

---
## 📄 Licencia

Este proyecto está bajo la licencia MIT – ver [LICENSE](LICENSE) para más detalles.