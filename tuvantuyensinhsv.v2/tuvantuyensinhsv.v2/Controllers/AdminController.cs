using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;
using Microsoft.AspNet.Identity;

namespace tuvantuyensinhsv.v2.Controllers
{
    [Authorize(Roles = "Admin,Moderate")]
    public class AdminController : Controller
    {
        private ProjectHEntities db = new ProjectHEntities();
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            string userid = User.Identity.GetUserId();
            ViewBag.MessageCount = db.InternalMessages.Where(t => t.AspNetUser1.Id == userid).Count();
            ViewBag.truongscount = db.Truongs.Count();

            ViewBag.baivietcount = db.BaiViets.Where(t => t.Trangthai == 0).Count();
            ViewBag.usercount = db.AspNetUsers.Count();
            ViewBag.system_infos = db.SystemInformations.ToList();

            return View();
        }

        public ActionResult Admin_ChangeStatePost(int ID, int stt)
        {
            //if (ID == null || stt == null)
            //{
            //    return HttpNotFound("NULL ID or STT");
            //}

            if (stt < 0 || stt > 4)
            {
                return HttpNotFound("Wrong status");
            }

            BaiViet baiviet = db.BaiViets.SingleOrDefault(t => t.ID == ID);

            if (baiviet == null)
            {
                return HttpNotFound("Bai Viet not found!");
            }

            baiviet.Trangthai = stt;

            db.Entry(baiviet).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "BaiViets");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiViet baiviet = db.BaiViets.Find(id);

            if (baiviet == null)
            {
                return HttpNotFound("Không tìm thấy bài viết.");
            }
            if (baiviet.Trangthai == 3)
            {
                db.BaiViets.Remove(baiviet);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound("Bài viết chưa được xác nhận bởi User!");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public string FResetSystem_Value(string key)
        {
            SystemInfo item = new SystemInfo();
            if (item == null)
                return "fail";
            item.SetValue(key, 0);
            

            return "";
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public string FRemoveSystem_Value(string key)
        {
            SystemInfo item = new SystemInfo();
            if (item == null)
                return "fail";
            item.RemoveValue(key);

            return "";
        }
    }
}