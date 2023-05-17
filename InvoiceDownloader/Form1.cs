using ClosedXML.Excel;
using InvoiceDownloader.Model;
using System.Data;
using System.Text;

namespace InvoiceDownloader
{
    public partial class Form1 : Form
    {
        public List<int> _branchKeys { get; set; } = new();
        public List<Product> _products { get; set; } = new();
        public List<CustomerDetail> _customers { get; set; } = new();
        public List<CustomerDetail> _customersFull { get; set; } = new();

        public List<string> _helperProducts { get; set; } = new();
        public List<CommissionDTO> _commissions { get; set; } = new();
        public List<SaleChannel> _saleChannels { get; set; } = new();


        private readonly ClientService _clientService;
        public Form1()
        {
            _clientService = new ClientService();
            InitializeComponent();
            startTime.Value = DateTime.Now;
            endTime.Value = DateTime.Now;
           
        }

        public async Task ReadCommission()
        {   
            using StreamReader reader = File.OpenText(txtFilePath.Text);
            
                var content = await reader.ReadToEndAsync();
                using var myStream = new MemoryStream(Encoding.UTF8.GetBytes(content));
                _commissions = await CSVConfig.CsvToList<CommissionDTO>(myStream);
          
        }


        #region CheckClick
        public async void check_Click(object sender, EventArgs e)
        {
            
            var token = await _clientService.GetClient(txtSecretKey.Text);
            if (token != null)
            {
                logText1.Text = "Connected successfully to KiotViet server!";
                errorText1.Text = "Loading branches...";
                var branches = await _clientService.prepareValue(token);
                _products = await _clientService.GetProductTree();
                var cus = await _clientService.GetCustomer();
                var customerGroup = cus.Where(c => c.Id == 3242 || c.Id == 3280).ToList();
                customerGroup.ForEach(g => g.customerGroupDetails.ForEach(cd => cd.GroupName = g.Name));
                cus.ForEach(g => g.customerGroupDetails.ForEach(cd => cd.GroupName = g.Name));
                _customers = customerGroup.SelectMany(g => g.customerGroupDetails).ToList();
                _customersFull = cus.SelectMany(g => g.customerGroupDetails).ToList();
                _helperProducts = (await _clientService.GetHelperProducts()).Select(p => p.Code).ToList()!;
                _saleChannels = await _clientService.GetSaleChannel();
                branches.ForEach(b => listView1.Items.Add(new ListViewItem(b.BranchName, b.Id)));
                errorText1.Text = "Loaded branches successfully";
            }    
            else
                logText1.Text = "Connected failure!";
        }
        #endregion

        #region OtherTask
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region exportInvoiceClick
        public async void exportClick(object sender, EventArgs e)
        {
            try
            {
                var start = startTime.Value;
                var end = endTime.Value;
                var invoices = await _clientService.getInvoices(start, end, _branchKeys);
                if(invoices.Count == 0)
                {
                    errorText1.Text = $"Notice: There is have 0 invoice with this filter!";
                    return;
                }    

                var invoicePrintModels = GetPrintModels(invoices);
                invoicePrintModels = UpdateDiscountDetails(invoicePrintModels);
                invoicePrintModels.ForEach(i => 
                {
                    i.CustomerGroup = _customers.FirstOrDefault(c => c.CustomerId.ToString().Equals(i.CustomerId))?.GroupName ?? "Bán Lẻ";
                    i.CustomerGroupFull = _customersFull.FirstOrDefault(c => c.CustomerId.ToString().Equals(i.CustomerId))?.GroupName ?? "Unknown";
                });
                if (_commissions.Count > 0)
                    invoicePrintModels.ForEach(i =>
                    {
                        var cms = _commissions.FirstOrDefault(c => c.ProductCode == i.ProductCode);
                        if(cms != null)
                        {
                            i.BanSiTVBH = cms.BanSiTVBH;
                            i.BanLeTVBH = cms.BanLeTVBH;
                            i.CSKHXemay = cms.CSKHXemay;
                            i.CSKHXetai = cms.CSKHXetai;
                            i.KhachSiXeNgoai = cms.KhachSiXeNgoai;
                            i.KhachLeXeNgoai = cms.KhachLeXeNgoai;
                        }    
                    });

                if (invoicePrintModels != null && invoicePrintModels.Any())
                    ExportExcel(invoicePrintModels);
                errorText1.Text = $"Notice: Export invoices successfully!";
            }
            catch (Exception ex)
            {
                errorText1.Text = $"Error: {ex.Message}. Please restart application and try again!";
            }
        }
        #endregion

