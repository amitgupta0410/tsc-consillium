﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Domain.ApiModels;

namespace GAPS.TSC.Consillium.Models
{
    public class ExpertRequestDashboardViewModel
    {

        public ExpertRequestDashboardViewModel()
        {
            ProjectLeadList = new Dictionary<int, string>();
            AssignedList = new Dictionary<int, string>();
            ProjectList=new Dictionary<int, string>();
            ClientList = new Dictionary<int, string>();

        }
        public IEnumerable<ExpertRequestSingleViewModel> ExpertRequests { get; set; }
        public Dictionary<int, string> ProjectLeadList { get; set; }
        public Dictionary<int, string> ProjectList { get; set; }
        public Dictionary<int, string> AssignedList { get; set; }
        public IDictionary<string, string> StatusOptions { get; set; }
        public Dictionary<int, string> ClientList { get; set; }
        public int ClientId { get; set; }
        public int ProjectId { get; set; }
        public int Assigned { get; set; }
        public RequestStatus? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ProjectLeadId { get; set; }
    }

    public class ExpertRequestSingleViewModel : BaseEntity{
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public int? ProjectLeadId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RestartDate { get; set; }
        public int? AssignedToId { get; set; }
        public virtual TeamMember AssignedTo { get; set; }
        
    }
}