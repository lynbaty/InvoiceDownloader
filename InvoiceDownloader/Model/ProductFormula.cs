using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class ProductFormula
    {
        public int Quantity { get; set; }
        public Product? Product { get; set; }
    }
}
