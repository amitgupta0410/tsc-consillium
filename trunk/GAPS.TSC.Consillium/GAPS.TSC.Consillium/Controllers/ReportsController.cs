﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAPS.TSC.Consillium.Controllers
{
    public class ReportsController : Controller
    {
        //
        // GET: /Reports/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PaidVsFreeInterview()
        {
            return View();
        }
        public ActionResult Interviews()
        {
            return View();
        }
        public ActionResult BillingReports()
        {
            return View();
        }
        public ActionResult ProjectData()
        {
            return View();
        }
        public ActionResult Productivity()
        {
            return View();
        }

        public ActionResult ParticularExpert()
        {
            return View();
        }
    }
}