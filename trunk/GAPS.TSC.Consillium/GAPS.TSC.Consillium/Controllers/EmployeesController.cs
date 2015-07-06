using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Domain.ApiModels;
using GAPS.TSC.Consillium.Models;
using GAPS.TSC.CONSILLIUM.Services;

namespace GAPS.TSC.Consillium.Controllers
{
    public class EmployeesController : Controller
    {
         private readonly IProjectService _projectService;
         private readonly IClientService _clientService;
         private readonly IUserService _userService;
         private readonly IMainMastersService _mainMastersService;
      
        // GET: /Projects/

        public EmployeesController(IProjectService projectService, IClientService clientService, IUserService userService,
          IMainMastersService mainMastersService )
            : base() {
            _projectService = projectService;
            _clientService = clientService;
            _userService = userService;
          
            _mainMastersService = mainMastersService;
           
        }
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
            return View(model);
        }

        [HttpPost]
        public ActionResult AddPls(AddProjectLeadModel model)
        {
            SpecialProjectLeadMap projectLead = new SpecialProjectLeadMap();
            projectLead.UserId = model.Id;
            projectLead.IsActive = true;
            _userService.AddSpecialProjectLeads(projectLead);
         return View();
        }



        public ActionResult AddMembers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMembers(TeamMember model)
        {
            return View();
        }

	}
}