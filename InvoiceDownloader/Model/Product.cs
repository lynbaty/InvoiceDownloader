using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? FullName { get; set; }
        public decimal? BasePrice { get; set; }
        public string? Unit { get; set; }
        public List<ProductFormula> ProductFormulas { get; set; } = new();
    }
}
