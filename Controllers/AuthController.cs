using Microsoft.AspNetCore.Mvc;
using SISCOMBUST.Data;
using SISCOMBUST.Models;
using System.Linq;

namespace SISCOMBUST.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            // Si ya hay una sesión activa, redirige al Home
            if (HttpContext.Session.GetString("Usuario") != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        public IActionResult Login(string usuarioLogin, string clave)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == usuarioLogin && u.Password == clave);

            if (user != null)
            {
                // Guardamos la sesión
                HttpContext.Session.SetString("Usuario", user.NombreUsuario);
                HttpContext.Session.SetString("Rol", user.Rol);
                HttpContext.Session.SetInt32("IdUsuario", user.IdUsuario);
                HttpContext.Session.SetString("NombreCompleto", user.NombreCompleto);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View();
        }

        // Cerrar sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
