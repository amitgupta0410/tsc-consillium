using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAPS.TSC.Consillium.Controllers
{
    public class CallsController : Controller
    {
        //
        // GET: /Calls/
        public ActionResult CallDasboard()
        {
            return View();
        }
        public ActionResult PaidCallDetails()
        {

            return View();
        }
	}
}