using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;
using tuvantuyensinhsv.v2.Models.Metadata;

namespace tuvantuyensinhsv.v2.Controllers
{
    /// <summary>
    /// Phân loại:
    /// ID: Mã đối tượng
    /// Loại: Loại đối tượng
    ///     T: Trường
    ///     N: Ngành
    ///     TN: Trường-Ngành
    ///     P: Tỉnh thành
    /// </summary>
    public class TabOject
    {
        public string ID { get; set; }
        public string Loai { get; set; }
        public string Ten { get; set; }
        public TabOject(string ID, string Loai, string Ten)
        {
            this.ID = ID;
            this.Loai = Loai;
            this.Ten = Ten;
        }

        public TabOject() {
            this.ID = "";
        }
    }

    public class TabEngineServerController : Controller
    {
        private ProjectHEntities db = new ProjectHEntities();

        public JsonResult getTruong(string id)
        {
            TabOject tab = new TabOject();

            Truong truong = db.Truongs.SingleOrDefault(t => t.MaTruong == id);
            if (truong == null)
                return null;
            tab.ID = truong.MaTruong;
            tab.Ten = truong.Ten;

            // do ko co nen gan tam vao day
            tab.Loai = truong.linkLogo;

            return Json(tab, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getNganh(string idtruong)
        {
            List<TabOject> result = new List<TabOject>();

            if (idtruong == null || idtruong == "")
                return Json(result, JsonRequestBehavior.AllowGet);
            List<TruongNganh> nganh = db.TruongNganhs.Where(t => t.MaTruong == idtruong).ToList();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //HashSet<TabOject> hashresult = new HashSet<TabOject>();
            for(int i = 0; i < nganh.Count; i++)
            {                

                if(dic.ContainsKey(nganh[i].MaNganh))
                {
                    continue;
                }
                else
                {
                    TabOject tab = new TabOject();
                    tab.ID = nganh[i].MaNganh;
                    tab.Ten = nganh[i].Nganh.Ten;

                    dic.Add(tab.ID, tab.Ten);
                    result.Add(tab);
                }  
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getTagList(int IDBaiViet) 
        {
            List<TabOject> list = new List<TabOject>();

            BaiViet baiviet = db.BaiViets.SingleOrDefault(t => t.ID == IDBaiViet);
            if (baiviet == null)
                return null;

            if (baiviet.Tabs == null || baiviet.Tabs == "")
            {
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            list = Encode(baiviet.Tabs);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getTagListMBTI(string idmbti)
        {
            List<TabOject> list = new List<TabOject>();

            MBTI mbti = db.MBTIs.SingleOrDefault(t => t.idMBTI == idmbti);
            if (mbti == null)
                return null;

            if (mbti.Tags == null || mbti.Tags == "")
            {
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            list = Encode(mbti.Tags);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public string Code(string ID, Char Loai)
        {
            string result = ID.Insert(0, Loai.ToString());
            return result;
        }

        public List<TabOject> Encode(string Ma)
        {
            List<TabOject> result = new List<TabOject>();

            if(Ma == null || Ma == "")
                return result;

            List<string> list = Ma.Split(',').ToList();

            foreach(string s in list)
            {
                TabOject tag = new TabOject();
                Char key = s[0];

                string ID = "";
                for(int i = 1; i < s.Length; i++)
                {
                    ID += s[i];
                }               

                switch(key)
                {
                    //Ngành
                    case 'N':
                        Nganh nganh = db.Nganhs.SingleOrDefault(t => t.MaNganh == ID);
                        if(nganh != null)
                        {
                            tag.ID = nganh.MaNganh;
                            tag.Ten = nganh.Ten;
                            tag.Loai = "N";
                        }
                        
                        break;

                    // Trường
                    case 'T':
                        Truong truong = db.Truongs.SingleOrDefault(t => t.MaTruong == ID);
                        if(truong != null)
                        {
                            tag.ID = truong.MaTruong;
                            tag.Loai = "T";
                            tag.Ten = truong.Ten;
                        }
                        
                        break;

                    //Thành phố
                    case 'P':
                        int id = Int16.Parse(ID);
                        ThanhPho tinh = db.ThanhPhoes.SingleOrDefault(t => t.ID == id);
                        if(tinh != null)
                        {
                            tag.ID = ID;
                            tag.Loai = "P";
                            tag.Ten = tinh.Ten;
                        }
                        break;
                }
                if(tag.ID != "")
                {
                    result.Add(tag);
                }                
            }

            return result;
        }

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
            if (ValidationTextSearch(ref text) == null)
            {
                return null;
            }
            List<TabOject> list = new List<TabOject>();
            if (text == null || text == "" || text.Contains("  "))
            {
                list.Add(new TabOject("", "", "Không tìm thấy"));
                return Json(list, JsonRequestBehavior.AllowGet);
            }

            List<Truong> truongs = db.Truongs.Where(t => t.Ten.Contains(text)).ToList();
            List<Nganh> nganhs = db.Nganhs.Where(t => t.Ten.Contains(text)).ToList();
            List<ThanhPho> thanhphoes = db.ThanhPhoes.Where(t => t.Ten.Contains(text)).ToList();

            //Truong
            for (int i = 0; i < truongs.Count; i++)
            {
                list.Add(new TabOject(truongs[i].MaTruong, "T", "Trường: " + " - " + truongs[i].Ten + "(" + truongs[i].ThanhPho.Ten + ")"));
            }

            //Nganh
            for (int i = 0; i < nganhs.Count; i++)
            {
                list.Add(new TabOject(nganhs[i].MaNganh, "N", "Ngành: " + nganhs[i].Ten));
            }

            //ThanhPho
            for (int i = 0; i < thanhphoes.Count; i++)
            {
                list.Add(new TabOject(thanhphoes[i].ID.ToString(), "P", thanhphoes[i].Ten));
            }

            if (list.Count == 0)
            {
                list.Add(new TabOject("", null, "Không tìm thấy"));
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
      
        private string ValidationTextSearch(ref string text)
        {
            if (_dontsearch.Contains(text.ToUpper()))
            {
                return null;
            }
            if (text == "y" || text == "Y")
            {
                text = " " + text + " ";
                return text;
            }

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
    }
}