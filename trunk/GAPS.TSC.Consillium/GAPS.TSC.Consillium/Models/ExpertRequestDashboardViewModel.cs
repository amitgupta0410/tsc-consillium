using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Domain.ApiModels;
using Microsoft.Ajax.Utilities;

namespace GAPS.TSC.Consillium.Models
{
    public class ExpertRequestDashboardViewModel
    {

        public ExpertRequestDashboardViewModel()
        {
            ProjectLeadList = new Dictionary<int, string>();
            AssignedList = new Dictionary<int, string>();
            ProjectList = new Dictionary<string, string>();
            ClientList = new Dictionary<string, string>();
            ToAddMembers = new List<string>();
            CallList = new Dictionary<int, int>();
        }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public IEnumerable<ExpertRequestSingleViewModel> ExpertRequests { get; set; }
        public Dictionary<int, string> ProjectLeadList { get; set; }
        public Dictionary<String, string> ProjectList { get; set; }
        public Dictionary<int, string> AssignedList { get; set; }
        public IDictionary<string, string> StatusOptions { get; set; }
        public Dictionary<string, string> ClientList { get; set; }
        public int? ClientId { get; set; }
        public int? ProjectId { get; set; }
        public int? Assigned { get; set; }
        public RequestStatus? Status { get; set; }
        [DisplayFormat(DataFormatString = "dd MM, yyyy")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "dd MM, yyyy")]
        public DateTime? EndDate { get; set; }
        public int? ProjectLeadId { get; set; }
        public string SearchString { get; set; }
        public List<string> ToAddMembers = new List<string>();
        public Dictionary<int, int> CallList { get; set; }
        public Dictionary<int, string> AssignDictionary { get; set; }
       
    }

    public class ExpertRequestSingleViewModel
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public int CallCount { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public int? ProjectLeadId { get; set; }
        [DisplayFormat(DataFormatString = "dd MM, yyyy")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "dd MM, yyyy")]
        public DateTime? EndDate { get; set; }
        [DisplayFormat(DataFormatString = "dd MM, yyyy")]
        public DateTime? RestartDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AssignedNames { get; set; }
     
    }

    public class AssignViewModel
    {
        public AssignViewModel()
        {
            AssignToList = new Dictionary<int, string>();
        }
        public int Id { get; set; }
        public Dictionary<int, string> AssignToList { get; set; }
        public IEnumerable<int> AssignedToIds { get; set; }
        public DateTime? StartDate { get; set; }
        public string Comments { get; set; }
    }
}