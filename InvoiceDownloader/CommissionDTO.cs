using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader
{
    public class CommissionDTO
    {
        [Name("LoaiHang")]
        public string? LoaiHang { get; set; }
        [Name("Brand")]
        public string? Brand { get; set; }
        [Name("ProductCode")]
        public string? ProductCode { get; set; }
        [Name("ProductName")]
        public string? ProductName { get; set; }
        [Name("Price")]
        public decimal Price { get; set; }
        [Name("BanSiTVBH")]
        public decimal BanSiTVBH { get; set; }
        [Name("BanLeTVBH")]
        public decimal BanLeTVBH { get; set; }
        [Name("CSKHXemay")]
        public decimal CSKHXemay { get; set; }
        [Name("CSKHXetai")]
        public decimal CSKHXetai { get; set; }
        [Name("KhachLeXeNgoai")]
        public decimal KhachLeXeNgoai { get; set; }
        [Name("KhachSiXeNgoai")]
        public decimal KhachSiXeNgoai { get; set; }
        [Name("DuanTVBH")]
        public decimal DuanTVBH { get; set; }
        [Name("GiaoNemDuAn")]
        public decimal GiaoNemDuAn { get; set; }
    }
}
