using InvoiceDownloader.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader
{
    public class ClientService
    {
        public HttpClient? _mainClient { get; set; }
        #region GetClient
        public async Task<string> GetClient(string secretKey)
        {
            var endPoint = $"https://id.kiotviet.vn/connect/token";

            var client = new HttpClient();
            //client.BaseAddress = new Uri(endPoint);



            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("scopes", "PublicApi.Access"));
            nvc.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            nvc.Add(new KeyValuePair<string, string>("client_id", "7416265a-a935-46a2-a97d-76407f2566bc"));
            nvc.Add(new KeyValuePair<string, string>("client_secret", secretKey));
            var req = new HttpRequestMessage(HttpMethod.Post, endPoint) { Content = new FormUrlEncodedContent(nvc) };
            var res = await client.SendAsync(req);

            var bodyContent = await res.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenResponse>(bodyContent);
            return token?.Token!;
        }
        #endregion

        public async Task<List<Branch>> prepareValue(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Retailer", "thegioinem");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            _mainClient = client;
            return await GetBranch();
        }

        private async Task<List<Branch>> GetBranch()
        {
            var endpoint = "https://public.kiotapi.com/branches?pageSize=100";

            var req = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var res = await _mainClient!.SendAsync(req);

            var bodyContent = await res.Content.ReadAsStringAsync();
            var branchResponse= JsonConvert.DeserializeObject<BrandResponse>(bodyContent);
            return branchResponse?.Data!;
        }

        public async Task<List<Model.Invoice>> getInvoices(DateTime start, DateTime end, List<int> branchKeys)
        {
            int? totalPage = 2;
            var result = new List<Model.Invoice>();

            while (totalPage != null && result.Count < totalPage)
            {
                var endpoint = $"https://public.kiotapi.com/invoices?" + $"fromPurchaseDate={start.ToString()}&toPurchaseDate={end.ToString()}&pageSize=100&currentItem={result.Count}";
                if (branchKeys.Any())
                    endpoint = $"https://public.kiotapi.com/invoices?" + $"fromPurchaseDate={start.ToString()}&toPurchaseDate={end.ToString()}&pageSize=100&currentItem={result.Count}&branchIds={string.Join(",",branchKeys)}";

                var req = new HttpRequestMessage(HttpMethod.Get, endpoint);
                var res = await _mainClient!.SendAsync(req);

                var bodyContent = await res.Content.ReadAsStringAsync();
                var invoiceResponse = JsonConvert.DeserializeObject<InvoiceResponse>(bodyContent);
                totalPage = invoiceResponse?.Total;
                if(invoiceResponse?.Data != null)
                    result.AddRange(invoiceResponse?.Data!);
            }

            return result;
        }

        public async Task<List<Model.Return>> getReturns(DateTime start, DateTime end, List<int> branchKeys)
        {
            int? totalPage = 2;
            var result = new List<Model.Return>();

            while (totalPage != null && result.Count < totalPage)
            {
                var endpoint = $"https://public.kiotapi.com/returns?" + $"lastModifiedFrom={start.ToString()}&pageSize=100&currentItem={result.Count}";
                if (branchKeys.Any())
                    endpoint = $"https://public.kiotapi.com/returns?" + $"lastModifiedFrom={start.ToString()}&pageSize=100&currentItem={result.Count}&branchIds={string.Join(",", branchKeys)}";

                var req = new HttpRequestMessage(HttpMethod.Get, endpoint);
                var res = await _mainClient!.SendAsync(req);

                var bodyContent = await res.Content.ReadAsStringAsync();
                var invoiceResponse = JsonConvert.DeserializeObject<ReturnResponse>(bodyContent);
                totalPage = invoiceResponse?.Total;
                if (invoiceResponse?.Data != null)
                    result.AddRange(invoiceResponse?.Data!);
            }

            return result;
        }

        public async Task<List<Product>> GetProductTree()
        {
            int? totalPage = 2;
            var result = new List<Product>();

            while (totalPage != null && result.Count < totalPage)
            {
                var endpoint = $"https://public.kiotapi.com/products?" + $"productType=1&includeMaterial=true&pageSize=100&currentItem={result.Count}";

                var req = new HttpRequestMessage(HttpMethod.Get, endpoint);
                var res = await _mainClient!.SendAsync(req);

                var bodyContent = await res.Content.ReadAsStringAsync();
                var invoiceResponse = JsonConvert.DeserializeObject<ProductResponse>(bodyContent);
                totalPage = invoiceResponse?.Total;
                if (invoiceResponse?.Data != null)
                    result.AddRange(invoiceResponse?.Data!);
            }

            return result;
        }

        public async Task<List<Product>> GetHelperProducts()
        {
            var result = new List<Product>();

            var endpoint = $"https://public.kiotapi.com/products?" + $"productType=3&includeMaterial=true&pageSize=100";

            var req = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var res = await _mainClient!.SendAsync(req);

            var bodyContent = await res.Content.ReadAsStringAsync();
            var invoiceResponse = JsonConvert.DeserializeObject<ProductResponse>(bodyContent);

            return invoiceResponse?.Data!;
        }
    }
}
