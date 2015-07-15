using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
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

        public EmployeesController(IUserService userService, IAttachmentService attachmentService, IMainMastersService mainMastersService, IExpertService expertService, IClientService clientService, IProjectService projectService , IExpertRequestService expertRequestService)
            : base(attachmentService) {

            _userService = userService;
            _mainMastersService = mainMastersService;
            _expertService = expertService;
            _clientService = clientService;
            _projectService = projectService;
            _expertRequestService = expertRequestService;
            }
        //
        // GET: /Employees/
        public ActionResult Index(ExpertDashboardViewModel model)
        {
            var experts = _expertService.Get(x => x.DeletedAt == null);
            var expertRequests = experts.Select(x => x.ExpertRequests);

            model.IndustryList = _mainMastersService.GetAllIndustries().ToDictionary(x => x.Id, x => x.Name);
            model.GeographicList = _mainMastersService.GetAllGeographies().ToDictionary(x => x.Id, x =>x.Name);
            model.ClientList = _clientService.GetAllClients().ToDictionary(x => x.Id, x => x.Name);
//            model.ProjectList = _projectService.GetAllMasterProjects().ToDictionary(x => x.Id, x => x.Name);
            model.ProjectList= _expertRequestService.GetAllExpertsProjects()
                .ToDictionary(x => x.Id, x => x.ProjectName);
            int parsedId;
            int.TryParse(model.SearchString, out parsedId);
            if (!String.IsNullOrEmpty(model.SearchString))
            {


                experts = experts.Where(x => x.Name.Contains(model.SearchString.ToLower())
                                                       || x.Email.Contains(model.SearchString.ToLower())||x.GeographicId==parsedId||x.IndustryId==parsedId);
            }
            if (model.GeographicId!= null)
            {
                experts = experts.Where(x => x.GeographicId == model.GeographicId);
            }
            if (model.IndustryId != null)
            {
                experts = experts.Where(x => x.IndustryId == model.IndustryId);
            }
            if (model.ProjectId.HasValue)
            {
              experts = experts.Where(x =>x.ExpertRequests.Select(y=>y.Id).Contains(model.ProjectId.Value));
            }

            model.Experts = experts.Select((Mapper.Map<Expert, ExpertSingleViewModel>));
             
            return View(model);
        }

        public ActionResult AddNewLead(int? id) {

            var model = new AddLeadModel();
            if (id.HasValue) {
                var expert = _expertService.GetById(id.Value);
                if (expert != null) {
                    model = Mapper.Map<Expert, AddLeadModel>(expert);
                    var workexperience = _expertService.GetWorkExperiences(expert.Id);
                    if (workexperience != null)
                        model.WorkExperiences = workexperience.Select(Mapper.Map<WorkExperience, WorkExperienceModel>);
                }
            }

            model.CountryOptions = _mainMastersService.GetAllCountries().ToDictionary(x => x.Id, x => x.Name);
            model.CurrencyOptions = _mainMastersService.GetAllCurrencies().ToDictionary(x => x.CurrencyId, x => x.CurrencyName);
            model.RecruiterOptions = _userService.GetAllTeamMembers().ToDictionary(x => x.Id, x => x.Name);
            model.TitleOptions = EnumHelper.GetEnumLabels(typeof(TitleOptions));
            model.ExpertTypeOptions = EnumHelper.GetEnumLabels(typeof(ExpertType));
            model.LeadStatusOptions = EnumHelper.GetEnumLabels(typeof(LeadStatus));
            return View(model);
        }


        [HttpPost]
        public ActionResult AddNewLead(AddLeadModel model) {
            if (!ModelState.IsValid) return RedirectToAction("AddNewLead");

            var expert = Mapper.Map<AddLeadModel, Expert>(model);
            expert.CreatedAt = DateTime.Now;
            if (model.Id == 0) {
                if (!model.IsLead) {
                    expert.JoiningDate = DateTime.Now;
                }
            }
            if (Request.Files["File"] != null && Request.Files["File"].ContentLength > 0) {
                //                var file = UploadAndSave("File");
                expert.ResumeId = 2;
            }
            var result = model.Id == 0 ? _expertService.Add(expert) : _expertService.Update(expert);
            //            if (result)
            //                return RedirectToAction("Index");
            return RedirectToAction("AddNewLead", new { id = result.Id });
        }


        public ActionResult ConvertLead(int id) {
            var expert = _expertService.GetById(id);
            expert.JoiningDate = DateTime.Now;
            var result = _expertService.Update(expert);
            return RedirectToAction("AddNewLead", new { id = result.Id });
        }

        public ActionResult DeleteLead(int id) {
            var expert = _expertService.GetById(id);
            expert.DeletedAt = DateTime.Now;
            var result = _expertService.Update(expert);
            return RedirectToAction("AddNewLead");
        }

        [HttpPost]
        public JsonResult LeadNameExist(string Name) {
            var result = _expertService.LeadNameExist(Name);
            return Json(result);
        }

        [HttpPost]
        public JsonResult EmailExist(string Email) {
            var result = _expertService.EmailExist(Email);
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


        public ActionResult LeadsDashboard() {
            return View();
        }
        public ActionResult ExpertsDasboard() {

            return View();
        }
        public ActionResult ProfileView() {
            return View();
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
            var model = new AddMembersToTeam();
            model.Employees = _userService.GetAllTeamMembers();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMembers(TeamMember model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            TeamMember teamMember = null;
            IEnumerable<TeamMember> team = new List<TeamMember>();
            if (model.TeamMemberType == TeamMemberType.Internal) {
                team = _userService.GetAllTeamMembers().Where(x => x.UserId == model.UserId);
            } else {
                team = _userService.GetAllTeamMembers().Where(x => x.Name == model.Name);
            }
            if (team.Count() != 0) {

                SetMessage(MessageType.Info, MessageConstant.GetMessage(Messages.Duplicate));
                return RedirectToAction("AddMembers");
            }
            teamMember = new TeamMember();
            teamMember.UserId = model.UserId;
            teamMember.TeamMemberType = model.TeamMemberType;
            teamMember.Name = model.Name;
            teamMember.IsActive = true;

            _userService.AddTeamMember(teamMember);
            SetMessage(MessageType.Success, MessageConstant.GetMessage(Messages.RequestSuccess));
            return RedirectToAction("AddMembers");
        }

    }
}