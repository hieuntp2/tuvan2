using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;
using Microsoft.AspNet.Identity;
using System;

namespace tuvantuyensinhsv.v2.Controllers
{
    public class QuestionsController : Controller
    {
        private ProjectHEntities db = new ProjectHEntities();

        // GET: Questions
        public ActionResult Index()
        {
            List<Question> questions = db.Questions.Include(q => q.AspNetUser).OrderBy(t => t.Ngaydang).ToList();
            
            return View(questions);
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }

            List<Question> questions = db.Questions.OrderBy(t => t.Ngaydang).Take(5).ToList();
            ViewBag.questions = questions;
            return View(question);
        }

        public JsonResult getQuestion(int? index)
        {
            JsonObjectBaiViet[] questions = new JsonObjectBaiViet[5];

            int skip;
            if (index == null || index == 0)
            {
                skip = 0;
            }
            else
            {
                skip = (int)(3 * index);
            }

            questions = db.Questions.OrderByDescending(t => t.Ngaydang).Skip(skip).Take(3).Select(t => new JsonObjectBaiViet()
            {
                id = t.id,
                tieude = t.Tieude,
                noidung = t.Noidung,
                ngaydang = t.Ngaydang.ToString(),
                avatar = t.AspNetUser.profile_avatar_link
            }).ToArray();

            for (int i = 0; i < questions.Length; i++)
            {
                preparebaiviets(ref questions[i]);
            }
            return Json(questions, JsonRequestBehavior.AllowGet);
        }

        private void preparebaiviets(ref JsonObjectBaiViet bai)
        {
            if (bai.noidung.Length >= 100)
            {
                bai.noidung = bai.noidung.Substring(0, 99);
            }
            bai.loai = 'q';
            bai.noidung = RemoveHTMLTags(bai.noidung);
        }

        private string RemoveHTMLTags(string HTMLCode)
        {
            return System.Text.RegularExpressions.Regex.Replace(
              HTMLCode, "<[^>]*>", "");
        }

        // GET: Questions/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "id,Tieude,Noidung,userid,Tabs")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.Ngaydang = DateTime.Now;
                question.userid = User.Identity.GetUserId();
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "UserName", question.userid);
            return View(question);
        }

        // GET: Questions/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "UserName", question.userid);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "id,Tieude,Noidung,userid")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "UserName", question.userid);
            return View(question);
        }

        // GET: Questions/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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
