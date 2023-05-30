namespace SaleInvoicesApp.Models
{
    //public class Reports
    //{
    //    public string InvoiceType { get; set; }

    //    public string Category { get; set; }

    //    //public double MinPrice { get; set; }
    //}

    public class ReportsDto
    {
        public string InvoiceType { get; set; }

        public string Category { get; set; }

        //public double MinPrice { get; set; }

        public List<Inventory> Products { get; set; }
    }
}
