using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using AutoMapper;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Domain.ApiModels;
using GAPS.TSC.CONS.Repositories;
using GAPS.TSC.CONS.Services;
using GAPS.TSC.Consillium.Models;
using GAPS.TSC.Consillium.Utils;
using GAPS.TSC.CONS.Util;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace GAPS.TSC.Consillium.Controllers {
    public class EmployeesController : BaseController {

        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly IMainMastersService _mainMastersService;
        private readonly IExpertService _expertService;
        private readonly IClientService _clientService;
        private readonly IExpertRequestService _expertRequestService;
        private readonly IAttachmentService _attachmentService;

        public EmployeesController(IUserService userService, IAttachmentService attachmentService, IMainMastersService mainMastersService, IExpertService expertService, IClientService clientService, IProjectService projectService, IExpertRequestService expertRequestService)
            : base(attachmentService) {

            _userService = userService;
            _mainMastersService = mainMastersService;
            _expertService = expertService;
            _clientService = clientService;
            _projectService = projectService;
            _expertRequestService = expertRequestService;
            _attachmentService = attachmentService;
        }
        //
        // GET: /Employees/
        public ActionResult Index(ExpertDashboardViewModel model)
        {
            var experts = _expertService.Get(x => x.DeletedAt == null);

            foreach (var expert in experts)
            {

                var workExperience = _expertRequestService.GetAllDesignations(expert.Id);
                foreach (var experience in workExperience)
                {


                    string company = experience.Organisation;
                    model.ToAddOrganisations.Add(company);


                }
            }

            var expertRequest = _expertRequestService.GetAllExpertsProjects();
            foreach (var request in expertRequest)
            {
                if (request.ProjectId != null)
                {
                    var name = _projectService.GetAllMasterProjects().FirstOrDefault(x => x.Id == request.ProjectId);
                    if (name != null)
                        model.ExpertRequestlist.Add(request.Id, name.Name);
                }
                else
                {
                    model.ExpertRequestlist.Add(request.Id, request.ProjectName);

                }
            }

            model.IndustryList = _mainMastersService.GetAllIndustries().ToDictionary(x => x.Id, x => x.Name);
            model.GeographicList = _mainMastersService.GetAllGeographies().ToDictionary(x => x.Id, x => x.Name);

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
                    _projectService.GetAllMasterProjects().FirstOrDefault(x => x.Id == apiProject.ProjectId);

                var name =
                    _clientService.GetAllClients()
                        .FirstOrDefault(x => projectApi != null && x.Id == projectApi.ClientId);
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

                var name = _projectService.GetAllMasterProjects().FirstOrDefault(x => x.Id == apiProject.ProjectId);
                if (name != null)
                    apiNames.Add(name.Name);
            }
            var combineList = mannualNames.Concat(apiNames);
            model.ProjectList = combineList.Distinct().ToDictionary(x => x, x => x);
            if (!String.IsNullOrEmpty(model.ClientName))
            {
                var projectId = 0;
                var client = _clientService.GetAllClients().FirstOrDefault(x => x.Name == model.ClientName);
                if (client != null)
                {
                    var project = _projectService.GetAllMasterProjects().FirstOrDefault(x => x.ClientId == client.Id);


                    if (project != null)
                    {

                        projectId = project.Id;
                    }
                }
                experts =
                    experts.Where(
                        x =>
                            x.ExpertRequests.Select(y => y.ClientName).Contains(model.ClientName) ||
                            (x.ExpertRequests.Select(y => y.ProjectId).Contains(projectId)));
            }


            if (!String.IsNullOrEmpty(model.ProjectName))
            {
                var project = _projectService.GetAllMasterProjects().FirstOrDefault(x => x.Name == model.ProjectName);
                var projectId = 0;
                if (project != null)
                {
                    projectId = project.Id;
                }
                experts =
                    experts.Where(
                        x =>
                            x.ExpertRequests.Select(y => y.ProjectName).Contains(model.ProjectName) ||
                            (x.ExpertRequests.Select(y => y.ProjectId).Contains(projectId)));
            }


            int parsedId;
            int.TryParse(model.SearchString, out parsedId);
            if (!String.IsNullOrEmpty(model.SearchString))
            {

               
                    foreach (var expert in experts)
                    {

                        var workExperience = _expertRequestService.GetAllDesignations(expert.Id);
                        var experince = workExperience.FirstOrDefault(x => x.Organisation == model.SearchString);
                        if (experince != null)
                        {
                            parsedId = experince.Id;
                        }
                        break;
                    }

                    var geographic =
                        _mainMastersService.GetAllGeographies().FirstOrDefault(x => x.Name == model.SearchString);
                    var industry =
                        _mainMastersService.GetAllIndustries().FirstOrDefault(x => x.Name == model.SearchString);
                    if (geographic != null)
                    {
                        parsedId = geographic.Id;

                    }
                    if (industry != null)
                    {
                        parsedId = industry.Id;

                    }
                    experts = experts.Where(x => x.Name.Contains(model.SearchString.ToLower())
                                                 || x.Email.Contains(model.SearchString.ToLower()) ||
                                                 x.GeographicId == parsedId || x.IndustryId == parsedId ||
                                                 x.WorkExperiences.Select(y => y.Id).Contains(parsedId));
                }


                if (model.GeographicId != null)
                {
                    experts = experts.Where(x => x.GeographicId == model.GeographicId);
                }
                if (model.IndustryId != null)
                {
                    experts = experts.Where(x => x.IndustryId == model.IndustryId);
                }
                if (model.ProjectId.HasValue)
                {
                    experts = experts.Where(x => x.ExpertRequests.Select(y => y.Id).Contains(model.ProjectId.Value));
                }

                model.Experts = experts.Select((Mapper.Map<Expert, ExpertSingleViewModel>));

                return View(model);
            }
        

        [HttpPost]
        public ActionResult AddExpertToRequest(ExpertDashboardViewModel model) {
            if (model.Expert != null) {
                if (!ModelState.IsValid) {
                    return View(model);
                }
                foreach (var expertId in model.Expert) {
                    _expertRequestService.AddExpertToRequest(model.RequestId, expertId);
                }

                return RedirectToAction("Index");
            } else {
                SetMessage(MessageType.Info, MessageConstant.GetMessage(Messages.Danger));
                return RedirectToAction("Index");
            }
        }

        public ActionResult AddNewLead(int? id) {

            var model = new AddLeadModel();
            if (id.HasValue) {
                var expert = _expertService.GetById(id.Value);

                if (expert != null) {
                    model = Mapper.Map<Expert, AddLeadModel>(expert);
                    var workexperience = _expertService.GetWorkExperiences(expert.Id);
                    if (expert.ResumeId != null) {
                        var attachment = _attachmentService.GetById(expert.ResumeId.Value);

                        model.FileName = attachment.ActualName;
                        model.FileGuidName = string.Format("{0}{1}", FilePath, attachment.FileName);
                    }
                    if (workexperience != null)
                        model.WorkExperiences = workexperience.Select(Mapper.Map<WorkExperience, WorkExperienceModel>);
                }
            }

            var users = _userService.GetAllTeamMembers().ToList();
            model.RecruiterOptions = users.Where(x => x.UserId == null).ToDictionary(x => x.Id, x => x.Name);
            users = users.Where(x => x.UserId != null).ToList();
            var apiUsers = _userService.GetAllUsers().ToList();
            foreach (var user in users) {
                var userModel = apiUsers.FirstOrDefault(x => x.Id == user.UserId);
                if (userModel != null)
                    model.RecruiterOptions.Add(user.Id, userModel.FullName);
            }

            model.CountryOptions = _mainMastersService.GetAllCountries().ToDictionary(x => x.Id, x => x.Name);
            model.CurrencyOptions = _mainMastersService.GetAllCurrencies().ToDictionary(x => x.CurrencyId, x => x.CurrencyCode);
            model.GeographicOptions = _mainMastersService.GetAllGeographies().ToDictionary(x => x.Id, x => x.Name);
            model.IndustryOptions = _mainMastersService.GetAllIndustries().ToDictionary(x => x.Id, x => x.Name);
            model.TitleOptions = EnumHelper.GetEnumLabels(typeof(TitleOptions));
            model.ExpertTypeOptions = EnumHelper.GetEnumLabels(typeof(ExpertType));
            model.LeadStatusOptions = EnumHelper.GetEnumLabels(typeof(LeadStatus));
            return View(model);
        }


        [HttpPost]
        public ActionResult AddNewLead(AddLeadModel model) {
            //            if (!ModelState.IsValid) return RedirectToAction("AddNewLead");
            var expert = Mapper.Map<AddLeadModel, Expert>(model);
            expert.CreatedAt = DateTime.Now;
            if (model.Id == 0) {
                if (!model.IsLead) {
                    expert.JoiningDate = DateTime.Now;
                }
            }
            if (Request.Files["File"] != null && Request.Files["File"].ContentLength > 0) {
                var file = UploadAndSave("File");
                expert.ResumeId = file.Id;
            }
            var result = model.Id == 0 ? _expertService.Add(expert) : _expertService.Update(expert);
            if (result == null) return RedirectToAction("AddNewLead");
            SetMessage(MessageType.Success, MessageConstant.GetMessage(Messages.AddLeadSuccess));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ConvertLead(AddLeadModel model) {
            var expert = _expertService.GetById(model.Id);
            if (expert == null) return new HttpNotFoundResult();
            expert.JoiningDate = model.JoiningDate;
            expert.ExpertNotes.Add(new ExpertNote() {
                Content = model.JoiningNotes,
                ExpertId = model.Id,
                //todo: change it with logged in user
                TeamMemberId = 1,
            });
            var result = _expertService.Update(expert);
            if (result != null)
                SetMessage(MessageType.Success, MessageConstant.GetMessage(Messages.ConvertLead));
            return RedirectToAction("AddNewLead", new { id = result.Id });
        }

        public ActionResult DeleteLead(int id) {
            var expert = _expertService.GetById(id);
            expert.DeletedAt = DateTime.Now;
            var result = _expertService.Update(expert);
            if (result != null)
                SetMessage(MessageType.Success, MessageConstant.GetMessage(Messages.DeleteLead));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult LeadNameExist(string Name, int Id) {
            var result = _expertService.LeadNameExist(Name, Id);
            return Json(result);
        }

        [HttpPost]
        public JsonResult EmailExist(string Email, int Id) {
            var result = _expertService.EmailExist(Email, Id);
            return Json(result);
        }

        [HttpPost]
        public ActionResult AddExperience(AddLeadModel model) {
            //            var expert = _expertService.GetById(model.Id);
            var workExperience = Mapper.Map<AddLeadModel, WorkExperience>(model);
            var result = _expertService.AddExperience(model.Id, workExperience);

            return RedirectToAction("AddNewLead", new { id = model.Id });
        }

        public ActionResult DeleteWork(int id) {
            var result = _expertService.DeleteWorkExperience(id);
            return RedirectToAction("AddNewLead", new { id = result });
        }

        public ActionResult EditWork(int id) {
            var experience = _expertService.GetWorkExperienceById(id);
            var model = Mapper.Map<WorkExperience, WorkExperienceModel>(experience);
            return PartialView("_EditWorkExperience", model);
        }

        [HttpPost]
        public ActionResult UpdateWork(WorkExperienceModel model) {
            var toUpdate = Mapper.Map<WorkExperienceModel, WorkExperience>(model);
            var result = _expertService.EditWorkExperience(toUpdate);
            return RedirectToAction("AddNewLead", new { id = result });
        }

        public ActionResult ConvertToExpert(int id) {
            var expert = _expertService.GetById(id);
            if (expert == null) return new HttpNotFoundResult();
            var model = Mapper.Map<Expert, AddLeadModel>(expert);
            return PartialView("_ConvertToExpert", model);
        }

        public ActionResult LeadsDashboard() {
            return View();
        }
        public ActionResult ExpertsDasboard() {

            return View();
        }
        public ActionResult ProfileView(int id) {
            var expert = _expertService.GetById(id);
            if (expert == null) return RedirectToAction("Index");

            var model = Mapper.Map<Expert, ProfileViewModel>(expert);
            var projects = _projectService.GetAllMasterProjects();
            var projectList = _expertRequestService.GetAllExpertsProjects();
            projectList.ForEach(x => {
                if (!x.ProjectId.HasValue)
                    model.ProjectList.Add(x.Id, x.ProjectName);
                else {
                    var project = projects.FirstOrDefault(y => y.Id == x.ProjectId);
                    if (project != null)
                        model.ProjectList.Add(x.Id, project.Name);
                }
            });
            var recruiter = _userService.GetTeamMemberById(expert.RecruiterId);
            model.RecruiterName = recruiter.UserId.HasValue ? _userService.FindById(recruiter.UserId.Value).FullName : recruiter.Name;
            var currency = _mainMastersService.GetAllCurrencies().FirstOrDefault(x => x.CurrencyId == expert.FeesCurrencyId);
            if (currency != null)
                model.FeesCurrency = currency.CurrencyCode;
            var country = _mainMastersService.GetAllCountries().FirstOrDefault(x => x.Id == expert.CountryId);
            if (country != null)
                model.Country = country.Name;
            var geography = _mainMastersService.GetAllGeographies().FirstOrDefault(x => x.Id == expert.GeographicId);
            if (geography != null)
                model.Geography = geography.Name;
            var industry = _mainMastersService.GetAllIndustries().FirstOrDefault(x => x.Id == expert.IndustryId);
            if (industry != null)
                model.Industry = industry.Name;
            if (expert.ExpertNotes != null)
                model.ExpertNoteModels = expert.ExpertNotes.Select(Mapper.Map<ExpertNote, ExpertNoteModel>);
            if (expert.WorkExperiences != null)
                model.WorkExperienceModels = expert.WorkExperiences.Select(Mapper.Map<WorkExperience, WorkExperienceModel>);
            var calls = _expertService.GetCallsForExperts(id).ToList();
            if (!calls.Any()) return View(model);
            var callModels = calls.Select(Mapper.Map<Call, ExpertCallsModel>).ToArray();

            for (int i = 0; i < calls.Count(); i++) {
                var call = calls.FirstOrDefault(x => x.Id == callModels[i].Id);
                if (call == null) continue;
                if (call.ExpertRequest.ProjectId == null) {
                    callModels[i].ExpertRequestName = call.ExpertRequest.ProjectName;
                } else {
                    var project = projects.FirstOrDefault(x => x.Id == call.ExpertRequest.ProjectId);
                    if (project != null)
                        callModels[i].ExpertRequestName = project.Name;
                }
            }
            model.ExpertCallsModels = callModels;
            return View(model);
        }

        [HttpPost]
        public ActionResult UploadCv(ProfileViewModel model) {
            var expert = _expertService.GetById(model.Id);
            if (model.File != null && model.File.ContentLength > 0) {
                var file = UploadAndSave("File");
                expert.ResumeId = file.Id;
            }

            SetMessage(MessageType.Success, MessageConstant.GetMessage(Messages.AddCvSuccess));
            return RedirectToAction("Profileview", new { id = expert.Id });
        }

        [HttpPost]
        public ActionResult AssignProject(ProfileViewModel model) {
            var expert = _expertService.GetById(model.Id);
            if (expert == null) return HttpNotFound();
            var request = _expertRequestService.GetById(model.ProjectAssignedId);
            request.Experts.Add(expert);
            var result = _expertRequestService.Update(request);
            SetMessage(MessageType.Success, MessageConstant.GetMessage(Messages.ProjectAssigned));
            return RedirectToAction("Profileview", new { id = expert.Id });
        }

        [HttpPost]
        public ActionResult AddNote(ProfileViewModel model) {
            var note = new ExpertNote() {
                Content = model.NoteToAdd,
                CreatedAt = DateTime.Now,
                //todo: This will be the logged in userId
                TeamMemberId = 1,
                ExpertId = model.Id
            };
            var result = _expertService.AddNote(note);
            return RedirectToAction("ProfileView", new { id = result });
        }

        public ActionResult AddPls() {
            var model = new AddProjectLeadModel();
            model.ProjectLead = _userService.GetAllProjectLeads();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddPls(AddProjectLeadModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            IEnumerable<SpecialProjectLeadMap> projectLead = new List<SpecialProjectLeadMap>();
            projectLead = _userService.GetAllProjectLeads().Where(x => x.UserId == model.Id);
            if (projectLead.Count() != 0) {
                SetMessage(MessageType.Info, MessageConstant.GetMessage(Messages.Duplicate));
                return RedirectToAction("AddPls");
            }

            var projectLeadAdd = new SpecialProjectLeadMap();
            projectLeadAdd.UserId = model.Id;
            projectLeadAdd.IsActive = true;
            _userService.AddSpecialProjectLeads(projectLeadAdd);
            SetMessage(MessageType.Success, MessageConstant.GetMessage(Messages.RequestSuccess));
            return RedirectToAction("AddPls");
        }



        public ActionResult AddMembers() {
            var model = new AddMembersToTeam {
                UserOptions =
                    _userService.GetAllUsers().ToDictionary(x => x.Id, x => string.Format("{0}({1})", x.FullName, x.Id)),
                Employees = _userService.GetAllTeamMembers()
            };
            var users = _userService.GetAllUsers();

            model.Employees.ForEach(x => {
                var userModel = users.FirstOrDefault(y => y.Id == x.UserId);
                if (userModel != null)
                    x.Name = x.UserId != null ? userModel.FullName : x.Name;
            });
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMembers(TeamMember model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            var team = model.TeamMemberType == TeamMemberType.Internal ? _userService.GetAllTeamMembers().Where(x => x.UserId == model.UserId) : _userService.GetAllTeamMembers().Where(x => x.Name == model.Name);
            if (team.Count() != 0) {
                SetMessage(MessageType.Info, MessageConstant.GetMessage(Messages.Duplicate));
                return RedirectToAction("AddMembers");
            }

            var teamMember = new TeamMember {
                UserId = model.UserId,
                TeamMemberType = model.TeamMemberType,
                Name = model.Name,
                IsActive = true
            };

            _userService.AddTeamMember(teamMember);
            SetMessage(MessageType.Success, MessageConstant.GetMessage(Messages.RequestSuccess));
            return RedirectToAction("AddMembers");
        }

    }
}