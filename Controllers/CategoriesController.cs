using Microsoft.AspNetCore.Mvc;
using SaleInvoicesApp.Data;
using SaleInvoicesApp.Models;

namespace SaleInvoicesApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly SaleAppDbContext _context;

        public CategoriesController(SaleAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> model = _context.Categories.ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if(ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
