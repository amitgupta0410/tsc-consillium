using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Services;
using GAPS.TSC.Consillium.Models;
//using GAPS.TSC.CONSILLIUM.Services;

namespace GAPS.TSC.Consillium.Controllers
{
    public class RequestsController : BaseController
    {

        private readonly IUserService _userService;
        private readonly IMainMastersService _masterService;
        private readonly IProjectService _projectService;

        //public RequestsController(IUserService userService,IMainMastersService mastersService,IProjectService projectService)
        //{
        //    _userService = userService;
        //    _masterService = mastersService;
        //    _projectService = projectService;
        //}
        //
        // GET: /Requests/
        public RequestsController(IAttachmentService attachmentService) : base(attachmentService) {}

        public ActionResult RequestExpertEn()
        {
            var model = new RequestExpertEn();
            var projectClients =
                _projectService.GetAllMasterProjects().Select(x => x.ClientId).Distinct().ToList();
            return View(model);
        }


        public JsonResult GetProjects(int id)
        {
            var projects = _projectService.GetAllMasterProjects().Where(x => x.ClientId == id);
            return Json(projects.Select(x => new { x.Id, x.Name }), JsonRequestBehavior.AllowGet);
        }

        

        public ActionResult RequestExpertManual()
        {
            return View();
        }
	}
}