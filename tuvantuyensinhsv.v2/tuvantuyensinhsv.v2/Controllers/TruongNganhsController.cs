using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;
using Microsoft.AspNet.Identity;

namespace tuvantuyensinhsv.v2.Controllers
{
    public class TruongNganhsController : Controller
    {
        private ProjectHEntities db = new ProjectHEntities();

        // GET: /TruongNganhs/
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Index()
        {
            var truongnganhs = db.TruongNganhs.ToList();
            return View(truongnganhs);
        }

        // GET: /TruongNganhs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TruongNganh truongnganh = db.TruongNganhs.Find(id);
            if (truongnganh == null)
            {
                return HttpNotFound();
            }
            return View(truongnganh);
        }

        // GET: /TruongNganhs/Create
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Create(string MaTruong)
        {
            if (MaTruong != null)
            {
                Truong truong = db.Truongs.SingleOrDefault(t => t.MaTruong == MaTruong);
                if (truong != null)
                {
                    ViewBag.MaTruong = truong.MaTruong;
                }
            }
            ViewBag.KhoiThi = new SelectList(db.KhoiThis, "ID", "Ten");
            ViewBag.MaNganh = new SelectList(db.Nganhs, "MaNganh", "Ten");
            ViewBag.MaTruong = new SelectList(db.Truongs, "MaTruong", "Ten");
            return View();
        }

        // POST: /TruongNganhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Create([Bind(Include = "MaTruong,MaNganh,KhoiThi,Diem1,Diem2,Diem3,NgayCapNhat")] TruongNganh truongnganh)
        {
            if (ModelState.IsValid)
            {
                TruongNganh findtruong = db.TruongNganhs.SingleOrDefault(t => t.MaTruong == truongnganh.MaTruong
                                                                        && t.MaNganh == truongnganh.MaNganh
                                                                        && t.KhoiThi == truongnganh.KhoiThi);
                truongnganh.NgayCapNhat = DateTime.Now;  
                if(findtruong == null)
                {
                    db.TruongNganhs.Add(truongnganh);
                }
                else
                {
                    findtruong.NgayCapNhat = DateTime.Now;
                    findtruong.Diem1 = truongnganh.Diem1;
                    findtruong.Diem2 = truongnganh.Diem2;
                    findtruong.Diem3 = truongnganh.Diem3;
                    db.Entry(findtruong).State = EntityState.Modified;
                }              

                db.SaveChanges();

                 SysLogEngine log = new SysLogEngine();
                 log.WriteUserLog("Tạo T-N, T-ID: " + findtruong.MaTruong + "N-ID: " + findtruong.MaNganh, User.Identity.GetUserId());

                return RedirectToAction("Details", "Truongs", new { ID = truongnganh.MaTruong });
            }
            return RedirectToAction("Details", "Truongs", new { ID = truongnganh.MaTruong });
        }

        // GET: /TruongNganhs/Edit/5
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Edit(string MaTruong, string idnganh, string idkhoi)
        {
            if (MaTruong == null || idnganh == null || idkhoi == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TruongNganh truongnganh = db.TruongNganhs.SingleOrDefault(t => t.MaTruong == MaTruong
                                                                       && t.MaNganh == idnganh
                                                                       && t.KhoiThi == idkhoi);
            if (truongnganh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNganh = new SelectList(db.Nganhs, "ID", "Ten", truongnganh.MaNganh);
            ViewBag.MaTruong = new SelectList(db.Truongs, "ID", "Ten", truongnganh.MaTruong);
            return View(truongnganh);
        }

        // POST: /TruongNganhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Edit([Bind(Include = "MaTruong,IDNganh,KhoiThi,Diem1,Diem2,Diem3,NgayCapNhat")] TruongNganh truongnganh)
        {
            if (ModelState.IsValid)
            {
                truongnganh.NgayCapNhat = DateTime.Now;
                db.Entry(truongnganh).State = EntityState.Modified;               

                db.SaveChanges();

                SysLogEngine log = new SysLogEngine();
                log.WriteUserLog("Tạo T-N, T-ID: " + truongnganh.MaTruong + "N-ID: " + truongnganh.MaNganh, User.Identity.GetUserId());

                return RedirectToAction("Details", "Truongs", new { ID = truongnganh.MaTruong });
            }
            return RedirectToAction("Details", "Truongs", new { ID = truongnganh.MaTruong });
        }

        // GET: /TruongNganhs/Delete/5
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Delete(string MaTruong, string idnganh, string idkhoi)
        {
            if (MaTruong == null || idnganh == null || idkhoi == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TruongNganh truongnganh = db.TruongNganhs.SingleOrDefault(t => t.MaTruong == MaTruong
                                                                       && t.MaNganh == idnganh
                                                                       && t.KhoiThi == idkhoi);
            if (truongnganh == null)
            {
                return HttpNotFound();
            }
            return View(truongnganh);
        }

        // POST: /TruongNganhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult DeleteConfirmed(string MaTruong, string idnganh, string idkhoi)
        {
            if (MaTruong == null || idnganh == null || idkhoi == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TruongNganh truongnganh = db.TruongNganhs.SingleOrDefault(t => t.MaTruong == MaTruong
                                                                       && t.MaNganh == idnganh
                                                                       && t.KhoiThi == idkhoi);           

            db.TruongNganhs.Remove(truongnganh);
            db.SaveChanges();

            SysLogEngine log = new SysLogEngine();
            log.WriteUserLog("Tạo T-N, T-ID: " + truongnganh.MaTruong + "N-ID: " + truongnganh.MaNganh, User.Identity.GetUserId());

            return RedirectToAction("Details", "Truongs", new { ID = truongnganh.MaTruong });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult LoadTruong()
        {
            List<JsonResultRequest> truongs = new List<JsonResultRequest>();

            List<Truong> listtruong = db.Truongs.ToList();
            for (int i = 0; i < listtruong.Count; i++)
            {
                truongs.Add(new JsonResultRequest(listtruong[i].MaTruong.ToString(), listtruong[i].Ten));
            }

            return Json(truongs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult LoadNganh()
        {
            List<JsonResultRequest> nganhs = new List<JsonResultRequest>();

            List<Nganh> listnganh = db.Nganhs.ToList();
            for (int i = 0; i < listnganh.Count; i++)
            {
                nganhs.Add(new JsonResultRequest(listnganh[i].MaNganh.ToString(), listnganh[i].Ten));
            }

            return Json(nganhs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult LoadKhoi()
        {
            List<JsonResultRequest> khois = new List<JsonResultRequest>();

            List<KhoiThi> listkhoi = db.KhoiThis.ToList();
            for (int i = 0; i < listkhoi.Count; i++)
            {
                khois.Add(new JsonResultRequest(listkhoi[i].ID.ToString(), listkhoi[i].Ten));
            }

            return Json(khois, JsonRequestBehavior.AllowGet);
        }
    }

    public class JsonResultRequest
    {
        public string ID { get; set; }
        public string Ten { get; set; }

        public JsonResultRequest(string ID, string Ten)
        {
            this.ID = ID;
            this.Ten = Ten;
        }
    }
}
