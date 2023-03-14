using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class CustomerDetail
    {
        public int CustomerId { get; set; }
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
    }
}
