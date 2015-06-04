using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;
using Microsoft.AspNet.Identity;

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

        public ActionResult tangsach()
        {
            return View();
        }

        [HttpPost]
        public ActionResult tangsach(List<SachJSONRecieve> saches)
        {
            for (int i = 0; i < saches.Count; i++)
            {

                if (saches[i].id < 0)
                {
                    Sach s = new Sach();
                    s.linkanh = saches[i].linkanh;
                    s.NXB = saches[i].nxb;
                    s.tacgia = saches[i].tacgia;
                    s.Ten = saches[i].ten;

                    addnewsachtodatabase(s);
                    saches[i].id = s.id;
                }

                SachChoMuon cm = new SachChoMuon();
                cm.id_sach = saches[i].id;
                cm.id_nguoidung = User.Identity.GetUserId();
                cm.loai = saches[i].loai;
                cm.soluong = saches[i].soluong;

                db.SachChoMuons.Add(cm);
            }
            db.SaveChanges();
            return View();
        }

        private void addnewsachtodatabase(Sach sach)
        {
            db.Saches.Add(sach);
        }

        public ActionResult getAllBook()
        {
            List<Sachreturn> ss = (from item in db.Saches
                                   select new Sachreturn
                                   {
                                       id = item.id,
                                       linkanh = item.linkanh,
                                       NXB = item.NXB,
                                       tacgia = item.NXB,
                                       Ten = item.Ten
                                   }).ToList();
            return Json(ss, JsonRequestBehavior.AllowGet);
        }
    }

    public class SachJSONRecieve
    {
        public int id { get; set; }
        public string ten { get; set; }
        public string linkanh { get; set; }
        public string nxb { get; set; }
        public string tacgia { get; set; }
        public bool loai { get; set; }
        public int soluong { get; set; }
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