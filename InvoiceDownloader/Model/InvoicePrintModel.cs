using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class InvoicePrintModel
    {
        public string? InvoiceCode { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerCode { get; set; }
        public string? CustomerGroup { get; set; }
        public string? CustomerName { get; set; }
        public string? BranchName { get; set; }
        public int TotalQuantity { get; set; } = 0;
        public decimal BasePrice { get; set; } = 0;
        public decimal SubTotal { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public decimal TotalDiscount { get; set; } = 0;
        public decimal DiscountDetails { get; set; } = 0;
        public decimal ComboDiscount { get; set; } = 0;
        public decimal ComboDiscountRaito { get; set; } = 0;
        public decimal Surcharge { get; set; } = 0;
        public string? ProductCode { get; set; } 
        public string? ProductName { get; set; } 
        public string? Unit { get; set; } 
        public DateTime? CreatedDate { get; set; }
        public int Status { get; set; }
        public string? Delivery { set; get; }
        public decimal BanSiTVBH { get; set; }
        public decimal BanLeTVBH { get; set; }
        public decimal CSKHXemay { get; set; }
        public decimal CSKHXetai { get; set; }
        public decimal KhachLeXeNgoai { get; set; }
        public decimal KhachSiXeNgoai { get; set; }
    }
}
