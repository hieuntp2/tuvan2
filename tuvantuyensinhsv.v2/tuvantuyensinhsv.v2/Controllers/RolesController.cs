using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;

namespace tuvantuyensinhsv.v2.Controllers
{
    [Authorize(Roles="Admin")]
    public class RolesController : Controller
    {
        private ProjectHEntities db = new ProjectHEntities();

        // GET: /Roles/
        public ActionResult Index()
        {
            return View(db.AspNetRoles.ToList());
        }

        [HttpPost]
        public ActionResult AssignUserToRole(string Username, string IDRole)
        {
            AspNetUser user = db.AspNetUsers.SingleOrDefault(t => t.UserName == Username);
            AspNetRole role = db.AspNetRoles.SingleOrDefault(t => t.Id == IDRole);
            AccountController account = new AccountController();

            account.UserManager.AddToRoleAsync(user.Id, role.Name);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RemoveUserFromRole(string IDUser, string IDRole)
        {
            AccountController account = new AccountController();
            AspNetRole role = db.AspNetRoles.SingleOrDefault(t => t.Id == IDRole);
            account.UserManager.RemoveFromRoleAsync(IDUser, role.Name);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Roles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRole aspnetrole = db.AspNetRoles.Find(id);
            if (aspnetrole == null)
            {
                return HttpNotFound();
            }
            return View(aspnetrole);
        }

        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name")] AspNetRole aspnetrole)
        {
            if (ModelState.IsValid)
            {
                db.AspNetRoles.Add(aspnetrole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspnetrole);
        }

        // GET: /Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRole aspnetrole = db.AspNetRoles.Find(id);
            if (aspnetrole == null)
            {
                return HttpNotFound();
            }
            return View(aspnetrole);
        }

        // POST: /Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name")] AspNetRole aspnetrole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspnetrole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspnetrole);
        }

        // GET: /Roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRole aspnetrole = db.AspNetRoles.Find(id);
            if (aspnetrole == null)
            {
                return HttpNotFound();
            }
            return View(aspnetrole);
        }

        // POST: /Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetRole aspnetrole = db.AspNetRoles.Find(id);
            db.AspNetRoles.Remove(aspnetrole);
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
