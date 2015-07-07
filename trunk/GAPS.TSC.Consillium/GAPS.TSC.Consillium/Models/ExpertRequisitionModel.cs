using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Domain.ApiModels;

namespace GAPS.TSC.Consillium.Models
{
    public class ExpertRequisitionModel
    {

        public ExpertRequisitionModel()
        {
            ProjectLead = new Dictionary<int, string>();
            AssignedList = new Dictionary<int, string>();
            ExpertRequests = new ExpertRequest();
            ClientList = new Dictionary<int, string>();

        }
        public ExpertRequest ExpertRequests { get; set; }
        public Dictionary<int, string> ProjectLead { get; set; }
        public Dictionary<int, string> AssignedList { get; set; }
        public IDictionary<string, string> StatusOptions { get; set; }
        public Dictionary<int, string> ClientList { get; set; }
        public int ClientId { get; set; }
        public int Assigned { get; set; }
    }
}