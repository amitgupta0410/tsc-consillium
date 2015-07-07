﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Domain.ApiModels;
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
          private readonly IExpertRequestService _expertRequestService;
        private readonly IProjectService _projectService;

          public ProjectsController(IUserService userService,IClientService clientService, IAttachmentService attachmentService,IExpertRequestService expertRequestService,IProjectService projectService)
              : base(attachmentService)
          {
            
            _userService = userService;
            _clientService = clientService;
              _expertRequestService = expertRequestService;
              _projectService = projectService;
          }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ExpertRequisition(ExpertRequisitionModel model)
        {
            IEnumerable<int> leadIds = _expertRequestService.GetProjectLeads();
           model.ProjectLead= _userService.GetAllUsers().Where(x => leadIds.Contains(x.Id)).ToDictionary(x => x.Id, x => x.FullName);
         
            model.StatusOptions = EnumHelper.GetEnumLabels(typeof(RequestStatus));
            model.AssignedList = _userService.GetAllTeamMembers().ToDictionary(x => x.Id, x => x.Name);
            
            model.ClientList = _clientService.GetAllClients().ToDictionary(x => x.Id, x => x.Name);

            model.ExpertRequests = _expertRequestService.GetAllExpertsProjects();
            
          
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