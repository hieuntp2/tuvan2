using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tuvantuyensinhsv.v2.Models.Metadata
{
    public class SearchKeyResult
    {
        public SearchKeyResult()
        {
            this.IDBaiViet = 0;
            this.MaNganh = "";
            this.IDThangPho = 0;
            this.MaTruong = "";
            this.TenNganh = " ";
            this.TenTruong = "";
            this.TieuDeBaiViet = " ";
            this.TinhThanh = " ";
            this.Loai = "";
        }         

        /// <summary>
        /// T: Trường, N: Ngành, S: Trường-Ngành, P: Tỉnh Thành, B: Bài viết
        /// </summary>
        public string Loai { get; set; }
        public int IDThangPho { get; set; }
        public string TinhThanh { get; set; }

        public string MaTruong { get; set; }
        public string TenTruong { get; set; }

        public string MaNganh { get; set; }
        public string TenNganh { get; set; }

        public int IDBaiViet { get; set; }
        public string TieuDeBaiViet { get; set; }

        public float Diem { get; set; }

        public object linkLogo { get; set; }
    }
}