        #region GetPrintModel
        private List<InvoicePrintModel> GetPrintModels(List<Model.Invoice> invoices)
        {
            var productIds = _products.Select(p => p.Id).ToList();
            var result = new List<InvoicePrintModel>();
            foreach (var invoice in invoices)
            {
                if (invoice.InvoiceDetails.Any(id => id.ProductCode == "PVC"))
                    invoice.PVC = true;
                foreach(var detail in invoice.InvoiceDetails)
                {
                    var invoiceModel = new InvoicePrintModel
                    {
                        InvoiceCode = invoice.Code,
                        SoldByName = invoice.SoldByName,
                        CustomerId = invoice.CustomerId,
                        CustomerCode = invoice.CustomerCode,
                        CustomerName = invoice.CustomerName,
                        TotalQuantity = detail.Quantity,
                        BasePrice = detail.Price,
                        SubTotal = detail.SubTotal,
                        Discount = detail.Discount,
                        ProductCode = detail.ProductCode,
                        ProductName = detail.ProductName,
                        CreatedDate = invoice.CreatedDate,
                        BranchName = invoice.BranchName,
                        Status = invoice.Status,
                        Delivery = invoice.InvoiceDelivery.Status == 1 ? "NO" : invoice.InvoiceDelivery.DeliveryCode,
                        DeliveryMan = invoice.InvoiceDelivery.PartnerDelivery.Name,
                        ChannelId = invoice.saleChannelId,
                        PVC = invoice.PVC
                    };
                    if (!result.Any(r => r.InvoiceCode == invoiceModel.InvoiceCode))
                    {
                        invoiceModel.TotalDiscount = invoice.Discount ?? 0;
                    }    

                    result.Add(invoiceModel);
                    if(productIds.Contains(detail.ProductId))
                    {
                        var productFomular = _products.FirstOrDefault(p => p.Id == detail.ProductId)?.ProductFormulas;
                        if (productFomular != null && productFomular.Any())
                        {
                            var comboSubtotal = detail.Price * detail.Quantity;
                            var tpSubtotal = productFomular.Sum(p => p.Quantity * detail.Quantity * p.Product?.BasePrice);
                            foreach (var product in productFomular)
                            {
                                var comboDiscount = (tpSubtotal - comboSubtotal) * (detail.Quantity * product.Quantity * product.Product?.BasePrice) / tpSubtotal;
                                var invoiceModel2 = new InvoicePrintModel
                                {
                                    InvoiceCode = invoice.Code,
                                    SoldByName = invoice.SoldByName,
                                    CustomerCode = invoice.CustomerCode,
                                    CustomerId = invoice.CustomerId,
                                    CustomerName = invoice.CustomerName,
                                    BasePrice = product.Product?.BasePrice ?? 0,
                                    TotalQuantity = detail.Quantity * product.Quantity,
                                    ProductCode = product.Product?.Code,
                                    ProductName = product.Product?.FullName,
                                    CreatedDate = invoice.CreatedDate,
                                    BranchName = invoice.BranchName,
                                    Unit = "TP",
                                    Status = invoice.Status,
                                    ComboDiscount = comboDiscount ?? 0,
                                    ComboDiscountRaito = ((detail.Quantity * product.Quantity * product.Product?.BasePrice) / tpSubtotal) ?? 0,
                                    Delivery = invoice.InvoiceDelivery.Status == 1 ? "NO" : invoice.InvoiceDelivery.DeliveryCode,
                                    DeliveryMan = invoice.InvoiceDelivery.PartnerDelivery.Name,
                                    ChannelId = invoice.saleChannelId,
                                    PVC = invoice.PVC
                                };
                                result.Add(invoiceModel2);
                            }
                        }    
                    }
                }    

                if (invoice.invoiceOrderSurcharges.Any())
                {
                    foreach (var sur in invoice.invoiceOrderSurcharges)
                    {
                        var invoiceModel3 = new InvoicePrintModel
                        {
                            InvoiceCode = invoice.Code,
                            SoldByName = invoice.SoldByName,
                            CustomerCode = invoice.CustomerCode,
                            CustomerName = invoice.CustomerName,
                            CustomerId= invoice.CustomerId,
                            ProductName = sur.SurchargeName,
                            Unit = "TK",
                            Surcharge = sur.SurValue,
                            CreatedDate = invoice.CreatedDate,
                            BranchName = invoice.BranchName,
                            Status = invoice.Status,
                            TotalQuantity = 1,
                            ProductCode = sur.SurchargeId.ToString(),
                            Delivery = invoice.InvoiceDelivery.Status == 1 ? "NO" : invoice.InvoiceDelivery.DeliveryCode,
                            DeliveryMan = invoice.InvoiceDelivery.PartnerDelivery.Name,
                            ChannelId = invoice.saleChannelId,
                            PVC = invoice.PVC

                        };
                        result.Add(invoiceModel3);
                    }    
                }    
            } 
            return result;
        }
        #endregion

