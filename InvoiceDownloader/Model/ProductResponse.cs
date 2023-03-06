using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class ProductResponse
    {
        public int Total { get; set; }
        public List<Product> Data { get; set; } = new();
    }
}
