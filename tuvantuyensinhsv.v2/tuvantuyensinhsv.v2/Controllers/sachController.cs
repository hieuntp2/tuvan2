using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tuvantuyensinhsv.v2.Models;
namespace tuvantuyensinhsv.v2.Controllers
{
    public class sachController : Controller
    {
        ProjectHEntities db = new ProjectHEntities();
        //
        // GET: /sach/
        // Sach cho: type = true.
        // Sach nhan: type = false
        public ActionResult Index()
        {
            List<SachChoMuon> sachs = db.SachChoMuons.ToList();
           
            return View(sachs);
        }
	}
}