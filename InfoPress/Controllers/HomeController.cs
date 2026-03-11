using InfoPress.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using InfoPress.Fabrici;
using InfoPress.Interfaces;
using InfoPress.Builder;
using InfoPress.Models;
using InfoPress.Singleton;
using InfoPress.Adapter;
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
        public IActionResult TestBuilder()
        {
            ArticolBuilder builder = new ArticolBuilder();

            DirectorArticol director = new DirectorArticol();

            director.ConstruiesteArticolStire(builder);

            Articol articol = builder.GetArticol();

            return Content("Articol creat cu Builder: " + articol.Titlu);
        }
        public IActionResult TestPrototype()
        {
            ArticolPrototype articolOriginal = new ArticolPrototype();

            articolOriginal.Titlu = "Știre originală";
            articolOriginal.Autor = "Reporter";

            ArticolPrototype copie = articolOriginal.Clone();

            copie.Titlu = "Știre copiată";

            return Content("Articol clonat: " + copie.Titlu);
        }
        public IActionResult TestSingleton()
        {
            ManagerConfigurare config1 = ManagerConfigurare.GetInstance();

            ManagerConfigurare config2 = ManagerConfigurare.GetInstance();

            bool aceeasiInstanta = config1 == config2;

            return Content("Singleton funcționează: " + aceeasiInstanta);
        }
        public IActionResult TestAdapter()
        {
            IImagineService imagineService = new ImagineAdapter();

            imagineService.IncarcaImagine("stire.jpg");

            return Content("Imagine încărcată folosind Adapter.");
        }
    }
}
