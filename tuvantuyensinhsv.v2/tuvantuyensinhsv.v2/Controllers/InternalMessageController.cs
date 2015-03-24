using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;
using Microsoft.AspNet.Identity;

namespace tuvantuyensinhsv.v2.Controllers
{
    [Authorize]
    public class InternalMessageController : Controller
    {
        private ProjectHEntities db = new ProjectHEntities();

        // GET: /InternalMessage/
        public ActionResult Index()
        {
            string userid = User.Identity.GetUserId();
            List<InternalMessage> list = db.InternalMessages.Where(t=>t.AspNetUser1.Id == userid).ToList();
            return View(list);
        }

        // GET: /InternalMessage/Details/5
        [HttpPost]
        public ActionResult Details(string from, string date)
        {
            if (from == null || date == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTime enteredDate = DateTime.Parse(date.Trim());
            string currentuser = User.Identity.GetUserId();
            InternalMessage internalmessage = db.InternalMessages.SingleOrDefault(t => t.DateCreate.Hour == enteredDate.Hour
                && t.DateCreate.Minute == enteredDate.Minute
                && t.DateCreate.Second == enteredDate.Second
                    && t.From == from
                    && t.To == currentuser);
            if (internalmessage == null)
            {
                return HttpNotFound();
            }
            return PartialView("Details", internalmessage);
        }

        // GET: /InternalMessage/Create

        public ActionResult Create(string To = null)
        {
            InternalMessage message = new InternalMessage();
            message.To = To;
            return PartialView("Create", message);
        }

        // POST: /InternalMessage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Create([Bind(Include = "To,Mesage")] InternalMessage internalmessage)
        {
            if (ModelState.IsValid)
            {
                AspNetUser touser = db.AspNetUsers.SingleOrDefault(t => t.UserName == internalmessage.To);
                if(touser == null)
                {
                    return "Wrong UserID or Username!";
                }
                if (touser.Id == User.Identity.GetUserId())
                {
                    return "FAIL";
                }

                internalmessage.To = touser.Id;
                internalmessage.From = User.Identity.GetUserId();

                internalmessage.DateCreate = DateTime.Now;
                db.InternalMessages.Add(internalmessage);
                db.SaveChanges();
                return "OK";
            }

            ViewBag.From = new SelectList(db.AspNetUsers, "Id", "UserName", internalmessage.From);
            ViewBag.To = new SelectList(db.AspNetUsers, "Id", "UserName", internalmessage.To);
            return null;
        }

        // POST: /InternalMessage/Delete/5
        [HttpPost]
        public string DeleteConfirmed(string from,DateTime datecreate)
        {
            string to = User.Identity.GetUserId();
            InternalMessage internalmessage = db.InternalMessages.SingleOrDefault(t => t.DateCreate.Hour == datecreate.Hour
                && t.DateCreate.Minute == datecreate.Minute
                && t.DateCreate.Second == datecreate.Second
                    && t.From == from
                    && t.To == to);
            db.InternalMessages.Remove(internalmessage);
            db.SaveChanges();
            return "OK";
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
