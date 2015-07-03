using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.Controllers
{
    public class EmployeesController : Controller
    {
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
            return View();
        }

        [HttpPost]
        public ActionResult AddMembers(TeamMember model)
        {
            return View();
        }

	}
}