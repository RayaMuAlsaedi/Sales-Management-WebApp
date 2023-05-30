using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SaleInvoicesApp.Models;

namespace SaleInvoicesApp.Controllers
{
    public class WelcomeController : Controller
    {
        //private readonly UserManager<SalesStaff> _userManager;

        //public WelcomeController(UserManager<SalesStaff> userManager)
        //{
        //    _userManager = userManager;
        //}

        public IActionResult Index()
        {
            //var usr = _userManager.FindByEmailAsync(User.Identity.Name).Result;
            
            return View();
            
        }
    }
}
