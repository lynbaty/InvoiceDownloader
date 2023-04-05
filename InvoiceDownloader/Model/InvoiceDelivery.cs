using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class InvoiceDelivery
    {
        public int Status { get; set; } = 1;
        public string? DeliveryCode { get; set; }
        public PartnerDelivery PartnerDelivery { get; set; } = new();
    }
}
