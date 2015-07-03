using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public BaseController()
        { }
        protected void SetMessage(MessageType messageType, string message)
        {
            TempData["Message"] = message;
            TempData["MessageType"] = messageType;
        }
        public ActionResult Index()
        {
            return View();
        }
	}
}