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
        private readonly IExpertService _expertService;
        private readonly IExpertRequestService _expertRequestService;
        private readonly IClientService _clientService;

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
            _expertService = expertService;
        }
        [HttpGet]
        public ActionResult Index(ExpertRequestDashboardViewModel model)
        {
            var leadIds = _expertRequestService.GetProjectLeads().ToList();

            model.ProjectLeadList = _userService.GetAllUsers().ToList().Where(x => leadIds.Contains(x.Id)).ToDictionary(x => x.Id, x => x.FullName);

            model.StatusOptions = EnumHelper.GetEnumLabels(typeof(RequestStatus));
            model.AssignedList = _userService.GetAllTeamMembers().ToDictionary(x => x.Id, x => x.Name);
          
            var mannualProjects = _expertRequestService.GetAllExpertsProjects().Where(x => x.ProjectId == null)
               .ToDictionary(x => x.Id, x => x.ProjectName);
            var mannualProjectsclients = _expertRequestService.GetAllExpertsProjects().Where(x => x.ProjectId == null)
               .ToDictionary(x => x.Id, x => x.ClientName);
            var apiProjects = _expertRequestService.GetAllExpertsProjects().Where(x => x.ProjectId != null);

            List<string> mannualClients = new List<string>();
            foreach (var mannualProjectclient in mannualProjectsclients)
            {
                mannualClients.Add(mannualProjectclient.Value);

            }
            List<string> apiClients = new List<string>();

            foreach (var apiProject in apiProjects)
            {
                var projectApi =
                   _projectService.GetAllMasterProjects().FirstOrDefault(x => apiProject != null && x.Id == apiProject.ProjectId);

                var name = _clientService.GetAllClients().FirstOrDefault(x =>projectApi != null && x.Id == projectApi.ClientId);
                if (name != null)
                    apiClients.Add(name.Name);
            }
            var combineClientList = mannualClients.Concat(apiClients);
            model.ClientList = combineClientList.Distinct().ToDictionary(x => x, x => x);
            List<string> mannualNames = new List<string>();
            foreach (var mannualProject in mannualProjects)
            {
                mannualNames.Add(mannualProject.Value);

            }

            List<string> apiNames = new List<string>();
            foreach (var apiProject in apiProjects)
            {

                var name = _projectService.GetAllMasterProjects().FirstOrDefault(x => apiProject != null && x.Id == apiProject.ProjectId);
                if (name != null)
                    apiNames.Add(name.Name);
            }
            var combineList = mannualNames.Concat(apiNames);
            model.ProjectList = combineList.Distinct().ToDictionary(x => x, x => x);



            var projects = _expertRequestService.GetAllExpertsProjects();

            foreach (var expertRequest in projects)
            {
                if (expertRequest.ProjectId != null)
                {
                    var projectApi =
                  _projectService.GetAllMasterProjects().FirstOrDefault(x => x.Id == expertRequest.ProjectId);
                    if (projectApi != null)
                    {
                        expertRequest.ProjectName = projectApi.Name;
                        var client = _clientService.GetAllClients().FirstOrDefault(x => x.Id == projectApi.ClientId);
                        if (client != null)
                            expertRequest.ClientName = client.Name;
                    }
                }
            }



            int parsedProjectId = 0;
            int.TryParse(model.SearchString, out parsedProjectId);
            if (!String.IsNullOrEmpty(model.ClientName))
            {
               
                var client = _clientService.GetAllClients().FirstOrDefault(x => x.Name == model.ClientName);
                if (client != null)
                {
                    var project = _projectService.GetAllMasterProjects().FirstOrDefault(x => x.ClientId == client.Id);


                    if (project != null)
                    {

                        parsedProjectId = project.Id;
                    }
                }
                projects = projects.Where(x => (x.ClientName != null && x.ClientName.Contains(model.ClientName)) || (x.ProjectId.HasValue && x.ProjectId == parsedProjectId));
            }

            int parsedClientId = 0;
            int.TryParse(model.SearchString, out parsedClientId);
            if (!String.IsNullOrEmpty(model.ProjectName))
            {
                var project = _projectService.GetAllMasterProjects().FirstOrDefault(x => x.Name == model.ProjectName);

                if (project != null)
                {
                    parsedClientId = project.Id;
                }
                projects =
                    projects.Where(
                        x =>
                          (x.ProjectName != null && x.ProjectName.Contains(model.ProjectName)) ||
                            (x.ProjectId.HasValue && x.ProjectId == parsedClientId));
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


            if (model.Assigned > 0)
            {
                projects = projects.Where(x => x.AssignedToId == model.Assigned);

            }
            if (model.ProjectLeadId != null)
            {
                projects = projects.Where(x => x.ProjectLeadId == model.ProjectLeadId);

            }
          
            int parsedId;
            int.TryParse(model.SearchString, out parsedId);
            if (model.SearchString!=null && !String.IsNullOrEmpty(model.SearchString.ToLower()))
            {

                var project = _projectService.GetAllMasterProjects().FirstOrDefault(x => x.Name == model.SearchString);
                var projectLead = _userService.GetAllUsers().FirstOrDefault(x => x.FullName == model.SearchString);
                var client = _clientService.GetAllClients().FirstOrDefault(x => x.Name == model.SearchString);
                if (project != null)
                {
                    parsedId = project.Id;

                }
                if (projectLead != null)
                {
                    parsedId = projectLead.Id;

                }
                if (client != null)
                {
                    
                      var projectclient = _projectService.GetAllMasterProjects().FirstOrDefault(x => x.ClientId == client.Id);


                    if (projectclient != null)
                    {

                        parsedId = projectclient.Id;
                    }

                }
              
                projects = projects.Where(x => x.ProjectName != null && x.ProjectName.Contains(model.SearchString.ToLower())
                                                       || (x.ClientName != null && x.ClientName.Contains(model.SearchString.ToLower())) || (x.ProjectId.HasValue && x.ProjectId == parsedId) || (x.ProjectLeadId.HasValue && x.ProjectLeadId == parsedId) );
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
            expertRequest.CostSharingType = model.CostSharingTypeValue;
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
        public ActionResult ProjectDetailAddExpert(ProjectDetailViewModel model)
        {
            foreach (var expertId in model.ExpertIds)
            {
                _expertRequestService.AddExpertToRequest(model.Id, expertId);
            }

            return RedirectToAction("ProjectDetail", new { id = model.Id });
        }

        public ActionResult DeleteExpert(int requestId, int expertId)
        {


            _expertRequestService.RemoveExpertFromRequest(requestId, expertId);


            return RedirectToAction("ProjectDetail", new { id = requestId });
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

        public ActionResult Calls(int id)
        {
            var model = new CallsViewModel
            {
                ExpertList = _expertRequestService.GetExpertsForRequest(id).ToDictionary(x => x.Id, x => x.Name)
            };
            var expertRequest = _expertRequestService.GetAllExpertsProjects().FirstOrDefault(x => x.Id == id);
            if (expertRequest != null)
            {
                model.GeographicId = expertRequest.GeographicId;
                model.ExpertRequestId = id;
                model.CostBorneBy = expertRequest.CostSharingType;
            }

            model.PaymentModeDictionary = _expertRequestService.GetAllPayments().ToDictionary(x => x.Id, x => x.Name);
            var teamMembers = _userService.GetAllTeamMembers().ToList();
            model.TeamMembers = teamMembers.Where(x => x.UserId == null).ToDictionary(x => x.Id, x => x.Name);
            teamMembers = teamMembers.Where(x => x.UserId != null).ToList();
            var apiUsers = _userService.GetAllUsers().ToList();
            foreach (var teamMember in teamMembers)
            {
                var teamModel = apiUsers.FirstOrDefault(x => x.Id == teamMember.UserId);
                if(teamModel!=null)
                model.TeamMembers.Add(teamMember.Id,teamModel.FullName);
            }
            model.CallTypeOptions = EnumHelper.GetEnumLabelValuess(typeof(CallType));
            model.CostSharingOptions = EnumHelper.GetEnumLabelValuess(typeof(CostSharingType));
            model.Geography = _masterService.GetAllGeographies().ToDictionary(x => x.Id, x => x.Name);
            model.Currency = _masterService.GetAllCurrencies().ToDictionary(x => x.CurrencyId, x => x.CurrencyCode);
            model.PaymentStatusOptions = EnumHelper.GetEnumLabelValuess(typeof(PaymentStatus));
            model.ExpertCalls = _expertRequestService.GetCallsForRequest(id).Select(Mapper.Map<Call, CallsExpertViewModel>);
            return View(model);
        }

        [HttpPost]
        public ActionResult Calls(CallsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var call = Mapper.Map<CallsViewModel, Call>(model);
            _expertRequestService.AddCallsToRequest(model.ExpertRequestId, call);
            SetMessage(MessageType.Success, MessageConstant.GetMessage(Messages.RequestSuccess));
            return RedirectToAction("Calls");
        }
        [HttpPost]
        public FileResult ExportCallDetails(CallsViewModel model)
        {
            var calls = _expertRequestService.GetCallsForRequest(model.ExpertRequestId).Select(Mapper.Map<Call, CallsExpertViewModel>);
            return DownloadCsv(calls, "Calls.csv", new CallsExpertMapModel());
        }
        public JsonResult GetHonorarium(int expertReqId, int expertId)
        {
            var expert = _expertService.GetById(expertId);
            return Json(new { expert.Id, expert.FeesAmount, expert.FeesCurrencyId }, JsonRequestBehavior.AllowGet);
        }


    }
}