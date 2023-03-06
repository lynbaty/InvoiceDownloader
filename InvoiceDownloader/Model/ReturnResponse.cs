using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class ReturnResponse
    {
        public int Total { get; set; }
        public List<Return> Data { set; get; } = new();
    }
}
