using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;
namespace tuvantuyensinhsv.v2.Controllers
{
    public class sachController : Controller
    {
        ProjectHEntities db = new ProjectHEntities();
        //
        // GET: /sach/
        // Sach cho: type = true.
        // Sach nhan: type = false
        public ActionResult Index()
        {
            List<Sach> sachs = db.Saches.ToList();

            return View(sachs);
        }

        public ActionResult getbookinfor(int id)
        {
            Sach sach = db.Saches.SingleOrDefault(t => t.id == id);
            Sachreturn s = new Sachreturn
            {
                id = sach.id,
                Ten = sach.Ten,
                linkanh = sach.linkanh,
                NXB = sach.NXB,
                tacgia = sach.tacgia
            };

            s.nguoi_cho = (from item in db.SachChoMuons
                           where item.id_sach == s.id && item.loai == true
                           select new UserInfor
                           {
                               ten = item.AspNetUser.hoTen,
                               anhavatar = item.AspNetUser.profile_avatar_link,
                               soluong = (int)item.soluong,
                               commnet = item.comment
                           }).ToList();

            s.nguoi_nhan = (from item in db.SachChoMuons
                            where item.id_sach == s.id && item.loai == false
                            select new UserInfor
                            {
                                ten = item.AspNetUser.hoTen,
                                anhavatar = item.AspNetUser.profile_avatar_link,
                                soluong = (int)item.soluong,
                                commnet = item.comment
                            }).ToList();
            return Json(s, JsonRequestBehavior.AllowGet);
        }
    }

    public class Sachreturn
    {
        public int id { get; set; }
        public string Ten { get; set; }
        public string linkanh { get; set; }
        public string NXB { get; set; }
        public string tacgia { get; set; }

        public List<UserInfor> nguoi_cho;
        public List<UserInfor> nguoi_nhan;
    }

    public class UserInfor
    {
        public string ten;
        public string anhavatar;
        public int soluong;
        public string commnet;
    }
}