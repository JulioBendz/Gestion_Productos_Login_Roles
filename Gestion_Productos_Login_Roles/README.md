# Gestión de Productos, Login y Roles

Proyecto ASP.NET Core MVC para gestionar productos y categorías con autenticación básica por sesión y roles (admin / empleado).

Características
- Listado, creación, edición y eliminación de productos
- Relación Producto ↔ Categoría
- Login simple con roles guardados en sesión ("Rol" = "admin" | "empleado")
  - Empleado: ver y agregar productos
  - Admin: ver, agregar, editar y eliminar
- Persistencia con Entity Framework Core (BDContext)

Stack tecnológico
- .NET 8 (ASP.NET Core MVC)
- Entity Framework Core
- SQL Server (cadena de conexión en appsettings.json)

Instalación y puesta en marcha (Windows)
1. Requisitos
   - .NET 8 SDK instalado
   - SQL Server / LocalDB accesible

2. Configurar la conexión
   - Editar appsettings.json y ajustar la cadena de conexión con la clave `"conexion"`.

3. Migraciones y base de datos
   - Desde Package Manager Console:
     - `Add-Migration Inicial` (si no hay migraciones)
     - `Update-Database`
   - O con la CLI:
     - `dotnet ef migrations add Inicial`
     - `dotnet ef database update`

4. Ejecutar la aplicación
   - `dotnet build`
   - `dotnet run`
   - Abrir en el navegador la URL indicada (por defecto https://localhost:5001 o similar).

Uso rápido
- Registrar/usar usuarios con rol `admin` o `empleado` en la tabla Usuarios.
- Iniciar sesión desde la pantalla de Login; el proyecto guarda en sesión las claves `"Usuario"` y `"Rol"`.
- Ir a Productos:
  - Empleado: puede ver la lista y crear nuevos productos.
  - Admin: además puede editar y eliminar.

Notas y recomendaciones
- Para producción, reemplazar la autenticación por una solución segura (Identity, JWT, OpenID).
- Validaciones de modelo ya definidas (Required, Range). Revisar mensajes si se requieren reglas adicionales.
- Si se agregan campos sensibles, omitirlos del binding o usar viewmodels.
- Si necesitas seeding automático para categorías, puedo añadir un método de inicialización.

Contribuciones
- Abrir issues o PR con mejoras, arreglos o nuevas funcionalidades.

Licencia
- Añadir licencia en LICENSE.md según prefieras (MIT, Apache, etc.).