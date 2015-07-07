using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Services;
using GAPS.TSC.CONS.Util;
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
            model.Units = _masterService.GetAllUnits().ToDictionary(x => x.Id, x => x.Name);
            model.Industry=_masterService.GetAllIndustries().ToDictionary(x => x.Id, x => x.Name);
            model.Geography = _masterService.GetAllGeographies().ToDictionary(x => x.Id, x => x.Name);
            model.Currency = _masterService.GetAllCurrencies().ToDictionary(x => x.CurrencyId, x => x.CurrencyName);
            model.CostSharingOptions = EnumHelper.GetEnumLabels(typeof(CostSharingType));
            return View(model);
        }
        [HttpPost]
        public ActionResult RequestExpertEn(RequestExpertEn model)
        {
            var csvFile = Request.Files["ApprovalDocumentFile"];
            return View();
        }


        public JsonResult GetProjects(int id)
        {
            var projects = _projectService.GetAllMasterProjects().Where(x => x.ClientId == id);
            return Json(projects.Select(x => new { x.Id, x.Name }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProjectsBd(int id)
        {

            var client = _masterService.GetAllClients().FirstOrDefault(x => x.Id == id);
            var bdLead = _userService.FindById(client.BdPersonnelId);
            string name = "";
            if (bdLead != null)
                name = bdLead.FullName + ">" + bdLead.Id;
            return Json(name, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProjectLead(int id)
        {
            var projectLead = _projectService.GetProjectLeads(id).FirstOrDefault(x => x.IsActive);
            string name = "";
            if (projectLead != null)
                name = projectLead.FullName+">"+projectLead.EmployeeId;
            return Json(name, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProjectUnit(int id)
        {
            var project = _projectService.GetAllMasterProjects().FirstOrDefault(x => x.Id == id);
           
            if (project != null)
            {
                int unitId = project.UnitId ?? default(int);
                var unit = _masterService.FindUnitById(unitId);
                return Json(unit);

            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult RequestExpertManual()
        {
            return View();
        }
	}
}