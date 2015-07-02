using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Domain.ApiModels;
using GAPS.TSC.CONSILLIUM.Services;
using GAPS.TSC.CONS.Domain;



namespace GAPS.TSC.Consillium.Controllers {
    public class HomeController : Controller {

        private readonly IProjectService _projectService;
        private readonly IClientService _clientService;
        private readonly IUserService _userService;
        private readonly IMainMastersService _mainMastersService;

        public HomeController(IProjectService projectService, IClientService clientService, IUserService userService, IMainMastersService mastersService)
            : base()
        {
            _projectService = projectService;
            _clientService = clientService;
            _userService = userService;
            _mainMastersService = mastersService;
        }
        public ActionResult Index(Employees employees)
        {
            employees.employees = _userService.GetAllUsers();

            return View(employees);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}