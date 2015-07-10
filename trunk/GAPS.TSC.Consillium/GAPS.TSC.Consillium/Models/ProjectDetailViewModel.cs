using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.Models
{
    public class ProjectDetailViewModel:BaseEntity
    {
        public int? ProjectId { get; set; }
        public string ProjectLeadName { get; set; }
        public string ProjectName { get; set; } 
        public string ClientName { get; set; }
        public int? ClientId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RestartDate { get; set; }
        public TeamMember Recruiter { get; set; }
        public string BdLeadName { get; set; }
        public string Geography { get; set; }
        public decimal AllocatedBudget { get; set; }
        public string Industry { get; set; }
        public int PaidCalls { get; set; }
        public DateTime RequestedDate { get; set; }
        public String Comments { get; set; }
        public CostSharingType CostSharingType { get; set; }
        public string Description { get; set; }
        public ICollection<Expert> Experts { get; set; } 
        public DateTime? RestartEndDate { get; set; }   
    }
}