using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GAPS.TSC.Consillium.Models;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Services;
using WebGrease.Css.Extensions;

namespace GAPS.TSC.Consillium.Controllers
{
    public class CallsController : BaseController
    {
        private readonly IExpertRequestService _expertRequestService;
        private readonly IExpertService _expertService;
        private readonly IClientService _clientService;
        private readonly IProjectService _projectService;
        private readonly IMainMastersService _mainMastersService;

        public CallsController(IExpertService expertService, IMainMastersService mainMastersService, IExpertRequestService expertRequestService, IAttachmentService attachmentService, IClientService clientService, IProjectService projectService)
            : base(attachmentService)
        {
            _expertService = expertService;
            _expertRequestService = expertRequestService;
            _projectService = projectService;
            _clientService = clientService;
            _mainMastersService = mainMastersService;
        }
        //
        // GET: /Calls/

        public ActionResult CallDashboard(CallDashboardModel model)
        {
            var calls = _expertRequestService.GetAllCalls();
            var currency = _mainMastersService.GetAllCurrencies();
            var expertRequests = _expertRequestService.GetAllExpertsProjects();
            var projects = _projectService.GetAllMasterProjects();
            var clients = _clientService.GetAllClients();
            var projectList = _expertRequestService.GetAllExpertsProjects();
            projectList.ForEach(x =>
            {
                if (!x.ProjectId.HasValue)
                {
                    model.Filter.ProjectList.Add(x.Id, x.ProjectName);
                    if (!model.Filter.ClientList.ContainsKey(x.ClientName))
                        model.Filter.ClientList.Add(x.ClientName, x.ClientName);
                }
                else
                {
                    var project = projects.FirstOrDefault(y => y.Id == x.ProjectId);
                    if (project != null)
                    {
                        model.Filter.ProjectList.Add(x.Id, project.Name);
                        var client = clients.FirstOrDefault(v => v.Id == project.ClientId);
                        if (client != null && !model.Filter.ClientList.ContainsKey(client.Name))
                            model.Filter.ClientList.Add(client.Name, client.Name);
                    }
                }
            });

            if (!String.IsNullOrEmpty(model.Filter.Client))
            {
                var client = clients.FirstOrDefault(x => x.Name == model.Filter.Client);
                if (client != null)
                {
                    var clientProjects = _projectService.GetAllMasterProjects().Where(x => x.ClientId == client.Id).ToList();
                    var expertRequestIds = expertRequests.Where(x => (clientProjects.Any() && x.ProjectId.HasValue &&
                                                  clientProjects.Select(y => y.Id).Contains(x.ProjectId.Value)) ||
                                                 (x.ClientName != null && x.ClientName == model.Filter.Client)).Select(x => x.Id);
                    calls = calls.Where(x => expertRequestIds.Contains(x.ExpertRequestId));

                }
            }

            if (model.Filter.Project.HasValue)
            {
                calls = calls.Where(x => x.ExpertRequestId == model.Filter.Project.Value);
            }

            if (model.Filter.Date.HasValue)
            {
//                calls = calls.Where(x => x.InterviewDate.Date == model.Filter.Date.Value.Date);
                var callsQuery = new List<Call>();
                foreach (var call in calls)
                {
                    if(call.InterviewDate.Date == model.Filter.Date.Value.Date)
                        callsQuery.Add(call);
                }
                calls = callsQuery.AsQueryable();
            }

            model.Calls = calls.Select(Mapper.Map<Call, CallSingleViewModel>).ToList();

            model.Calls.ForEach(x =>
            {
                var currencyModel = currency.FirstOrDefault(y => y.CurrencyId == x.AmountCurrencyId);
                if (currencyModel != null)
                    x.AmountCurrency = currencyModel.CurrencyCode;
            });
            model.Calls.Where(x => x.ProjectId.HasValue && x.ProjectId.Value > 0).ForEach(x =>
            {
                var project = projects.FirstOrDefault(y => y.Id == x.ProjectId.Value);
                if (project != null)
                {
                    x.ExpertRequest = project.Name;
                }
            });
            
            return View(model);
        }
        public ActionResult PaidCallDetails()
        {

            return View();
        }
    }
}