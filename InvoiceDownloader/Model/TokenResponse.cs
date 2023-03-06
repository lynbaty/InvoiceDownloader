using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader.Model
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string? Token { get; set; }
    }
}
