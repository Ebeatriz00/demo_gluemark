# ERP Modular – Etiquetas Adhesivas (Demo Gluemark)

> Proyecto en construcción de un **ERP modular y escalable** para el sector de etiquetas adhesivas.  
> Arquitectura multiempresa, backend en **C# .NET 8** (Clean Architecture) y frontend en **React** (en desarrollo).

![.NET 8](https://img.shields.io/badge/.NET-8.0-blue) ![Status](https://img.shields.io/badge/status-WIP-orange)

---

# 📦 Estructura del Repositorio
```
├── backend/ # API REST en C# .NET 8 (Clean Architecture)
│ └── README.md # Guía de instalación y uso del backend
├── frontend/ # Interfaz en React (WIP)
│ └── README.md # Guía de instalación y uso del frontend
└── docs/ # Diagramas, documentación y notas técnicas
```
---

## 🚀 Descripción General

Este ERP está diseñado para:

- Gestionar **producción**: formulación, plan de corte, órdenes de trabajo, trazabilidad por lote/bobina.  
- Gestionar **ventas**: cotizaciones, pedidos, facturación electrónica (conector **SUNAT**) e integración con ERP externos.  
- Administrar **inventario**: entradas/salidas, mermas, stock mínimo, rotación y control por ubicación.  
- **Mantenimiento y vida útil** de máquinas: registro de incidencias, calendario de mantenimiento preventivo/correctivo, indicadores MTBF/MTTR.  
- **Analítica de planta**: KPIs (rendimiento, scrap, OEE básico), tableros y reportes exportables.  

### 🧱 Arquitectura
- **Backend:** C# .NET 8, Clean Architecture, SQL Server, AutoMapper, FluentValidation (WIP).  
- **Frontend:** React, Vite, TailwindCSS (WIP).  
- **Integraciones:** Compatibilidad con múltiples sistemas externos (ERP, contabilidad, facturación electrónica, etc.), SUNAT.

---

## 📌 Nota sobre el alcance

Este proyecto es una **demo de portafolio** desarrollada de forma personal.  
Por lo tanto:  
- No incluye todas las funcionalidades que tendría un ERP completo en producción.  
- Algunos módulos están **simplificados** para fines de demostración.  
- Las integraciones, reportes y flujos pueden variar según el avance y tiempo disponible.  

El objetivo principal es mostrar la **arquitectura, buenas prácticas y capacidad técnica**, no ofrecer un producto terminado listo para producción.

---

## 📂 Documentación por módulo

- [Backend API – .NET 8](Gluemark.Api/README.md)  
- [Frontend – React](frontend/README.md)

---

## 💼 Enfoque para Portafolio
Este proyecto forma parte de mi **portafolio profesional** y tiene como objetivos:  
- Mostrar dominio de **arquitectura limpia** en .NET 8.  
- Evidenciar experiencia en **desarrollo de APIs escalables y seguras**.  
- Demostrar capacidad para **integrar sistemas empresariales** y manejar lógica de negocio compleja.  
- Aplicar buenas prácticas de **versionado, documentación y modularidad**.

---

## 🛣️ Roadmap general
- [ ] Módulo de usuarios y roles
- [ ] Gestión de producción y órdenes de trabajo
- [ ] Integración con sistemas ERP externos
- [ ] Panel de reportes y KPIs
- [ ] Despliegue en contenedores (Docker)

---

## 📄 Licencia
Este proyecto está bajo la licencia MIT – ver [LICENSE](LICENSE) para más detalles.


