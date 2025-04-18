using Microsoft.AspNetCore.Mvc;
using PAWMartes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace PAWMartes.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly PAWContext _context;


        public UsuarioController(PAWContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        // GET: Usuario
        public IActionResult Lista()
        {
            var usuarios = _context.Usuario.ToList();
            return View(usuarios);
        }


        [HttpPost]
        public IActionResult Index(string Username, string contraseña)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.Username == Username);

            if (usuario != null)
            {
                var hasher = new PasswordHasher<Usuario>();
                var resultado = hasher.VerifyHashedPassword(usuario, usuario.Contraseña, contraseña);

                if (resultado == PasswordVerificationResult.Success)
                {
                    // Guardar en sesión si las credenciales son correctas
                    HttpContext.Session.SetString("usuario", usuario.Username);
                    HttpContext.Session.SetString("Rol", usuario.Rol); // Guardar el tipo de usuario (Admin, Cliente, etc.)
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Credenciales inválidas";
                }
            }
            else
            {
                ViewBag.Error = "Usuario no encontrado";
            }

            return View();
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Usuario");
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var hasher = new PasswordHasher<Usuario>();
                usuario.Contraseña = hasher.HashPassword(usuario, usuario.Contraseña);
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Usuario");
            }
            return View(usuario);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _context.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Username,Nombre,Correo,Telefono,Contraseña,Rol")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Usuario.Any(u => u.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }
        // GET: Usuario/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }


    }
}
