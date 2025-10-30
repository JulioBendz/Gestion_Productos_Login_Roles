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

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            var productos = await _context.Productos.Include(p => p.Categoria).ToListAsync();
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

        // Editar y Eliminar igual con validaciones
    }
}
