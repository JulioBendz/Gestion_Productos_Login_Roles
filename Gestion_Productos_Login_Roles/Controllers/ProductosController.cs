using Gestion_Productos_Login_Roles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Productos_Login_Roles.Controllers
{
    public class ProductosController : Controller
    {
        private readonly BDContext _context;

        public ProductosController(BDContext context)
        {
            _context = context;
        }

        // helper simple para roles en sesión (espera "Rol" = "admin" o "empleado")
        private bool IsAdmin() =>
            HttpContext.Session.GetString("Rol")?.ToLower() == "admin";

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            var productos = await _context.Productos.Include(p => p.Categoria).ToListAsync();

            // enviar a la vista si es admin para mostrar botones
            ViewBag.EsAdmin = IsAdmin();

            return View(productos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ModelStateErrors"] = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                ViewBag.Categorias = _context.Categorias.ToList();
                return View(producto);
            }

            try
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["SaveError"] = ex.Message;
                ViewBag.Categorias = _context.Categorias.ToList();
                return View(producto);
            }
        }

        // Edit - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            if (!IsAdmin()) // sólo admin puede editar
                return RedirectToAction(nameof(Index));

            if (id == null) return NotFound();

            var producto = await _context.Productos.FindAsync(id.Value);
            if (producto == null) return NotFound();

            ViewBag.Categorias = _context.Categorias.ToList();
            return View(producto);
        }

        // Edit - POST (reemplazado: obtener la entidad, asignar campos y guardar)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            if (!IsAdmin()) // sólo admin puede editar
                return RedirectToAction(nameof(Index));

            if (id != producto.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = _context.Categorias.ToList();
                return View(producto);
            }

            var entidad = await _context.Productos.FindAsync(id);
            if (entidad == null) return NotFound();

            // Asignar sólo las propiedades que queremos actualizar
            entidad.Nombre = producto.Nombre;
            entidad.Precio = producto.Precio;
            entidad.Stock = producto.Stock;
            entidad.IdCategoria = producto.IdCategoria;

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Productos.Any(e => e.Id == producto.Id))
                    return NotFound();
                throw;
            }
        }

        // Delete - POST (se invoca desde Index mediante formulario)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            if (!IsAdmin()) // sólo admin puede eliminar
                return RedirectToAction(nameof(Index));

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                TempData["SaveError"] = "Producto no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["SaveError"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
