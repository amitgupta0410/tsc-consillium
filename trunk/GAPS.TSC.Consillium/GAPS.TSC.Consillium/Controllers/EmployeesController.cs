﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Services;
using GAPS.TSC.Consillium.Models;
using GAPS.TSC.Consillium.Utils;

namespace GAPS.TSC.Consillium.Controllers
{
    public class EmployeesController : BaseController
    {

      
        private readonly IUserService _userService;

        public EmployeesController(IProjectService projectService,IClientService clientService,IUserService userService,IAttachmentService attachmentService) : base(attachmentService) {
            _projectService = projectService;
            _clientService = clientService;
            _userService = userService;
        }
        //
        // GET: /Employees/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddNewLead()
        {
            return View();
        }
        public ActionResult LeadsDashboard()
        {
            return View();
        }
        public ActionResult ExpertsDasboard()
        {

            return View();
        }
        public ActionResult ProfileView()
        {
            return View();
        }

        public ActionResult AddPls()
        {
            var model = new AddProjectLeadModel();
            model.ProjectLead = _userService.GetAllProjectLeads();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddPls(AddProjectLeadModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IEnumerable<SpecialProjectLeadMap> projectLead = new List<SpecialProjectLeadMap>();
            projectLead = _userService.GetAllProjectLeads().Where(x => x.UserId == model.Id);
            if (projectLead.Count()!=0)
            {
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



        public ActionResult AddMembers()
        {
            var model = new AddMembersToTeam();
            model.Employees = _userService.GetAllTeamMembers();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMembers(TeamMember model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TeamMember teamMember = null;
            IEnumerable<TeamMember> team = new List<TeamMember>();
            if (model.TeamMemberType==TeamMemberType.Internal)
            {
                team = _userService.GetAllTeamMembers().Where(x => x.UserId == model.UserId);
            }
            else
            {
                team = _userService.GetAllTeamMembers().Where(x => x.Name == model.Name);
            }
            if (team.Count() != 0)
            {

                SetMessage(MessageType.Info, MessageConstant.GetMessage(Messages.Duplicate));
                return RedirectToAction("AddMembers");
            }
             
                teamMember =  new TeamMember();
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