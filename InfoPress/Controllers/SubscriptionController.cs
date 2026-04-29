using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using InfoPress.Models;

namespace InfoPress.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public SubscriptionController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Checkout(string plan)
        {
            ViewBag.Plan = plan;
            ViewBag.Price = plan == "Monthly" ? "120 MDL" : "1200 MDL";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(string cardName, string cardNumber)
        {
            // MOCK PAYMENT PROCESS
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                user.IsPremiumSubscriber = true;
                user.SubscriptionExpiryDate = DateTime.Now.AddMonths(1);
                await _userManager.UpdateAsync(user);
                
                TempData["Message"] = "Abonament activat cu succes! Acum ai acces la conținutul Premium.";
                return RedirectToAction("Index", "News");
            }
            return RedirectToAction("Index");
        }
    }
}
