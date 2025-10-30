using Gestion_Productos_Login_Roles.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Gestion_Productos_Login_Roles.Controllers
{
    public class LoginController : Controller
    {
        private readonly BDContext _context;

        public LoginController(BDContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string usuario, string clave)
        {
            var user = _context.Usuarios
                .FirstOrDefault(u => u.UsuarioNombre == usuario && u.Clave == clave);

            if (user != null)
            {
                HttpContext.Session.SetString("Usuario", user.UsuarioNombre);
                HttpContext.Session.SetString("Rol", user.Rol);
                return RedirectToAction("Index", "Productos");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
