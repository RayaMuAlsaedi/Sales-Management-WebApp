using System.ComponentModel.DataAnnotations.Schema;

namespace SaleInvoicesApp.Models
{
    public class Invoice
    {
        
        public int Id { get; set; }

        //[ForeignKey("Inventory")]
        public int ProductID { get; set; }

        public string InvoiceType { get; set; } = string.Empty;

        public double SalePrice { get; set; }

       // public virtual Inventory Inventory { get; set; }
    }

    public class InvoiceDto
    {
        public int Id { get; set; }

        public int ProductID { get; set; }

        public string InvoiceType { get; set; } = string.Empty;

        public double SalePrice { get; set; }

        public List<Inventory> Products { get; set;}
    }
}
