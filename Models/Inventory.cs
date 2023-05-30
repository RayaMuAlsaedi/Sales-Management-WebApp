using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleInvoicesApp.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string ProductCategory { get; set; } = string.Empty;

        //public int CategoryId { get; set; }

        public double ProductPrice { get; set; }

        //public virtual Invoice Invoice { get; set; }
    }

    public class InventoryDto
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string ProductCategory { get; set; }

        public double ProductPrice { get; set; }

        public List<Inventory> Inventory { get; set;}
    }
}
