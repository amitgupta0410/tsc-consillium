using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Domain.ApiModels;
using GAPS.TSC.CONS.Repositories;
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
        private readonly IAttachmentService _attachmentService;
        private readonly IExpertService _expertService;
        
        // GET: /Requests/
        public RequestsController(IAttachmentService attachmentService, IMainMastersService mastersService,
            IProjectService projectService, IUserService userService, IExpertRequestService expertRequestService, IClientService clientService, IExpertService expertService)
            : base(attachmentService)
        {
            _userService = userService;
            _masterService = mastersService;
            _projectService = projectService;
            _expertRequestService = expertRequestService;
            _clientService = clientService;
            _attachmentService = attachmentService;
            _expertService = expertService;
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
            int parsedId;
            int.TryParse(model.SearchString, out parsedId);
            foreach (var expertRequest in projects)
            {
                if (expertRequest.ProjectId != null)
                {
                      var projectApi =
                    _projectService.GetAllMasterProjects().FirstOrDefault(x => x.Id == expertRequest.ProjectId);
                      if (projectApi != null) { 
                    expertRequest.ProjectName = projectApi.Name;
                    var client = _clientService.GetAllClients().FirstOrDefault(x => x.Id == projectApi.ClientId);
                    if (client != null)
                        expertRequest.ClientName = client.Name;
                      }
                }
                var team = _userService.GetAllTeamMembers().FirstOrDefault(x => x.Id == expertRequest.AssignedToId);
                if (team != null)
                model.ToAddMembers.Add(team.Name);
            }
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
                projects = projects.Where(x => projectids.Contains(x.ProjectId.GetValueOrDefault())).ToList();
                model.ProjectList =
                    GetProjectsForClientForFilter(model.ClientId.GetValueOrDefault())
                        .ToDictionary(x => x.Id, x => x.ProjectName);
            }
            if (model.Assigned > 0)
            {
                projects = projects.Where(x => x.AssignedToId == model.Assigned);

            }
            if (model.ProjectId != null)
            {
                projects = projects.Where(x => x.Id == model.ProjectId);
            }

            if (!String.IsNullOrEmpty(model.SearchString))
            {


                projects = projects.Where(x => x.ProjectName.Contains(model.SearchString.ToLower())
                                                       || x.ClientName.Contains(model.SearchString.ToLower()) || x.ProjectLeadId == parsedId);
            }
            model.ExpertRequests = projects.Select(Mapper.Map<ExpertRequest, ExpertRequestSingleViewModel>);


            return View(model);
        }

        private IEnumerable<ExpertRequest> GetProjectsForClientForFilter(int clientId)
        {
            var projects = _expertRequestService.GetAllExpertsProjects().ToList();
            var projectids = _projectService.GetAllMasterProjects().Where(x => x.ClientId == clientId).Select(x => x.Id).ToList();
            projects = projects.Where(x => projectids.Contains(x.ProjectId.GetValueOrDefault())).ToList();
            return projects;
        }
        public JsonResult GetClientProjects(int id)
        {
            var projects = GetProjectsForClientForFilter(id);

            return Json(projects.Select(x => new { x.Id, x.ProjectName }), JsonRequestBehavior.AllowGet);
        }



        public ActionResult RequestExpert()
        {
            var model = new ExpertRequestViewModel();
            var projectClients = _projectService.GetAllMasterProjects().Select(x => x.ClientId).Distinct().ToList();
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

        public ActionResult UpdateRequest(int id)
        {
            var expertRequest = _expertRequestService.GetAllExpertsProjects().Single(m => m.Id == id);
            var expertRequestModel = Mapper.Map<ExpertRequest, UpdateExpertRequest>(expertRequest);
            expertRequestModel.CostSharingOptions = EnumHelper.GetEnumLabelValuess(typeof(CostSharingType));
            if (expertRequestModel.ProjectId != 0 && expertRequestModel.ProjectId != null)
            {
                var project = _projectService.GetAllMasterProjects().Single(x => x.Id == expertRequestModel.ProjectId);
                expertRequestModel.ClientId = project.ClientId ?? default(int);
                expertRequestModel.IsRequestExpertManual = false;
            }
            else
            {
                expertRequestModel.ProjectName = expertRequest.ProjectName;
                expertRequestModel.ClientName = expertRequest.ClientName;
                expertRequestModel.BdLeadName = expertRequest.BdLeadName;
                expertRequestModel.IsRequestExpertManual = true;
            }
            expertRequestModel.Id = id;
            expertRequestModel.ProjectList = _projectService.GetAllMasterProjects().Where(x => x.ClientId == expertRequestModel.ClientId).ToDictionary(x => x.Id, x => x.Name);
            expertRequestModel.ProjectLeadList = _userService.GetAllUsers().Where(x => x.Id == expertRequestModel.ProjectLeadId).ToDictionary(x => x.Id, x => x.FullName);
            var projectClients = _projectService.GetAllMasterProjects().Select(x => x.ClientId).Distinct().ToList();
            expertRequestModel.Clients = _masterService.GetAllClients().Where(x => projectClients.Contains(x.Id) && x.IsActive).ToDictionary(x => x.Id, x => x.Name);
            expertRequestModel.Currency = _masterService.GetAllCurrencies().ToDictionary(x => x.CurrencyId, x => x.CurrencyCode);
            expertRequestModel.Units = _masterService.GetAllUnits().ToDictionary(x => x.Id, x => x.Name);
            expertRequestModel.Industry = _masterService.GetAllIndustries().ToDictionary(x => x.Id, x => x.Name);
            expertRequestModel.Geography = _masterService.GetAllGeographies().ToDictionary(x => x.Id, x => x.Name);
            return View(expertRequestModel);
        }

        [HttpPost]
        public ActionResult UpdateRequest(UpdateExpertRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.ApprovalDocumentFile != null)
            {
                var approveFile = UploadAndSave("ApprovalDocumentFile");
                model.ApprovalDocumentId = approveFile.Id;
            }
            if (model.ScopingDocumentFile != null)
            {
                var scopingFile = UploadAndSave("ScopingDocumentFile");
                model.ScopingDocumentId = scopingFile.Id;
            }
            var expertRequest = _expertRequestService.GetAllExpertsProjects().Single(m => m.Id == model.Id);
            expertRequest.ProjectId = model.ProjectId;
            expertRequest.ProjectLeadId = model.ProjectLeadId;
            expertRequest.ScopingDocumentId = model.ScopingDocumentId;
            expertRequest.ApprovalDocumentId = model.ApprovalDocumentId;
            expertRequest.IndustryId = model.IndustryId;
            expertRequest.GeographicId = model.GeographicId;
            expertRequest.CostSharingType = model.CostSharingTypeValue;
            expertRequest.BudgetAmount = model.BudgetAmount;
            expertRequest.BudgetCurrencyId = model.BudgetCurrencyId;
            expertRequest.UnitId = model.UnitId;
            expertRequest.TscShare = model.TscShare;
            expertRequest.ClientShare = model.ClientShare;
            expertRequest.Description = model.Description;
            expertRequest.Comments = model.Comments;

            expertRequest.ProjectName = model.ProjectName;
            expertRequest.ClientName = model.ClientName;
            expertRequest.BdLeadName = model.BdLeadName;

            _expertRequestService.Update(expertRequest);
            SetMessage(MessageType.Success, MessageConstant.GetMessage(Messages.Update));
            return RedirectToAction("UpdateRequest");
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
      
        //public ActionResult RequestExpertManual()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult ProjectDetail(int id)
        {
            var model = new ProjectDetailViewModel();

            var projectMeta = _expertRequestService.GetAllExpertsProjects().FirstOrDefault(x => x.Id == id);
            if (projectMeta != null && projectMeta.ProjectId != null)
            {
                var projectApi =
                    _projectService.GetAllMasterProjects().FirstOrDefault(x => x.Id == projectMeta.ProjectId);

                if (projectApi != null)
                {
                    model.ProjectName = projectApi.Name;
                    var client = _clientService.GetAllClients().FirstOrDefault(x => x.Id == projectApi.ClientId);
                    if (client != null)
                        model.ClientName = client.Name;
                }
            }
            else
            {
                if (projectMeta != null)
                {


                    model.ClientName = projectMeta.ClientName;
                    model.ProjectName = projectMeta.ProjectName;
                }
            }
            if (projectMeta != null)
            {
                //                model.ClientName = projectMeta.ClientName;
                //                model.ProjectName = projectMeta.ProjectName;
                model.Comments = projectMeta.Comments;
                model.CostSharingType = projectMeta.CostSharingType;
                model.AllocatedBudget = projectMeta.BudgetAmount;
                model.StartDate = projectMeta.StartDate;
                model.EndDate = projectMeta.EndDate;
                model.RestartDate = projectMeta.RestartDate;
                model.Description = projectMeta.Description;
                model.Id = id;
               

                model.RequestedDate = projectMeta.CreatedAt;
                var currency =
                    _masterService.GetAllCurrencies().FirstOrDefault(x => x.CurrencyId == projectMeta.BudgetCurrencyId);
                if (currency != null)
                    model.BudgetCurrencyName = currency.CurrencyName;
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

            model.Experts = _expertRequestService.GetExpertsForRequest(id).Select(Mapper.Map<Expert, ExpertViewModel>);

            foreach (var expert in model.Experts)
            {
                var name = _masterService.GetAllGeographies().FirstOrDefault(x => x.Id == expert.GeographicId);

                if (name != null)
                    model.ToAddRegions.Add(name.Name);
                var workExperience = _expertRequestService.GetAllDesignations(expert.Id).OrderByDescending(x => x.StartDate);
                foreach (var experience in workExperience)
                {
                    string designation = experience.Designation;
                    model.ToAddDesignations.Add(designation);
                    string company = experience.Organisation;
                    model.ToAddOrganisations.Add(company);
                    break;


                }
                
            }

            model.ExpertList = _expertService.Get(x => x.DeletedAt == null).ToDictionary(x => x.Id, x => x.Name);

            return View(model);
        }
        [HttpPost]
        public ActionResult ProjectDetail(ProjectDetailViewModel model)
        {
            var projectMeta = _expertRequestService.GetAllExpertsProjects().FirstOrDefault(x => x.Id == model.Id);

            if (projectMeta != null)
            {
                projectMeta.Comments = model.Comments;

            }
            _expertRequestService.Update(projectMeta);
            return RedirectToAction("ProjectDetail");
        }

        [HttpPost]
        public ActionResult ProjectDetailButton(ProjectDetailViewModel model)
        {
            foreach (var expertId in model.ExpertIds)
            {
                _expertRequestService.AddExpertToRequest(model.Id, expertId); 
            }

            return RedirectToAction("ProjectDetail", new { id = model.Id }); 
        }
       
        public ActionResult DeleteExpert( int requestId,int expertId)
        {


            _expertRequestService.RemoveExpertFromRequest(requestId, expertId);


            return RedirectToAction("ProjectDetail", new { id =requestId });
        }
        public ActionResult RequestManual()
        {
            var model = new ExpertRequestViewModel();
            var projectLeadIds = _expertRequestService.GetProjectLeads();
            model.ProjectLeadList = _userService.GetAllUsers().Where(x => projectLeadIds.Contains(x.Id)).ToDictionary(x => x.Id, x => x.FullName);
            model.Industry = _masterService.GetAllIndustries().ToDictionary(x => x.Id, x => x.Name);
            model.Geography = _masterService.GetAllGeographies().ToDictionary(x => x.Id, x => x.Name);
            model.Currency = _masterService.GetAllCurrencies().ToDictionary(x => x.CurrencyId, x => x.CurrencyCode);
            model.Units = _masterService.GetAllUnits().ToDictionary(x => x.Id, x => x.Name);
            model.CostSharingOptions = EnumHelper.GetEnumLabelValuess(typeof(CostSharingType));
            model.IsRequestExpertManual = true;
            return View("RequestExpert", model);
        }


    }
}