using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;

namespace tuvantuyensinhsv.v2.Controllers
{
    public class ThanhPhoesController : Controller
    {
        private ProjectHEntities db = new ProjectHEntities();

        // GET: /ThanhPhoes/
        [Authorize(Roles = "Admin,Moderate")]
        public ActionResult Index()
        {
            return View(db.ThanhPhoes.ToList());
        }

        // GET: /ThanhPhoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhPho thanhpho = db.ThanhPhoes.Find(id);
            if (thanhpho == null)
            {
                return HttpNotFound();
            }
            return View(thanhpho);
        }

        // GET: /ThanhPhoes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ThanhPhoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include="Ten,ID,MoTa")] ThanhPho thanhpho)
        {
            if (ModelState.IsValid)
            {
                db.ThanhPhoes.Add(thanhpho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thanhpho);
        }

        // GET: /ThanhPhoes/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhPho thanhpho = db.ThanhPhoes.Find(id);
            if (thanhpho == null)
            {
                return HttpNotFound();
            }
            return View(thanhpho);
        }

        // POST: /ThanhPhoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include="Ten,ID,MoTa")] ThanhPho thanhpho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thanhpho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thanhpho);
        }

        // GET: /ThanhPhoes/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhPho thanhpho = db.ThanhPhoes.Find(id);
            if (thanhpho == null)
            {
                return HttpNotFound();
            }
            return View(thanhpho);
        }

        // POST: /ThanhPhoes/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThanhPho thanhpho = db.ThanhPhoes.Find(id);
            db.ThanhPhoes.Remove(thanhpho);
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
