using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAPS.TSC.Consillium.Controllers
{
    public class RequestsController : Controller
    {
        //
        // GET: /Requests/
        public ActionResult RequestExpertEn()
        {
            return View();
        }

        public ActionResult RequestExpertManual()
        {
            return View();
        }
	}
}