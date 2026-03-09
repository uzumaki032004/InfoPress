using InfoPress.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using InfoPress.Fabrici;
using InfoPress.Interfaces;

namespace InfoPress.Controllers
{
    public class HomeController : Controller
    {
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
        public IActionResult TestFactory()
        {
            FabricaContinut fabrica = new FabricaGalerie();

            IContinut continut = fabrica.CreeazaContinut();

            continut.Titlu = "Galerie foto de la eveniment";

            continut.Publica();

            return Content("Continut creat folosind Factory Method.");
        }
    }
}
