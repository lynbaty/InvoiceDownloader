using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class Return
    {
        public string? CustomerCode { get; set; }
        public int Status { get; set; } = 1;
        public decimal ReturnDiscount { get; set; } = decimal.Zero;
        public decimal ReturnFee { get; set; } = decimal.Zero;
        public long? InvoiceId { get; set; }
        public string? Code { get; set; }
        public string? CustomerName { get; set; }
        public string? BranchName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<ReturnDetail> ReturnDetails { set; get; } = new();
    }
}
