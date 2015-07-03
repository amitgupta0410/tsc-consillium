using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain.ApiModels
{
    public class ProjectDashboardModel
    {
        public ProjectDashboardModel()
        {
           
        }
        public IEnumerable<ProjectCombineModel> Projects { get; set; }
    }

    public class ProjectCombineModel
    {
        public string Group { get; set; }
        public string Client { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectLead { get; set; }
        public DateTime? ExpectedReviewDate { get; set; }
        public DateTime? InterimDeliveryDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public int? Slides { get; set; }
        public string Style { get; set; }
        public int? QmReviewerId { get; set; }
        public string EditorAssigned { get; set; }
        public ProjectStatus Status { get; set; }
        public int ProjectId { get; set; }
        public int? ClientId { get; set; }
        public int? GroupId { get; set; }
        public string QmReviewer { get; set; }
        public int? Score { get; set; }
        public bool IsMandatoryReview { get; set; }
        public int? ProjectLeadId { get; set; }
        public IEnumerable<int> Editors { get; set; }
        public string ReviewerAssigned { get; set; }
    }

}
