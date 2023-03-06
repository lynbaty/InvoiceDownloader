using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class InvoiceOrderSurcharges
    {
        public string? SurchargeName { get; set; }
        public long? SurchargeId { get; set; }
        public decimal SurValue { get; set; }
    }
}
