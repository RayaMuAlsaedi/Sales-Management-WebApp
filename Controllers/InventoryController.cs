using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaleInvoicesApp.Data;
using SaleInvoicesApp.Models;

namespace SaleInvoicesApp.Controllers
{
    public class InventoryController : Controller
    {
        private readonly SaleAppDbContext _context;

        public InventoryController(SaleAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            InventoryDto model = new InventoryDto();
            ViewBag.CategoriesList = _context.Categories.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(InventoryDto model)
        {
            model.Inventory = _context.Inventory.Where(m => m.ProductCategory == model.ProductCategory).ToList();
            ViewBag.CategoriesList = _context.Categories.ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            Inventory model = new Inventory();
            ViewBag.CategoriesList = _context.Categories.ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Inventory model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoriesList = _context.Categories.ToList();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var model = await _context.Inventory.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var inv = await _context.Inventory.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (inv != null)
            {
                _context.Inventory.Remove(inv);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}