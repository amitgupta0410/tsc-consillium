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

      
        // GET: /Requests/
        public RequestsController(IAttachmentService attachmentService, IMainMastersService mastersService,
            IProjectService projectService, IUserService userService) : base(attachmentService)
        {
           _userService= userService;
            _masterService = mastersService;
            _projectService = projectService;
        }

        public ActionResult RequestExpertEn()
        {
            var model = new RequestExpertEn();
            var projectClients =
                _projectService.GetAllMasterProjects().Select(x => x.ClientId).Distinct().ToList();
            model.Clients =_masterService.GetAllClients().Where(x => projectClients.Contains(x.Id) && x.IsActive).ToDictionary(x => x.Id, x => x.Name);
            model.Industry=_masterService.GetAllIndustries().ToDictionary(x => x.Id, x => x.Name);
            model.Geography = _masterService.GetAllGeographies().ToDictionary(x => x.Id, x => x.Name);
            model.Currency = _masterService.GetAllCurrencies().ToDictionary(x => x.CurrencyId, x => x.CurrencyName);
            return View(model);
        }
        [HttpPost]
        public ActionResult RequestExpertEn(RequestExpertEn model)
        {
            return View();
        }


        public JsonResult GetProjects(int id)
        {
           
            var projects = _projectService.GetAllMasterProjects().Where(x => x.ClientId == id);
            return Json(projects.Select(x => new { x.Id, x.Name }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProjectsBd(int id)
        {

            var bdLeadPersonnel = _masterService.GetAllClients().Where(x => x.Id == id).FirstOrDefault();//_userService.FindById(id);
            var bdLead = _userService.FindById(bdLeadPersonnel.BdPersonnelId);
            string name = "";
            if (bdLead != null)
                name = bdLead.FullName + ">" + bdLead.Id;
            return Json(name, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProjectLeads(int id)
        {
            var projectLeads = _projectService.GetProjectLeads(id);

            return Json(projectLeads.Select(x => new { x.EmployeeId, x.FullName }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProjectLead(int id)
        {
            var projectLead = _projectService.GetProjectLeads(id).Where(x=>x.IsActive).FirstOrDefault();
            string name = "";
            if (projectLead != null)
                name = projectLead.FullName+">"+projectLead.EmployeeId;
                
            return Json(name, JsonRequestBehavior.AllowGet);
        }

        

        public ActionResult RequestExpertManual()
        {
            return View();
        }
	}
}