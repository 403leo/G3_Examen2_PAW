using Microsoft.AspNetCore.Mvc;
using PAWMartes.Models;
using PAWMartes.Services;
using System.Diagnostics;

namespace PAWMartes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ITrasientServices _transient;
        private readonly IScopedServices _scoped;
        private readonly ISingeltonServices _singelton;



        public HomeController(ILogger<HomeController> logger, ITrasientServices trasient, IScopedServices scoped, ISingeltonServices singleton)
        {
            _logger = logger;
            _transient = trasient;
            _scoped = scoped;
            _singelton = singleton;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ListaServicios([FromServices]ITrasientServices trasient2, [FromServices]IScopedServices scoped2, [FromServices]ISingeltonServices singelton2) {
            ViewData["trasientID"] = _transient.ObtenerID().ToString();
            ViewData["scopedID"] = _scoped.ObtenerID().ToString();
            ViewData["SingeltonID"] = _singelton.ObtenerID().ToString();

			ViewData["trasientID2"] = trasient2.ObtenerID().ToString();
			ViewData["scopedID2"] = scoped2.ObtenerID().ToString();
			ViewData["SingeltonID2"] = singelton2.ObtenerID().ToString();

			return View();

        }
    }
}
