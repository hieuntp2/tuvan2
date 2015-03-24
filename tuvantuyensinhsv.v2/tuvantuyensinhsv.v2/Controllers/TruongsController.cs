using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;
using Microsoft.AspNet.Identity;

namespace tuvantuyensinhsv.v2.Controllers
{
    public class TruongsController : Controller
    {
        private ProjectHEntities db = new ProjectHEntities();

        // GET: /Truongs/
        public ActionResult Index()
        {
            var truongs = db.Truongs.Include(t => t.ThanhPho);
            return View(truongs.ToList());
        }

        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult ThieuWebsite()
        {
            List<Truong> truongs = db.Truongs.Where(t => t.Website == null).ToList() ;
            ViewBag.Title = "Trường thiếu Webiste";
            return View("ThieuWebsite",truongs);
        }

        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult ThieuAddress()
        {
            List<Truong> truongs = db.Truongs.Where(t => t.IDThanhPho == 1).ToList();
            ViewBag.Title = "Trường thiếu Địa chỉ";
            return View("ThieuWebsite", truongs);
        }

        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult ThieuLogo()
        {
            List<Truong> truongs = db.Truongs.Where(t => t.linkLogo == null).ToList();
            ViewBag.Title = "Trường thiếu Logo";
            return View("ThieuWebsite", truongs);
        }

        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult ThieuTruongNganh()
        {
            List<Truong> truongs = db.Truongs.Where(t => t.TruongNganhs.Count() == 0).ToList();
            ViewBag.Title = "Trường thiếu thông tin Ngành";
            return View("ThieuWebsite", truongs);
        }

        // GET: /Truongs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Truong truong = db.Truongs.Find(id);
            if (truong == null)
            {
                return HttpNotFound();
            }

            List<BaiViet> baiviets = db.BaiViets.Where(t => t.Tabs.Contains("T" + truong.MaTruong)).ToList();
            ViewBag.baiviets = baiviets;

            SystemInfo record = new SystemInfo();
            record.AddValue("truong_"+id, 1);

            return View(truong);
        }

        // GET: /Truongs/Create
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Create()
        {
            ViewBag.IDThanhPho = new SelectList(db.ThanhPhoes, "ID", "Ten");
            return View();
        }

        // POST: /Truongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Create([Bind(Include="Ten,Website,IDThanhPho,MaTruong,LoaiTruong")] Truong truong)
        {
            if (ModelState.IsValid)
            {
                db.Truongs.Add(truong);
                db.SaveChanges();

                SysLogEngine log = new SysLogEngine();
                log.WriteUserLog("Tạo trường " + truong.MaTruong, User.Identity.GetUserId());

                return RedirectToAction("Details", new { id = truong.MaTruong });
            }

            ViewBag.IDThanhPho = new SelectList(db.ThanhPhoes, "ID", "Ten", truong.IDThanhPho);
            return View(truong);
        }

        // GET: /Truongs/Edit/5
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truong truong = db.Truongs.Find(id);
            if (truong == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDThanhPho = new SelectList(db.ThanhPhoes, "ID", "Ten", truong.IDThanhPho);
            return View(truong);
        }

        // POST: /Truongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Edit([Bind(Include = "Ten,MaTruong,linkLogo,Website,IDThanhPho,LoaiTruong,DeAnTuyenSInh,GioiThieu")] Truong truong)
        {
            if (ModelState.IsValid)
            {
                SysLogEngine log = new SysLogEngine();
                log.WriteUserLog("Sửa trường " + truong.MaTruong, User.Identity.GetUserId());

                db.Entry(truong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = truong.MaTruong });
            }
            ViewBag.IDThanhPho = new SelectList(db.ThanhPhoes, "ID", "Ten", truong.IDThanhPho);
            return View(truong);
        }

        // GET: /Truongs/Delete/5
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truong truong = db.Truongs.Find(id);
            if (truong == null)
            {
                return HttpNotFound();
            }
            return View(truong);
        }

        // POST: /Truongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult DeleteConfirmed(string id)
        {
            Truong truong = db.Truongs.Find(id);           

            db.Truongs.Remove(truong);
            db.SaveChanges();

            SysLogEngine log = new SysLogEngine();
            log.WriteUserLog("Xóa trường" + truong.MaTruong, User.Identity.GetUserId());

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
