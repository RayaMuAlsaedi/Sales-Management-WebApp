using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleInvoicesApp.Data;
using SaleInvoicesApp.Models;
using static SaleInvoicesApp.Models.Enums;

namespace SaleInvoicesApp.Controllers
{
    [Authorize(Roles = "Admin,Cashier")]//Only Admin and Cashier can see reports
    public class ReportsController : Controller
    {
        private readonly SaleAppDbContext _context;

        public ReportsController(SaleAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ReportsDto model = new ReportsDto();
            ViewBag.CategoriesList = _context.Categories.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ReportsDto model)
        {
            
            if (model.InvoiceType != "All")
            {
               var  query = from invent in _context.Inventory
                            join invoice in _context.Invoices
                            on invent.Id equals invoice.ProductID
                            where invent.ProductCategory == model.Category
                            && invoice.InvoiceType == model.InvoiceType
                            select invent;
                model.Products = query.ToList();
            }
            else
            {
               var  query = from invent in _context.Inventory
                            join invoice in _context.Invoices
                            on invent.Id equals invoice.ProductID
                            where invent.ProductCategory == model.Category
                            //&& invoice.InvoiceType == model.InvoiceType
                            select invent;
                model.Products = query.ToList();
            }
            ViewBag.CategoriesList = _context.Categories.ToList();
            return View(model);
        }
    }
}