        #region ItemCheckedEvent
        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if(e.Item.ImageKey != null && e.Item.Checked == true)
                _branchKeys.Add(Convert.ToInt32(e.Item.ImageKey));
            if (e.Item.ImageKey != null && e.Item.Checked != true)
                _branchKeys.Remove(Convert.ToInt32(e.Item.ImageKey));
        }

        #endregion

        #region GenerateHeader

        private DataTable GenerateHeader(string[] chatsLabel)
        {
            DataTable table = new();
            table.TableName = "Báo cáo bán hàng";



            for (int i = 0; i < chatsLabel.Length; i++)
            {
                var item = chatsLabel[i];
                table.Columns.Add(item);
            }
            return table;
        }
        #endregion

        #region UpdateDiscountDetails

        private List<InvoicePrintModel> UpdateDiscountDetails (List<InvoicePrintModel> invoices)
        {
            for (int i = 0; i < invoices.Count; i++)
            {
                if (_helperProducts.Contains(invoices[i].ProductCode!))
                {
                    invoices[i].DiscountDetails = invoices[i].TotalQuantity * invoices[i].Discount;
                }
                if(invoices[i].Unit == "TP")
                {
                    invoices[i].DiscountDetails = invoices[i - 1].DiscountDetails;
                }
                else
                {
                    var invoiceGroup = invoices.Where( x => x.InvoiceCode == invoices[i].InvoiceCode && !_helperProducts.Contains(x.ProductCode!) && x.Unit != "TP" && x.Unit != "TK").ToList();
                    var totalVenue = invoiceGroup.Sum(x => x.TotalQuantity * x.BasePrice);
                    if (totalVenue == 0)
                        continue;
                    var discountProduct = invoiceGroup.Sum(x => x.Discount * x.TotalQuantity);
                    var discountInvoice = invoiceGroup.Sum(x => x.TotalDiscount);

                    invoices[i].DiscountDetails = (invoices[i].TotalQuantity * invoices[i].BasePrice) * (discountProduct + discountInvoice) / totalVenue;
                }    
            }
            return invoices;
        }
        #endregion

        #region InvoiceHandler
        private string InvoiceHandle(int status)
        {
            switch (status)
            {
                case 1:
                    return "Hoàn Thành";
                case 2:
                    return "Đã hủy";
                case 3:
                    return "Đang xử lý";
                default:
                    return "Không giao được"; 
            }
        }

        #endregion

        #region LoadDataToExcelFile

