using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class Invoice
    {
        public string? CustomerCode { get; set; }
        public int Status { get; set; } = 1;
        public decimal? Discount { get; set; }
        public string? Code { get; set; }
        public string? CustomerName { get; set; }
        public string? SoldByName { get; set; }
        public string? CustomerId { get; set; }
        public int saleChannelId { get; set; } = 0;
        public string? BranchName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<InvoiceDetail> InvoiceDetails { set; get; } = new();
        public List<InvoiceOrderSurcharges> invoiceOrderSurcharges { set; get; } = new();
        public InvoiceDelivery InvoiceDelivery { set; get; } = new();
        public SaleChannel SaleChannel { set; get; } = new();
        public bool PVC { get; set; } = false;
    }
}
