using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;

namespace tuvantuyensinhsv.v2.Controllers
{
    public class MBTIsController : Controller
    {
        private ProjectHEntities db = new ProjectHEntities();

        // GET: MBTIs
        public ActionResult Index()
        {
            SystemInfo record = new SystemInfo();
            record.AddValue("mbti_index", 1);
            return View("/Views/MBTIs/Index2.cshtml");
        }

        // GET: MBTIs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MBTI mBTI = db.MBTIs.SingleOrDefault(t => t.idMBTI == id);
            if (mBTI == null)
            {
                return HttpNotFound();
            }

            TabEngineServerController tag = new TabEngineServerController();
            List<TabOject> list = tag.Encode(mBTI.Tags);
            ViewBag.tags = list;

            SystemInfo record = new SystemInfo();
            record.AddValue("mbti_"+id, 1);

            return PartialView("Details", mBTI);
        }


        public ActionResult getRangeSchool(string idnganh, float diem)
        {
            if (idnganh == null || diem == null)
                return null;
            List<TruongNganh> lon = db.TruongNganhs.Where(t => t.MaNganh == idnganh && t.Diem1 >= diem).OrderBy(t => t.Diem1).Skip(0).Take(5).ToList();
            List<TruongNganh> nho = db.TruongNganhs.Where(t => t.MaNganh == idnganh && t.Diem1 < diem).OrderByDescending(t => t.Diem1).Skip(0).Take(5).ToList();

            List<TruongNganh> res = new List<TruongNganh>();
            res.AddRange(lon);
            res.AddRange(nho);
            res = res.OrderByDescending(t => t.Diem1).ToList();

            return PartialView(res);
        }

        // GET: MBTIs/Create
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: MBTIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Create([Bind(Include = "idMBTI,Tags,GioiThieu")] MBTI mBTI)
        {
            if (ModelState.IsValid)
            {
                db.MBTIs.Add(mBTI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mBTI);
        }

        // GET: MBTIs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MBTI mBTI = db.MBTIs.Find(id);
            if (mBTI == null)
            {
                return HttpNotFound();
            }
            return View(mBTI);
        }

        // POST: MBTIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Edit([Bind(Include = "idMBTI,Tags,GioiThieu")] MBTI mBTI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mBTI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mBTI);
        }

        // GET: MBTIs/Delete/5
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MBTI mBTI = db.MBTIs.Find(id);
            if (mBTI == null)
            {
                return HttpNotFound();
            }
            return View(mBTI);
        }

        // POST: MBTIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult DeleteConfirmed(string id)
        {
            MBTI mBTI = db.MBTIs.Find(id);
            db.MBTIs.Remove(mBTI);
            db.SaveChanges();
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