        private void LoadDataToExcelFile(ref DataTable table, List<InvoicePrintModel> invoices)
        {
            for (int i = 0; i < invoices.Count; i++)
            {
                var itemPrev = new InvoicePrintModel();
                var item = invoices[i];
                if (i > 0)
                    itemPrev = invoices[i - 1];

                var validDelivery = new List<string> { "XM", "XT", "XN" };

                table.Columns[13].DataType = typeof(int);
                table.Columns[14].DataType = typeof(decimal);
                table.Columns[15].DataType = typeof(decimal);
                table.Columns[16].DataType = typeof(decimal);
                table.Columns[17].DataType = typeof(decimal);
                table.Columns[18].DataType = typeof(decimal);
                table.Columns[19].DataType = typeof(decimal);
                table.Columns[20].DataType = typeof(decimal);
                table.Columns[21].DataType = typeof(decimal);
                table.Columns[22].DataType = typeof(decimal);

                table.Columns[26].DataType = typeof(decimal);
                table.Columns[27].DataType = typeof(decimal);
                table.Columns[28].DataType = typeof(decimal);
                table.Columns[29].DataType = typeof(decimal);
                table.Columns[30].DataType = typeof(decimal);
                table.Columns[31].DataType = typeof(decimal);
                table.Columns[32].DataType = typeof(decimal);
                table.Columns[33].DataType = typeof(decimal);

                var discountDetail = item.Unit == "TP" ? item.DiscountDetails * item.ComboDiscountRaito : item.DiscountDetails;
                var comboDiscount = item.Unit == "TP" ? item.ComboDiscount : 0;

                table.Rows.Add(item.Unit == "TP" ? "TP" : i + 1, 
                               item.InvoiceCode,
                               item.SoldByName,
                               InvoiceHandle(item.Status),
                               item.BranchName,
                               item.CustomerCode,
                               item.CustomerGroup,
                               item.CustomerGroupFull,
                               item.CustomerName, 
                               item.CreatedDate, 
                               item.ProductCode, 
                               item.ProductName, 
                               item.Unit, 
                               item.TotalQuantity,
                               item.BasePrice,
                               Math.Round(item.TotalQuantity * item.BasePrice),
                               Math.Round(item.Discount),
                               Math.Round(item.Discount * item.TotalQuantity),
                               Math.Round(item.SubTotal),
                               Math.Round(item.TotalDiscount),
                               Math.Round(discountDetail),
                               Math.Round(comboDiscount),
                               item.Unit == "TK" ? Math.Round(item.Surcharge) : Math.Round(item.TotalQuantity * item.BasePrice - discountDetail - comboDiscount),
                               _saleChannels.FirstOrDefault(s => s.Id == item.ChannelId)?.Name ?? string.Empty,
                               validDelivery.Contains(item.Delivery ?? "NO") ? item.DeliveryMan : string.Empty,
                               validDelivery.Contains(item.Delivery ?? "NO") ? item.Delivery : string.Empty,
                               BHCommisionCount(item),
                               VCCommisionCount(item),
                               item.BanSiTVBH,
                               item.BanLeTVBH,
                               item.CSKHXemay,
                               item.CSKHXetai,
                               item.KhachLeXeNgoai,
                               item.KhachSiXeNgoai
                               //Math.Round(item.Surcharge)
                               );
            }
        }
        #endregion

        #region ExportExcel

