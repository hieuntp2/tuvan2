using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;
namespace tuvantuyensinhsv.v2.Controllers
{
    public class HomeController : Controller
    {
        private ProjectHEntities db = new ProjectHEntities();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            //List<Question> questions = db.Questions.OrderBy(t=>t.Ngaydang).Take(5).ToList();
            //ViewBag.questions = questions;

            //List<BaiViet> baiviets = db.BaiViets.OrderBy(t => t.NguoiDang).Take(5).ToList();
            //ViewBag.baiviets = baiviets;
            return View();
        }

        [HttpPost]
        public ActionResult AjaxLoadTruongNganh(string ID)
        {
            List<TruongNganh> list = db.TruongNganhs.Where(t => t.MaTruong == ID).ToList();
            return PartialView(list);
        }
   
        public ActionResult About()
        {
            return View();
        }

        public ActionResult IndexSearch()
        {
            SystemInfo record = new SystemInfo();
            record.AddValue("homepage",1);
            return View();
        }

	}
}