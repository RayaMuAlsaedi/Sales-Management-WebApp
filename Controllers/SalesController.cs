using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleInvoicesApp.Data;
using SaleInvoicesApp.Models;
using static SaleInvoicesApp.Models.Enums;

namespace SaleInvoicesApp.Controllers
{
    [Authorize(Roles = "Admin, Cashier")]//Only cashier can access this
    public class SalesController : Controller
    {
        private readonly SaleAppDbContext _context;

        public SalesController(SaleAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            InvoiceDto model = new InvoiceDto();
            model.Products = _context.Inventory.Where(p => p.ProductName != string.Empty).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(InvoiceDto model)
        {
            
                Invoice invoice = new Invoice();
                invoice.ProductID = model.ProductID;
                invoice.SalePrice = model.SalePrice;
                invoice.InvoiceType= model.InvoiceType;

                //_context.Invoices.Add(invoice);
                _context.Add(invoice);
                _context.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}
