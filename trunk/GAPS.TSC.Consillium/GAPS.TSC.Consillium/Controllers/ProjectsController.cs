using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAPS.TSC.Consillium.Controllers
{
    public class ProjectsController : Controller
    {
        //
        // GET: /Projects/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpertRequisition()
        {
            return View();
        }

        public ActionResult Projects()
        {
            return View();
        }
        public ActionResult Payments()
        {
            return View();
        }
	}
}