using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class SaleChannelResponse
    {
        public List<SaleChannel> Data { get; set; } = new();
    }
}
