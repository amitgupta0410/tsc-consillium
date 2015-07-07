using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Services;
using GAPS.TSC.CONS.Util;
using GAPS.TSC.Consillium.Models;

namespace GAPS.TSC.Consillium.Controllers
{
    public class ProjectsController : BaseController
    {
        //
        // GET: /Projects/
       // public ProjectsController(IAttachmentService attachmentService) : base(attachmentService) {}


          private readonly IUserService _userService;
          private readonly IClientService _clientService;

          public ProjectsController(IUserService userService,IClientService clientService, IAttachmentService attachmentService )
              : base(attachmentService)
          {
            
            _userService = userService;
            _clientService = clientService;
          }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpertRequisition(ExpertRequisitionModel model)
        {
            IEnumerable<SpecialProjectLeadMap> projectLead = new List<SpecialProjectLeadMap>();
            projectLead = _userService.GetAllProjectLeads();
           // model.ProjectLead = projectLead.ToDictionary(x)
            model.StatusOptions = EnumHelper.GetEnumLabels(typeof(RequestStatus));
            model.AssignedList = _userService.GetAllTeamMembers().ToDictionary(x => x.Id, x => x.Name);
            
            model.ClientList = _clientService.GetAllClients().ToDictionary(x => x.Id, x => x.Name);
                   



            return View(model);
        }

        public ActionResult Projects()
        {
            return View();
        }
        public ActionResult Payments()
        {
            return View();
        }
	}
}