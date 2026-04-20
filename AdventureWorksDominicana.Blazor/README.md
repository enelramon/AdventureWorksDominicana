# 🇩🇴 AdventureWorks Dominicana

> Proyecto final de la asignatura **Programacion Aplicada 1 (periodo 1-2026)** impartida en la **Universidad Católica Nordestana (UCNE)** utilizando la base de datos de muestra **AdventureWorks** de Microsoft, implementada con tecnologías modernas de **.NET** y arquitectura en capas.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)
![Blazor](https://img.shields.io/badge/Blazor-Server-7B2FBE?style=flat-square&logo=blazor)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-0095D5?style=flat-square)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?style=flat-square&logo=bootstrap)

---

## 📋 Tabla de Contenidos

- [Descripción](#-descripción)
- [Tecnologías Utilizadas](#-tecnologías-utilizadas)
- [Arquitectura del Proyecto](#-arquitectura-del-proyecto)
- [Base de Datos](#-base-de-datos-adventureworks)
- [Requisitos Previos](#-requisitos-previos)
- [Instalación y Configuración](#-instalación-y-configuración)
- [Guía de Uso](#-guía-de-uso)
- [Funcionalidades](#-funcionalidades)
- [Roles y Permisos](#-roles-y-permisos)
- [Capturas de Pantalla](#-capturas-de-pantalla)
- [Autores y contribuidores](#-autores-y-contribuidores)

---

## 📖 Descripción

**AdventureWorks Dominicana** es una implementación de la base de datos AdventureWorks proporcionada por Microsoft en el desarrollo de un sistema para una empresa de venta de bicicletas y dispositivos para una vida fitness.

La aplicación modela los procesos de negocio de una empresa comercial típica, incluyendo gestión de productos, clientes, pedidos y empleados, con datos y escenarios ajustados a la realidad del mercado dominicano.

---

## 🛠️ Tecnologías Utilizadas

| Capa | Tecnología |
|------|-----------|
| **Frontend** | Blazor Server / HTML / CSS |
| **Backend** | C# / ASP.NET Core |
| **ORM** | Entity Framework Core |
| **Base de Datos** | SQL Server |
| **UI Framework** | Bootstrap 5 |
| **IDE** | Visual Studio 2022 / VS Code |

---

## 🏗️ Arquitectura del Proyecto

El proyecto implementa una **arquitectura en Capas** que separa responsabilidades y facilita el mantenimiento:

```
AdventureWorksDominicana/
├── Identity    (Autenticacion)
│       ↓
├── Api         (Consumo de APIs en el futuro)
│       ↓
├── Blazor      (Razor pages - HTML - CSS)
│       ↓
├── Data        (Base de datos)
│       ↓
├── Services    (CRUDEs)
│       ↓
└── Test        (Pruebas)
```

- **Identity:** Implementa la autenticación manejada por roles para que los usuarios puedan logearse y acceder a ciertos modulos.
- **Api:** Incluye dicho proyecto para abrir la posibilidad de consumo de APIs segun se requiera en el futuro.
- **Blazor:** Es la parte donde se maneja todo el diseño y elementos para la interacción del usuario con el sistema.
- **Data:** Contiene los modelos, migraciones y contexto para una base de datos actualizada y completa.
- **Services:** Maneja todas las operaciones crude de los distintos modelos accediendo directamente a la base de datos.
- **Test:** Reservado para realizar las pruebas de codigo necesarias.

---

## 🗄️ Base de Datos: AdventureWorks

AdventureWorks es una base de datos de muestra proporcionada gratuitamente por Microsoft, diseñada para SQL Server. Simula una empresa ficticia llamada **Adventure Works Cycles** — una compañía fabricante y distribuidora de bicicletas y accesorios deportivos.

### Estructura general

La base de datos contiene alrededor de **70 tablas** organizadas en **5 esquemas principales**, cada uno representando un área funcional del negocio:

| Esquema | Descripción | Tablas clave |
|---------|-------------|--------------|
| **HumanResources** | Gestión de empleados y departamentos | `Employee`, `Department`, `EmployeeDepartmentHistory`, `EmployeePayHistory`, `Shift` |
| **Person** | Información personal y de contacto | `Person`, `Address`, `BusinessEntity`, `EmailAddress`, `PersonPhone`, `StateProvince` |
| **Production** | Catálogo de productos e inventario | `Product`, `ProductCategory`, `ProductModel`, `WorkOrder`, `BillOfMaterials`, `ProductInventory` |
| **Purchasing** | Proveedores y órdenes de compra | `Vendor`, `PurchaseOrderHeader`, `PurchaseOrderDetail`, `ShipMethod` |
| **Sales** | Clientes, ventas y territorios | `Customer`, `SalesOrderHeader`, `SalesOrderDetail`, `SalesTerritory`, `Store`, `SalesPerson` |

### Relaciones principales

La base de datos usa un diseño orientado a herencia donde `Person.BusinessEntity` actúa como supertipo, vinculando empleados, clientes y proveedores para evitar redundancia de datos.

Algunas relaciones importantes:

- Un `Person` puede ser `Employee`, `Customer` o `Vendor` (uno a muchos desde `BusinessEntity`)
- Un `SalesOrderHeader` contiene múltiples `SalesOrderDetail` (cabecera → líneas de pedido)
- Múltiples `Employee` pertenecen a un `Department`
- Un `Product` pertenece a una `ProductCategory` y puede tener múltiples `WorkOrder`

### Adaptación dominicana

En esta implementación, los datos han sido ajustados para reflejar el contexto del mercado dominicano: nombres, direcciones, territorios y moneda adaptados a la República Dominicana.

---

## ✅ Requisitos Previos

Antes de ejecutar el proyecto, asegúrate de tener instalado lo siguiente:

- [.NET SDK 10.0](https://dotnet.microsoft.com/download)
- [SQL Server 2019+](https://www.microsoft.com/es-es/sql-server/sql-server-downloads) (o SQL Server Express)
- [Visual Studio 2022](https://visualstudio.microsoft.com/es/) o [VS Code](https://code.visualstudio.com/) con extensiones de C#
- [Git](https://git-scm.com/)

---

## 🚀 Instalación y Configuración

> Puede acceder y registrarse como un cliente o utilizar las siguientes credenciales de administrador: **(adventureworksdominicana@outlook.com) - (Admin012*)**
> Para realizar la instalacion siga estos pasos:

### 1. Clonar el repositorio

```bash
git clone https://github.com/enelramon/AdventureWorksDominicana.git
cd AdventureWorksDominicana
```

### 2. Configurar la cadena de conexión

Edita el archivo `appsettings.json` y actualiza la cadena de conexión con tus credenciales de SQL Server:

```json
{
  "ConnectionStrings": {
    "SqlConStr": "Server=TU_SERVIDOR;Database=AdventureWorksDominicana;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 3. Aplicar las migraciones

Ejecuta los siguientes comandos para crear la base de datos:

```bash
dotnet ef database update
```

> Si prefieres usar la Consola del Administrador de Paquetes en Visual Studio, ejecuta: `Update-Database`

### 4. Ejecutar la aplicación

```bash
dotnet run
```

---

## 🖥️ Guía de Uso

### Acceso al sistema

Al iniciar la aplicación, el usuario verá la **pantalla de inicio de sesión**. Puede:

- **Registrarse** como nuevo cliente completando el formulario de registro, recibira un correo de confirmacion antes de poder ingresar.
- **Iniciar sesión** con sus credenciales si ya tiene una cuenta.

Según el rol asignado, el usuario tendrá acceso a distintos módulos del sistema (ver sección de Roles).

### Navegación general

La interfaz está organizada en una barra de navegación con acceso a los módulos principales (aquí están pautados desde la vista del administrador):

```
Sales            →  Sección para realizar compras e informaciones de monedas, historiales y clientes.
Shipping         →  Maneja lo relacionado a procesos de envio y metodos.
Products         →  Explorar productos, categorias, ofertas y unidades de medida.
Inventory        →  Gestión de inventario, ubicaciones y tiendas.
Geography        →  Manejo de regiones y provincias.
Human Resources  →  Gestión de personal, departamentos y personas.
Purchasing       →  Sección para manejar proveedores y ordenes de compras.
Configuration    →  Manejo de usuarios, métodos de pago, tipos de número de teléfono y tipos de contacto.

```

### Flujo típico de un pedido

1. El **cliente** inicia sesión y navega a la seccion de ventas para ver el catálogo de productos.
2. Selecciona uno o varios productos y crea un nuevo pedido.
3. El sistema registra el pedido en `SalesOrderHeader` y sus líneas en `SalesOrderDetail`.
4. Un **empleado o administrador** puede ver el pedido, cambiar su estado y hacer seguimiento.

### Gestión de productos

Desde el módulo de productos es posible:
- Ver el catálogo completo con imágenes, descripción y precio.
- Filtrar por categoría (ej. Bicicletas, Teléfonos, dispositivos electrónicos, etc).
- Consultar el inventario disponible por ubicación de almacén.
- Agregar, editar o desactivar productos.

### Gestión de clientes

- Registrar nuevos clientes con información de contacto completa.
- Ver el historial de compras por cliente.
- Segmentar clientes por zona o tipo.


---

## 🔐 Roles y Permisos

El sistema implementa autenticación con control de acceso basado en roles (RBAC). Los roles disponibles son:

| Rol | Descripción | Acceso |
|-----|-------------|--------|
| **Administrador** | Control total del sistema | Todos los módulos|
| **Customer** | Realizar compras | Ventas y métodos de pago|
| **SalesManager** | Manejo de ventas | Módulo de ventas, productos, inventario y clientes|
| **ProductionManager** | Control total del modulo de producción | Incluye productos, categorias, ofertas, unidades de medida y producto por vendedor.|
| **Inventory** | Control de inventario | Inventario, tiendas y ubicaciones|
| **HRManager** | Manejo del personal | Empleados, departamentos, personas, turnos y nóminas|
| **PurchasingManager** | Manejo del compras | Proveedores, métodos de envío y ordenes de compras|


> Para acceder como administrador en un entorno de desarrollo, crea un usuario a través del sistema de Identity y asígnale el rol `Admin` directamente en la base de datos o mediante el seed inicial de datos.

---

## 📸 Capturas de Pantalla

![Login](\AdventureWorksDominicana.Blazor\wwwroot\Capturas\login AWD.png) 
![Home](\AdventureWorksDominicana.Blazor\wwwroot\Capturas\login AWD.png) 
![Nomina](\AdventureWorksDominicana.Blazor\wwwroot\Capturas\PayRoll AWD.png)


---

## 👨‍💻 Autores y contribuidores

### Supervisor:
- **Enel R. Almonte:** [@enelramon](https://github.com/enelramon)

### Proyect Manager:
- **Joel Escaño:** [@joelescano08](https://github.com/joelescano08)

### Otros autores
- **Raydelis A. Hilario:** [@Raydelis06](https://github.com/Raydelis06)
- **Stormy Silverio:** [@StormyUCNE](https://github.com/StormyUCNE)
- **Romanny Hernández:** [@Romannyy](https://github.com/Romannyy)
- **Adenawell E. Valentin:** [@Adenawell](https://github.com/Adenawell)
- **Gianlouis A. Reynoso:** [@Gianlouis47](https://github.com/Gianlouis47)
- **Jose H. García:** [@Tino3q](https://github.com/Tino3q)
- **Luis F. Cortorreal:** [@Faibol27](https://github.com/Faibol27)
- **Jeremy Sanchez:** [@JeremySNL](https://github.com/JeremySNL)
- **Cristian Tavarez:** [@Cristian-Tavarez](https://github.com/Cristian-Tavarez)
- **Juan C. Fermín:** [@juancarlosfermin](https://github.com/juancarlosfermin)

---

<p align="center">
  Hecho por un gran equipo en la República Dominicana 🇩🇴
</p>