        private void ExportExcel(List<InvoicePrintModel>? invoices)
        {
            string[] chatsLabel = {
                        "STT",
                        "Mã HĐ",
                        "Người bán",
                        "Tình Trạng",
                        "Cửa Hàng",
                        "Mã KH",
                        "Nhóm KH",
                        "Nhóm KH Full",
                        "Tên KH",
                        "Ngày HĐ",
                        "Mã SP",
                        "Tên SP",
                        "Đơn vị",
                        "SL",
                        "Giá bán",
                        "Thành tiền",
                        "CK Đơn Vị",
                        "Tổng CK",
                        "Doanh thu sau CK",
                        "Giảm giá HĐ",
                        "PB giảm giá",
                        "PB giảm giá Combo",
                        "Doanh thu thuần",
                        "Kênh bán hàng",
                        "Đối tác giao hàng",
                        "Giao hàng",
                        "HH TVBH",
                        "HH CSKH",
                        "BanSiTVBH",
                        "BanLeTVBH",
                        "CSKHXemay",
                        "CSKHXetai",
                        "KhachLeXeNgoai",
                        "KhachSiXeNgoai"
                        //"Thu khác"
                    };
            DataTable table = GenerateHeader(chatsLabel);
            LoadDataToExcelFile(ref table, invoices!);

            var now = DateTime.Now.ToString("yyyyMMddHHmmss");
               
            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(table);
                var column = wb.Worksheets.Worksheet(1).Column(4);

                column.Style.Alignment.WrapText = true;
                wb.Worksheets.Worksheet(1).Columns().AdjustToContents();

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel|*.xlsx";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.ShowDialog();
                wb.SaveAs(saveFileDialog1.OpenFile());
            }
        }
        #endregion

        private void endTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var start = startTime.Value;
                var end = endTime.Value;
                var returns = await _clientService.getReturns(start, end, _branchKeys);
                if (returns.Count == 0)
                {
                    errorText1.Text = $"Notice: There is have 0 return with this filter!";
                    return;
                }

                var returnPrintModels = GetReturnPrintModel(returns);
                //invoicePrintModels = UpdateDiscountDetails(invoicePrintModels);
                if (returnPrintModels != null && returnPrintModels.Any())
                    ExportExcelReturn(returnPrintModels);
                errorText1.Text = $"Notice: Export returns successfully!";
            }
            catch (Exception ex)
            {
                errorText1.Text = $"Error: {ex.Message}. Please restart application and try again!";
            }
        }

        #region GetReturnPrintModel

        private List<ReturnPrintModel> GetReturnPrintModel(List<Model.Return> returns)
        {
            var productIds = _products.Select(p => p.Id).ToList();
            var result = new List<ReturnPrintModel>();
            foreach (var item in returns)
            {
                var totalPrice = item.ReturnDetails.Sum(rd => rd.Price * rd.Quantity);
                foreach (var detail in item.ReturnDetails)
                {
                    var detailDiscount = decimal.Zero;
                    var detailFee = decimal.Zero;
                    if (totalPrice != 0)
                    {
                        var specPrice = detail.Price * detail.Quantity;
                        detailDiscount = item.ReturnDiscount * specPrice / totalPrice;
                        detailFee = item.ReturnFee * specPrice / totalPrice;
                    }    


                    var returnModel = new ReturnPrintModel
                    {
                        ReturnCode = item.Code,
                        CustomerCode = item.CustomerCode,
                        CustomerName = item.CustomerName,
                        TotalQuantity = detail.Quantity,
                        BasePrice = detail.Price,
                        Price = detail.Price,
                        SubTotal = detail.Price * detail.Quantity,
                        ReturnDiscount = detailDiscount,
                        ReturnFee = detailFee,
                        ProductCode = detail.ProductCode,
                        ProductName = detail.ProductName,
                        CreatedDate = item.CreatedDate,
                        BranchName = item.BranchName,
                        Status = item.Status,
                    };
                    //if (!result.Any(r => r.ReturnCode == returnModel.ReturnCode))
                    //{
                    //    returnModel.ReturnDiscount = item.ReturnDiscount;
                    //}

                    result.Add(returnModel);
                    if (productIds.Contains(detail.ProductId))
                    {
                        var productFomular = _products.FirstOrDefault(p => p.Id == detail.ProductId)?.ProductFormulas;
                        if (productFomular != null && productFomular.Any())
                        {
                            var comboSubtotal = detail.Price * detail.Quantity;
                            var tpSubtotal = productFomular.Sum(p => p.Quantity * detail.Quantity * p.Product?.BasePrice);
                            foreach (var product in productFomular)
                            {
                                var comboDiscount = item.ReturnDiscount * (detail.Quantity * product.Quantity * product.Product?.BasePrice) / tpSubtotal;
                                var comboFee = item.ReturnFee * (detail.Quantity * product.Quantity * product.Product?.BasePrice) / tpSubtotal;

                                var invoiceModel2 = new ReturnPrintModel
                                {
                                    ReturnCode = item.Code,
                                    CustomerCode = item.CustomerCode,
                                    CustomerName = item.CustomerName,
                                    BasePrice = product.Product?.BasePrice ?? 0,
                                    Price = product.Product?.BasePrice ?? 0,
                                    TotalQuantity = detail.Quantity * product.Quantity,
                                    ReturnFee = comboFee ?? 0,
                                    ReturnDiscount = comboDiscount ?? 0,
                                    ProductCode = product.Product?.Code,
                                    ProductName = product.Product?.FullName,
                                    CreatedDate = item.CreatedDate,
                                    BranchName = item.BranchName,
                                    Unit = "TP",
                                    Status = item.Status,
                                    ComboDiscount = comboDiscount ?? 0,
                                };
                                result.Add(invoiceModel2);
                            }
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        private void ExportExcelReturn(List<ReturnPrintModel>? returns)
        {
            string[] chatsLabel = {
                        "STT",
                        "Mã HĐ",
                        "Tình Trạng",
                        "Cửa Hàng",
                        "Mã KH",
                        "Tên KH",
                        "Ngày HĐ",
                        "Mã SP",
                        "Tên SP",
                        "Đơn vị",
                        "SL",
                        "Giá bán cơ sở",
                        "Giá trả lại",
                        "Tổng tiền trả hàng",
                        "Giảm giá phiếu trả",
                        "Phí trả hàng",
                        "Hóa đơn đổi hàng",
                        "Cần trả khách",
                        //"Thu khác"
                    };
            DataTable table = GenerateHeader(chatsLabel);
            LoadDataReturnToExcelFile(ref table, returns!);

            var now = DateTime.Now.ToString("yyyyMMddHHmmss");

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(table);
                var column = wb.Worksheets.Worksheet(1).Column(4);

                column.Style.Alignment.WrapText = true;
                wb.Worksheets.Worksheet(1).Columns().AdjustToContents();

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel|*.xlsx";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.ShowDialog();
                wb.SaveAs(saveFileDialog1.OpenFile());
            }
        }

        #region LoadDataToExcelFile

        private void LoadDataReturnToExcelFile(ref DataTable table, List<ReturnPrintModel> returns)
        {
            for (int i = 0; i < returns.Count; i++)
            {
                var itemPrev = new ReturnPrintModel();
                var item = returns[i];
                if (i > 0)
                    itemPrev = returns[i - 1];

                table.Columns[10].DataType = typeof(int);
                table.Columns[11].DataType = typeof(decimal);
                table.Columns[12].DataType = typeof(decimal);
                table.Columns[13].DataType = typeof(decimal);
                table.Columns[14].DataType = typeof(decimal);
                table.Columns[15].DataType = typeof(decimal);
                table.Columns[16].DataType = typeof(decimal);
                table.Columns[17].DataType = typeof(decimal);


                table.Rows.Add(item.Unit == "TP" ? "TP" : i + 1,
                               item.ReturnCode,
                               InvoiceHandle(item.Status),
                               item.BranchName,
                               item.CustomerCode,
                               item.CustomerName,
                               item.CreatedDate,
                               item.ProductCode,
                               item.ProductName,
                               item.Unit,
                               item.TotalQuantity,
                               item.BasePrice,
                               item.Price,
                               Math.Round(item.TotalQuantity * item.Price),
                               Math.Round(item.ReturnDiscount),
                               Math.Round(item.ReturnFee),
                               0,
                               Math.Round((item.TotalQuantity * item.Price) - item.ReturnDiscount - item.ReturnFee)
                               //Math.Round(item.Surcharge)
                               );
            }
        }
        #endregion

        private async void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "csv",
               
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog1.FileName;
            }
            await ReadCommission();
        }

        private decimal BHCommisionCount(InvoicePrintModel item)
        {
            var dt = item.Unit == "TK" ? Math.Round(item.Surcharge/item.TotalQuantity) : Math.Round(item.SubTotal/item.TotalQuantity);
            if (dt < 50000 || item.BanSiTVBH == 0)
                return 0;

            if(item.CustomerGroup == "Bán Sỉ" || item.ChannelId != 0)
            {
                if (dt >= 400000)
                    return item.TotalQuantity * item.BanSiTVBH;
                else
                    return item.TotalQuantity * 6000;
            }else
            {
                if (dt >= 400000)
                    return item.TotalQuantity * item.BanLeTVBH;
                else 
                    return item.TotalQuantity * 12000;
            }    
        }
        private decimal VCCommisionCount(InvoicePrintModel item)
        {
            var dt = item.Unit == "TK" ? Math.Round(item.Surcharge/item.TotalQuantity) : Math.Round(item.SubTotal / item.TotalQuantity);
            if (dt < 50_000)
                return 0;
            
            decimal hhvc = 0;
            switch (item.Delivery)
            {
                case "XM":
                    if (dt < 400_000)
                        hhvc = item.TotalQuantity * 9_000;
                    else
                        hhvc = item.TotalQuantity * item.CSKHXemay;
                    break;
                case "XT":
                    if (dt < 400_000)
                        hhvc = item.TotalQuantity * 9_000;
                    else
                        hhvc = item.TotalQuantity * item.CSKHXetai;
                    break;
                case "XN":
                    if (dt < 400_000)
                        hhvc = item.TotalQuantity * 9_000;
                    else
                        hhvc = item.CustomerGroup == "Bán Sỉ" ? item.TotalQuantity * item.KhachSiXeNgoai : item.TotalQuantity * item.KhachLeXeNgoai;
                    break;
                default:
                    hhvc = 0;
                    break;
            }
            return (item.CustomerGroup == "Bán Sỉ" && item.PVC) ? hhvc * 2 : hhvc;
        }

        private void txtSecretKey_TextChanged(object sender, EventArgs e)
        {

        }
    }
}