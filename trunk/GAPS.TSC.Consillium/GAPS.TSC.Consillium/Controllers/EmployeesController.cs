using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.Consillium.Models;
using GAPS.TSC.CONSILLIUM.Services;
using GAPS.TSC.Consillium.Utils;

namespace GAPS.TSC.Consillium.Controllers
{
    public class EmployeesController : BaseController
    {

        private readonly IProjectService _projectService;
        private readonly IClientService _clientService;
        private readonly IUserService _userService;

        public EmployeesController(IProjectService projectService,IClientService clientService,IUserService userService)
        {
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

            return View();
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
                    
                SetMessage(MessageType.Info, MessageConstant.GetMessage(Messages.ErrorInTimesheet));
                return RedirectToAction("AddMembers");
            }
                teamMember = _userService.GetAllTeamMembers().FirstOrDefault(x => x.Id == model.Id);
                bool isNew = teamMember == null;
                teamMember = teamMember ?? new TeamMember();
                teamMember.UserId = model.UserId;
                teamMember.TeamMemberType = model.TeamMemberType;
                teamMember.Name = model.Name;
                teamMember.IsActive = true;
                if (isNew)
                {
                    _userService.AddTeamMember(teamMember);
                    SetMessage(MessageType.Success, MessageConstant.GetMessage(Messages.Random));
                    return RedirectToAction("AddMembers");
                }
                return RedirectToAction("AddMembers");
        }

	}
}