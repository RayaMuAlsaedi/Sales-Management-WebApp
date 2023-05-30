using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleInvoicesApp.Data;
using SaleInvoicesApp.Models;
using System.Data;
using System.Diagnostics;

namespace SaleInvoicesApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly SaleAppDbContext _context;

        public HomeController(SaleAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<SalesStaff> model = _context.Users.Where(u => u.FirstName.Length > 0).ToList();
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var model = await _context.Users.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            var usr = await _context.Users.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (usr != null)
            {
                _context.Users.Remove(usr);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}