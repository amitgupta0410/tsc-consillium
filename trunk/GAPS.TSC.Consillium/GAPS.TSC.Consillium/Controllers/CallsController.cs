using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Services;

namespace GAPS.TSC.Consillium.Controllers
{
    public class CallsController : BaseController
    {
        //
        // GET: /Calls/
        public CallsController(IAttachmentService attachmentService) : base(attachmentService) {}

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