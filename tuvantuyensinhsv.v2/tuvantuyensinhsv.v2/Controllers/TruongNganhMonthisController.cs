using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;

namespace tuvantuyensinhsv.v2.Controllers
{
    public class TruongNganhMonthisController : Controller
    {
        private ProjectHEntities db = new ProjectHEntities();

        // GET: TruongNganhMonthis
        public ActionResult Index()
        {           
            return View();
        }        

        // GET: TruongNganhMonthis/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TruongNganhMonthi truongNganhMonthi = await db.TruongNganhMonthis.FindAsync(id);
            if (truongNganhMonthi == null)
            {
                return HttpNotFound();
            }
            return View(truongNganhMonthi);
        }

        // GET: TruongNganhMonthis/Create
        public ActionResult Create()
        {
            ViewBag.MaNganh = new SelectList(db.Nganhs, "MaNganh", "Ten");
            ViewBag.MaTruong = new SelectList(db.Truongs, "MaTruong", "Ten");
            return View();
        }

        // POST: TruongNganhMonthis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaTruong,MaNganh,idMon1,idMon2,idMon3,Nam,Diem,SoLuongTruyCap")] TruongNganhMonthi truongNganhMonthi)
        {
            if (ModelState.IsValid)
            {
                db.TruongNganhMonthis.Add(truongNganhMonthi);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaNganh = new SelectList(db.Nganhs, "MaNganh", "Ten", truongNganhMonthi.MaNganh);
            ViewBag.MaTruong = new SelectList(db.Truongs, "MaTruong", "Ten", truongNganhMonthi.MaTruong);
            return View(truongNganhMonthi);
        }

        // GET: TruongNganhMonthis/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TruongNganhMonthi truongNganhMonthi = await db.TruongNganhMonthis.FindAsync(id);
            if (truongNganhMonthi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNganh = new SelectList(db.Nganhs, "MaNganh", "Ten", truongNganhMonthi.MaNganh);
            ViewBag.MaTruong = new SelectList(db.Truongs, "MaTruong", "Ten", truongNganhMonthi.MaTruong);
            return View(truongNganhMonthi);
        }

        // POST: TruongNganhMonthis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaTruong,MaNganh,idMon1,idMon2,idMon3,Nam,Diem,SoLuongTruyCap")] TruongNganhMonthi truongNganhMonthi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(truongNganhMonthi).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaNganh = new SelectList(db.Nganhs, "MaNganh", "Ten", truongNganhMonthi.MaNganh);
            ViewBag.MaTruong = new SelectList(db.Truongs, "MaTruong", "Ten", truongNganhMonthi.MaTruong);
            return View(truongNganhMonthi);
        }

        // GET: TruongNganhMonthis/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TruongNganhMonthi truongNganhMonthi = await db.TruongNganhMonthis.FindAsync(id);
            if (truongNganhMonthi == null)
            {
                return HttpNotFound();
            }
            return View(truongNganhMonthi);
        }

        // POST: TruongNganhMonthis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            TruongNganhMonthi truongNganhMonthi = await db.TruongNganhMonthis.FindAsync(id);
            db.TruongNganhMonthis.Remove(truongNganhMonthi);
            await db.SaveChangesAsync();
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
