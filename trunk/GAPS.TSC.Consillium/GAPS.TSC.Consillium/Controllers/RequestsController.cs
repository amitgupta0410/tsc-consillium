using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Services;

namespace GAPS.TSC.Consillium.Controllers
{
    public class RequestsController : BaseController
    {
        //
        // GET: /Requests/
        public RequestsController(IAttachmentService attachmentService) : base(attachmentService) {}

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