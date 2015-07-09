using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Domain.ApiModels;
using GAPS.TSC.CONS.Services;
using GAPS.TSC.CONS.Domain;



namespace GAPS.TSC.Consillium.Controllers {
    public class HomeController : BaseController {
        private readonly IUserService _userService;

        public HomeController(IUserService userService,IAttachmentService attachmentService)
            : base(attachmentService)
        {
            _userService = userService;
        }
        public ActionResult Index()
        {
            
            return View();
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