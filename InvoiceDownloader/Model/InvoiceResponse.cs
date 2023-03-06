using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class InvoiceResponse
    {
        public int Total { get; set; }
        public List<Invoice> Data { set; get; } = new();
    }
}
