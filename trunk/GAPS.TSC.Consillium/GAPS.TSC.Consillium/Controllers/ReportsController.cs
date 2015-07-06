using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Services;

namespace GAPS.TSC.Consillium.Controllers
{
    public class ReportsController : BaseController
    {
        //
        // GET: /Reports/
        public ReportsController(IAttachmentService attachmentService) : base(attachmentService) {}

        public ActionResult Index()
        {
            return View();
        }
	}
}