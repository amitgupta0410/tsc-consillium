using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Services;
using GAPS.TSC.CONS.Util;
using GAPS.TSC.Consillium.Models;
//using GAPS.TSC.CONSILLIUM.Services;
using GAPS.TSC.Consillium.Utils;

namespace GAPS.TSC.Consillium.Controllers
{
    public class RequestsController : BaseController
    {

        private readonly IUserService _userService;
        private readonly IMainMastersService _masterService;
        private readonly IProjectService _projectService;
        private readonly IExpertRequestService _expertRequestService;
      
        // GET: /Requests/
        public RequestsController(IAttachmentService attachmentService, IMainMastersService mastersService,
            IProjectService projectService, IUserService userService,IExpertRequestService expertRequestService) : base(attachmentService)
        {
           _userService= userService;
            _masterService = mastersService;
            _projectService = projectService;
            _expertRequestService = expertRequestService;
        }

        public ActionResult RequestExpert()
        {
            var model = new ExpertRequestViewModel();
            var projectClients =
                _projectService.GetAllMasterProjects().Select(x => x.ClientId).Distinct().ToList();
            model.Clients =_masterService.GetAllClients().Where(x => projectClients.Contains(x.Id) && x.IsActive).ToDictionary(x => x.Id, x => x.Name);
            model.Units = _masterService.GetAllUnits().ToDictionary(x => x.Id, x => x.Name);
            model.Industry=_masterService.GetAllIndustries().ToDictionary(x => x.Id, x => x.Name);
            model.Geography = _masterService.GetAllGeographies().ToDictionary(x => x.Id, x => x.Name);
            model.Currency = _masterService.GetAllCurrencies().ToDictionary(x => x.CurrencyId, x => x.CurrencyCode);
            model.CostSharingOptions = EnumHelper.GetEnumLabels(typeof(CostSharingType));
            return View(model);
        }
        [HttpPost]
        public ActionResult RequestExpert(ExpertRequestViewModel model)
        {
            var approveFile = UploadAndSave("ApprovalDocumentFile");
            var scopingFile = UploadAndSave("ScopingDocumentFile");
            var expertRequest = Mapper.Map<ExpertRequestViewModel,ExpertRequest>(model);
            expertRequest.ApprovalDocumentId = approveFile.Id;
            expertRequest.ScopingDocumentId = scopingFile.Id;
           _expertRequestService.Add(expertRequest);
            SetMessage(MessageType.Success, MessageConstant.GetMessage(Messages.RequestSuccess));
            return RedirectToAction("RequestExpert");
        }


        public JsonResult GetProjects(int id)
        {
            var projects = _projectService.GetAllMasterProjects().Where(x => x.ClientId == id);
            return Json(projects.Select(x => new { x.Id, x.Name }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProjectsBd(int id)
        {
            var client = _masterService.GetAllClients().FirstOrDefault(x => x.Id == id);
            if (client != null)
            {
                var bdLead = _userService.FindById(client.BdPersonnelId);
                if (bdLead != null)
                    return Json(bdLead, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult GetProjectLeads(int id)
        {
            var projectLeads = _projectService.GetProjectLeads(id);

            return Json(projectLeads.Select(x => new { x.EmployeeId, x.FullName }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProjectUnit(int id)
        {
            var project = _projectService.GetAllMasterProjects().FirstOrDefault(x => x.Id == id);
            if (project == null) {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            int unitId = project.UnitId ?? default(int);
            var unit = _masterService.FindUnitById(unitId);
                
            return Json(unit,JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult RequestExpertManual()
        {
            return View();
        }
	}
}