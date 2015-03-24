using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;
using Microsoft.AspNet.Identity;

namespace tuvantuyensinhsv.v2.Controllers
{
    public class NganhsController : Controller
    {
        private ProjectHEntities db = new ProjectHEntities();

        // GET: /Nganhs/
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Index()
        {
            return View(db.Nganhs.ToList());
        }

        // GET: /Nganhs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nganh nganh = db.Nganhs.Find(id);
            if (nganh == null)
            {
                return HttpNotFound();
            }

            List<BaiViet> baiviets = db.BaiViets.Where(t => t.Tabs.Contains("N" + nganh.MaNganh)).ToList();
            ViewBag.baiviets = baiviets;

            SystemInfo record = new SystemInfo();
            record.AddValue("nganh_"+id, 1);

            return View(nganh);
        }

        // GET: /Nganhs/Create
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Nganhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Create([Bind(Include="Ten,MaNganh")] Nganh nganh)
        {
            if (ModelState.IsValid)
            {
                db.Nganhs.Add(nganh);
                db.SaveChanges();

                SysLogEngine log = new SysLogEngine();
                log.WriteUserLog("Tạo ngành: " + nganh.MaNganh, User.Identity.GetUserId());

                return RedirectToAction("Index");
            }

            return View(nganh);
        }

        // GET: /Nganhs/Edit/5
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Edit(string id)
        {
            if (id == null || id==" ")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nganh nganh = db.Nganhs.Find(id);
            if (nganh == null)
            {
                return HttpNotFound();
            }
            return View(nganh);
        }

        // POST: /Nganhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Edit([Bind(Include="Ten,MaNganh")] Nganh nganh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nganh).State = EntityState.Modified;
                db.SaveChanges();

                SysLogEngine log = new SysLogEngine();
                log.WriteUserLog("Sửa ngành: " + nganh.MaNganh, User.Identity.GetUserId());

                return RedirectToAction("Index");
            }
            return View(nganh);
        }

        // GET: /Nganhs/Delete/5
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nganh nganh = db.Nganhs.Find(id);
            if (nganh == null)
            {
                return HttpNotFound();
            }
            return View(nganh);
        }

        // POST: /Nganhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult DeleteConfirmed(string id)
        {
            Nganh nganh = db.Nganhs.Find(id);
            db.Nganhs.Remove(nganh);
            db.SaveChanges();

            SysLogEngine log = new SysLogEngine();
            log.WriteUserLog("Tạo ngành: " + nganh.MaNganh, User.Identity.GetUserId());

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
