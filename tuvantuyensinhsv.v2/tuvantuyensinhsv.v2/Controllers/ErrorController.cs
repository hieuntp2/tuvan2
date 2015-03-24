using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tuvantuyensinhsv.v2.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View("NotFound");
        }
        public ViewResult NotFound()
        {
            Response.StatusCode = 404;  //you may want to set this to 200
            return View("NotFound");
        }
    }
}
