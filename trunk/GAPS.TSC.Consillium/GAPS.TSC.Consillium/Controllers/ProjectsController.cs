using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Services;

namespace GAPS.TSC.Consillium.Controllers
{
    public class ProjectsController : BaseController
    {
        //
        // GET: /Projects/
        public ProjectsController(IAttachmentService attachmentService) : base(attachmentService) {}

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