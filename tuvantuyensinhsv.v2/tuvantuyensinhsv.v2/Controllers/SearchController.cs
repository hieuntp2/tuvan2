using tuvantuyensinhsv.v2.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;

namespace tuvantuyensinhsv.v2.Controllers
{
    public class SearchController : Controller
    {
        ProjectHEntities db = new ProjectHEntities();
        List<string> _dontsearch = new List<string> { "TRƯỜNG", "", "ĐẠI HỌC","ĐẠI HỌ","ĐẠI H", 
            "CAO ĐẲNG","CAO ĐẲN","CAO ĐẲ","CAO Đ",
            "TRUNG CẤP","TRUNG C","TRUNG CẤ",
            "HỌC VIỆN","HỌC V","HỌC VI","HỌC VIỆ",
            null,
            "HỌC NGHỀ","HỌC N","HỌC NG","HỌC NGH",
            "SỸ QUAN","SỸ Q","SỸ QU","SỸ QUA",
            "PHÂN HIỆU","PHÂN H","PHÂN HI","PHÂN HIỆ"};
        //
        // GET: /Search/
        public JsonResult quickSearch(string text)
        {
            List<JsonSearchResult> list = new List<JsonSearchResult>();
            if (ValidationTextSearch(ref text) == null)
            {
                return null;
            }
           
            if (text == null || text == "" || text.Contains("  "))
            {
                list.Add(new JsonSearchResult("", "Không tìm thấy"));
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            
            List<Truong> truongs = db.Truongs.Where(t => t.Ten.Contains(text)).ToList();
            List<Nganh> nganhs = db.Nganhs.Where(t => t.Ten.Contains(text)).ToList();
            List<ThanhPho> thanhphoes = db.ThanhPhoes.Where(t => t.Ten.Contains(text)).ToList();

            for (int i = 0; i < truongs.Count; i++)
            {
                list.Add(new JsonSearchResult(truongs[i].Ten, "/Truongs/Details?ID=" + truongs[i].MaTruong));
            }

            for (int i = 0; i < nganhs.Count; i++)
            {
                list.Add(new JsonSearchResult(nganhs[i].Ten, "/Nganhs/Details?ID=" + nganhs[i].MaNganh));
            }

            for (int i = 0; i < thanhphoes.Count; i++)
            {
                list.Add(new JsonSearchResult(thanhphoes[i].Ten, "/ThanhPhoes/Details?ID=" + thanhphoes[i].ID));
            }

            if (list.Count == 0)
            {
                list.Add(new JsonSearchResult("Không tìm thấy", null));
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Key(string key)
        {
            if (ValidationTextSearch(ref key) == null)
            {
                return HttpNotFound("Bạn cần điền thông tin nhiều hơn");
            }

            System.Diagnostics.Stopwatch objStopWatch = new System.Diagnostics.Stopwatch();
            objStopWatch.Start();
            List<SearchKeyResult> result = new List<SearchKeyResult>();

            string[] arwords = key.Split(' ');
            ApproximateSearchTruong(result, arwords);
            ApproximateSearchNganhs(result, arwords);
            ApproximateSearchTruongNganhs(result, arwords);
            ApproximateSearchThanhPhoes(result, arwords);
            ApproximateSearchBaiViets(result, arwords);

            ViewBag.Key = key;
            objStopWatch.Stop();
            return View(result);
        }

        public ActionResult typekey(string key, string t)
        {
            string back_key = key;
            if (ValidationTextSearch(ref key) == null)
            {
                return HttpNotFound("Bạn cần điền thông tin nhiều hơn");
            }

            List<SearchKeyResult> result = new List<SearchKeyResult>();
            string[] words = key.Split(' ');
            switch (t.ToLower())
            {
                case "t":
                        ApproximateSearchTruong(result, words);                
                    break;
                case "n":
                        ApproximateSearchNganhs(result, words);             
                    break;
                case "p":
                        ApproximateSearchThanhPhoes(result, words);          
                    break;               
            }

            if(result.Count == 0)
            {
                return RedirectToAction("Key", new { key = back_key });
            }

            ViewBag.Key = key;
            return View("Key",result);
        }
            //private void searchTruongNganhs(List<SearchKeyResult> result, string key)
            //{
            //    List<TruongNganh> truongnganhs = db.TruongNganhs.AsNoTracking().Where(t => t.Nganh.Ten.Contains(key)).ToList(); ;

            //    for (int i = 0; i < truongnganhs.Count; i++)
            //    {
            //        //result.Add(new SearchKeyResult { IDBaiViet = baiviets[i].ID, TieuDeBaiViet = baiviets[i].TieuDe });
            //        AddResult(result, truongnganhs[i]);
            //    }
            //}
            //private void searchBaiViets(List<SearchKeyResult> result, string key)
            //{
            //    List<BaiViet> baiviets = db.BaiViets.AsNoTracking().Where(t => t.TieuDe.Contains(key)).ToList();
            //    ViewBag.baiviets = baiviets;
            //    for (int i = 0; i < baiviets.Count; i++)
            //    {
            //        AddResult(result, baiviets[i]);  
            //    }
            //}
            //private void searchThanhPhoes(List<SearchKeyResult> result, string key)
            //{
            //    List<ThanhPho> thanhphoes = db.ThanhPhoes.Where(t => t.Ten.Contains(key)).ToList();
            //    for (int i = 0; i < thanhphoes.Count; i++)
            //    {
            //        AddResult(result, thanhphoes[i]);
            //    }
            //}
            //private void searchNganhs(List<SearchKeyResult> result, string key)
            //{
            //    List<Nganh> nganhs = db.Nganhs.AsNoTracking().Where(t => t.Ten.Contains(key)).ToList();
            //    for (int i = 0; i < nganhs.Count; i++)
            //    {
            //        AddResult(result, nganhs[i]);
            //    }
            //}
            //private void searchTruong(List<SearchKeyResult> result, string key)
            //{
            //    List<Truong> truongs = db.Truongs.AsNoTracking().Where(t => t.Ten.Contains(key)).ToList();
            //    for (int i = 0; i < truongs.Count; i++)
            //    {
            //        AddResult(result, truongs[i]);
            //    }
            //}

            private void ApproximateSearchTruong(List<SearchKeyResult> result, string[] words)
            {
                List<SearchKeyResult> truongs = (from tru in db.Truongs
                                        where words.All(val => tru.Ten.Contains(val))
                                        select new SearchKeyResult()
                                        {
                                            IDThangPho = tru.IDThanhPho,
                                            TinhThanh = tru.ThanhPho.Ten,
                                            MaTruong = tru.MaTruong,
                                            TenTruong = tru.Ten,
                                            MaNganh = "",
                                            TenNganh = "",
                                            Diem = 0,
                                            IDBaiViet = 0,
                                            TieuDeBaiViet = "",
                                            Loai = "T",
                                            linkLogo = (tru.linkLogo == null ? "/Content/university.png" : tru.linkLogo)
                                        }).ToList();

                for (int i = 0; i < truongs.Count; i++)
                {
                    result.Add(truongs[i]);
                }
            }
            private void ApproximateSearchTruongNganhs(List<SearchKeyResult> result, string[] words)
            {
                List<SearchKeyResult> truongnganhs = (from tru in db.TruongNganhs
                                                  where words.All(val => tru.Nganh.Ten.Contains(val))
                                                  select new SearchKeyResult() 
                                                  {
                                                      IDThangPho = tru.Truong.ThanhPho.ID,
                                                      TinhThanh = tru.Truong.ThanhPho.Ten,
                                                      MaTruong = tru.Truong.MaTruong,
                                                      TenTruong = tru.Truong.Ten,
                                                      MaNganh = tru.MaNganh,
                                                      TenNganh = tru.Nganh.Ten,
                                                      Diem = (float)tru.Diem1,
                                                      IDBaiViet = 0,
                                                      TieuDeBaiViet = "",
                                                      Loai = "S",
                                                      linkLogo = (tru.Truong.linkLogo == null ? "/Content/university.png" : tru.Truong.linkLogo)
                                                  }).ToList();                      

                for (int i = 0; i < truongnganhs.Count; i++)
                {                   
                   result.Add(truongnganhs[i]);  
                }
            }
            private void ApproximateSearchBaiViets(List<SearchKeyResult> result, string[] words)
            {
                List<SearchKeyResult> baiviets = (from tru in db.BaiViets
                                          where words.All(val => tru.TieuDe.Contains(val))
                                          select new SearchKeyResult() 
                                          {
                                              IDThangPho = 0,
                                              TinhThanh = "",
                                              MaTruong = "",
                                              TenTruong = "",
                                              MaNganh = "",
                                              TenNganh = "",
                                              Diem = 0,
                                              IDBaiViet = tru.ID,
                                              TieuDeBaiViet = tru.TieuDe,
                                              Loai = "B"
                                          }).ToList();   
                               
                for (int i = 0; i < baiviets.Count; i++)
                {
                     result.Add(baiviets[i]);                   
                }
            }
            private void ApproximateSearchThanhPhoes(List<SearchKeyResult> result, string[] words)
            {
                List<SearchKeyResult> thanhphoes = (from tru in db.ThanhPhoes
                                             where words.All(val => tru.Ten.Contains(val))
                                             select new SearchKeyResult() 
                                             {
                                                 IDThangPho = tru.ID,
                                                 TinhThanh = tru.Ten,
                                                 MaTruong = "",
                                                 TenTruong = "",
                                                 MaNganh = "",
                                                 TenNganh = "",
                                                 Diem = 0,
                                                 IDBaiViet = 0,
                                                 TieuDeBaiViet = "",
                                                 Loai = "P"
                                             }).ToList(); 
                for (int i = 0; i < thanhphoes.Count; i++)
                {
                    result.Add(thanhphoes[i]);                    
                }
            }
            private void ApproximateSearchNganhs(List<SearchKeyResult> result, string[] words)
            {
                List<SearchKeyResult> nganhs = (from tru in db.Nganhs
                                      where words.All(val => tru.Ten.Contains(val))
                                                select new SearchKeyResult() 
                                      {
                                          IDThangPho = 0,
                                          TinhThanh = "",
                                          MaTruong = "",
                                          TenTruong = "",
                                          MaNganh = tru.MaNganh,
                                          TenNganh = tru.Ten,
                                          Diem = 0,
                                          IDBaiViet = 0,
                                          TieuDeBaiViet = "",
                                          Loai = "N"
                                      }).ToList(); 
                for (int i = 0; i < nganhs.Count; i++)
                {
                    result.Add(nganhs[i]);
                }
            }
   

            private string ValidationTextSearch(ref string text)
            {
                // Neu la nganh Y thi pass
                if (text.ToLower() == "y" || text.ToLower() == "dược" || text.ToLower() == "marketing")
                {
                    text = " " + text + " ";
                    return text;
                }

                // Neu so tu < 2 thi fail
                if (CountWord(text) < 2)
                {
                    return null;
                }

                // Neu chua cum tu ko tim kiem thi fail
                if (_dontsearch.Contains(text.ToUpper()))
                {
                    return null;
                }
                
                // Neu so
                if (text.Count() <= 3)
                    return null;

                return text;
            }       

            private int CountWord(string text)
            {
                int c = 0;
                for (int i = 1; i < text.Length; i++)
                {
                    if (char.IsWhiteSpace(text[i - 1]) == true)
                    {
                        if (char.IsLetterOrDigit(text[i]) == true ||
                            char.IsPunctuation(text[i]))
                        {
                            c++;
                        }
                    }
                }
                if (text.Length > 2)
                {
                    c++;
                }
                return c;
            }

            private string firstWord(string text)
            {
                string[] re = text.Split(' ');
                return re[0];
            }
    }
}