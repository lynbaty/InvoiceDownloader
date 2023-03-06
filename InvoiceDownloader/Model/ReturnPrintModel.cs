using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class ReturnPrintModel
    {
        public string? ReturnCode { get; set; }
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? BranchName { get; set; }
        public int TotalQuantity { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public decimal BasePrice { get; set; } = 0;
        public decimal SubTotal { get; set; } = 0;
        public decimal ReturnDiscount { get; set; } = 0;
        public decimal ReturnFee { get; set; } = 0;
        public decimal DiscountDetails { get; set; } = 0;
        public decimal ComboDiscount { get; set; } = 0;
        public string? ProductCode { get; set; } 
        public string? ProductName { get; set; } 
        public string? Unit { get; set; } 
        public DateTime? CreatedDate { get; set; }
        public int Status { get; set; }
    }
}
