using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Gestion_Productos_Login_Roles.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔹 1. Agregar servicio para MVC
builder.Services.AddControllersWithViews();

// 🔹 2. Agregar el contexto de base de datos (usa el nombre de tu clase BDContext)
builder.Services.AddDbContext<BDContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

// 🔹 3. Agregar soporte para sesiones (login)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // tiempo de expiración de sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// 🔹 4. Configurar el pipeline de la app
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔹 5. Activar sesiones antes de la autorización
app.UseSession();

app.UseAuthorization();

// 🔹 6. Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}"); // 👉 inicia desde el login

app.Run();