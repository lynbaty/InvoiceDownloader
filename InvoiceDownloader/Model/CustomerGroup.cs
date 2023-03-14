using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class CustomerGroup
    {
        public int Id { get; set; }
        public string? Name { get; set; }   
        public List<CustomerDetail> customerGroupDetails { set; get; }
    }   
}
