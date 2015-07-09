using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Domain.ApiModels;
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
        private readonly IClientService _clientService;
      
        // GET: /Requests/
        public RequestsController(IAttachmentService attachmentService, IMainMastersService mastersService,
            IProjectService projectService, IUserService userService, IExpertRequestService expertRequestService, IClientService clientService)
            : base(attachmentService)
        {
            _userService = userService;
            _masterService = mastersService;
            _projectService = projectService;
            _expertRequestService = expertRequestService;
              _clientService = clientService;
        }
        [HttpGet]
        public ActionResult Index(ExpertRequestDashboardViewModel model)
        {
            var leadIds = _expertRequestService.GetProjectLeads().ToList();
            model.ProjectLeadList = _userService.GetAllUsers().ToList().Where(x => leadIds.Contains(x.Id)).ToDictionary(x => x.Id, x => x.FullName);
            model.StatusOptions = EnumHelper.GetEnumLabels(typeof(RequestStatus));
            model.AssignedList = _userService.GetAllTeamMembers().ToDictionary(x => x.Id, x => x.Name);
            model.ClientList = _clientService.GetAllClients().ToDictionary(x => x.Id, x => x.Name);
            model.ProjectList = _projectService.GetAllMasterProjects().ToDictionary(x => x.Id, x => x.Name);
            var projects = _expertRequestService.GetAllExpertsProjects();

            if (model.ProjectLeadId != null)
            {
                projects = projects.Where(x => x.ProjectLeadId == model.ProjectLeadId);
            }

            if (model.Status != null)
            {
                projects = projects.Where(x => x.RequestStatus == model.Status);
            }
            if (model.StartDate != null)
            {
                projects = projects.Where(x => x.StartDate == model.StartDate);
            }
            if (model.EndDate != null)
            {
                projects = projects.Where(x => x.EndDate == model.EndDate);
            }

            if (model.ClientId > 0)
            {
                var projectids = _projectService.GetAllMasterProjects().Where(x => x.ClientId == model.ClientId).Select(x => x.Id);
                projects = projects.Where(x => projectids.Contains(x.Id));
            }
            if (model.Assigned > 0)
            {
                projects = projects.Where(x => x.AssignedToId == model.Assigned);

            }

            if (!String.IsNullOrEmpty(model.SearchString))
            {


                projects = projects.Where(x => x.ProjectName.Contains(model.SearchString.ToLower())
                                                       || x.ClientName.Contains(model.SearchString.ToLower())||model.ProjectLeadList.ContainsValue(model.SearchString.ToLower()));
            }
            model.ExpertRequests = projects.Select(Mapper.Map<ExpertRequest, ExpertRequestSingleViewModel>);
            return View(model);
        }


        public ActionResult RequestExpert()
        {
            var model = new ExpertRequestViewModel();
            var projectClients =
                _projectService.GetAllMasterProjects().Select(x => x.ClientId).Distinct().ToList();
            model.Clients = _masterService.GetAllClients().Where(x => projectClients.Contains(x.Id) && x.IsActive).ToDictionary(x => x.Id, x => x.Name);
            model.Units = _masterService.GetAllUnits().ToDictionary(x => x.Id, x => x.Name);
            model.Industry = _masterService.GetAllIndustries().ToDictionary(x => x.Id, x => x.Name);
            model.Geography = _masterService.GetAllGeographies().ToDictionary(x => x.Id, x => x.Name);
            model.Currency = _masterService.GetAllCurrencies().ToDictionary(x => x.CurrencyId, x => x.CurrencyCode);
            model.CostSharingOptions = EnumHelper.GetEnumLabelValuess(typeof(CostSharingType));
            return View(model);
        }
        [HttpPost]
        public ActionResult RequestExpert(ExpertRequestViewModel model)
        {
            var approveFile = UploadAndSave("ApprovalDocumentFile");
            var scopingFile = UploadAndSave("ScopingDocumentFile");
            var expertRequest = Mapper.Map<ExpertRequestViewModel, ExpertRequest>(model);
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

        public JsonResult GetProjectLeadList(int id)
        {
            var projectLeads = _projectService.GetProjectLeads(id);

            return Json(projectLeads.Select(x => new { x.EmployeeId, x.FullName }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProjectUnit(int id)
        {
            var project = _projectService.GetAllMasterProjects().FirstOrDefault(x => x.Id == id);
            if (project == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            int unitId = project.UnitId ?? default(int);
            var unit = _masterService.FindUnitById(unitId);
                
            return Json(unit, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RequestExpertManual()
        {
            return View();
        }

        public ActionResult ProjectDetail(int id)
        {
            var model = new ProjectDetailViewModel();
            //var clientId = _clientService.GetAllClients().Where(x => x.Id == id).ToDictionary(x => x.Id, x => x.Name);
            var projectMeta = _expertRequestService.GetAllExpertsProjects().FirstOrDefault(x => x.Id== id);
            if (projectMeta != null)
            {
                model.ClientName = projectMeta.ClientName;
                model.ProjectName = projectMeta.ProjectName;
                model.Comments = projectMeta.Comments;
                model.CostSharingType = projectMeta.CostSharingType;
                model.AllocatedBudget = projectMeta.BudgetAmount;
                model.StartDate = projectMeta.StartDate;
                model.EndDate = projectMeta.EndDate;
                model.RestartDate = projectMeta.RestartDate;
                model.Description = projectMeta.Description;
//                model.RestartEndDate = projectMeta.DeletedAt;
                model.RequestedDate = projectMeta.CreatedAt;
                var industry = _masterService.GetAllIndustries().FirstOrDefault(x => x.Id == projectMeta.IndustryId);
                if (industry != null) 
                model.Industry = industry.Name;
                var geographic = _masterService.GetAllGeographies().FirstOrDefault(x => x.Id == projectMeta.GeographicId);
                if (geographic != null)
                    model.Geography = geographic.Name;
                var projectLead = _userService.GetAllUsers().FirstOrDefault(x => x.Id == projectMeta.ProjectLeadId);
                if (projectLead != null)
                model.ProjectLeadName = projectLead.FullName;
                var bdLead = _userService.GetAllUsers().FirstOrDefault(x => x.Id == projectMeta.BdLeadId);
                if (bdLead != null)
                    model.BdLeadName = bdLead.FullName;
            }
          
            return View(model);
        }
        

        public ActionResult RequestManual()
        {
            var model = new ExpertRequestViewModel();
            var projectLeadIds = _expertRequestService.GetProjectLeads();
            model.ProjectLeadList = _userService.GetAllUsers().Where(x => projectLeadIds.Contains(x.Id)).ToDictionary(x=>x.Id,x=>x.FullName);
            model.Industry = _masterService.GetAllIndustries().ToDictionary(x => x.Id, x => x.Name);
            model.Geography = _masterService.GetAllGeographies().ToDictionary(x => x.Id, x => x.Name);
            model.Currency = _masterService.GetAllCurrencies().ToDictionary(x => x.CurrencyId, x => x.CurrencyCode);
            model.Units = _masterService.GetAllUnits().ToDictionary(x => x.Id, x => x.Name);
            model.CostSharingOptions = EnumHelper.GetEnumLabelValuess(typeof(CostSharingType));
            model.IsRequestExpertManual = true;
            return View(model);
        }

        
	}
}