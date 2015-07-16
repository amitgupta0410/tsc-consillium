using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.Models
{
    public class ProjectDetailViewModel : BaseEntity
    {
        public ProjectDetailViewModel()
        {

            ToAddRegions = new List<string>();
            ToAddDesignations = new List<string>();
            ToAddOrganisations=new List<string>();
            ExpertList=new Dictionary<int, string>();
            Industrylist = new Dictionary<int, string>();
             CountryList= new Dictionary<int, string>();


        }
        public Dictionary<int, string> ExpertList { get; set; }
        public Dictionary<int, string> Industrylist { get; set; }
        public Dictionary<int, string> CountryList { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectLeadName { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public int? ClientId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RestartDate { get; set; }
        public TeamMember Recruiter { get; set; }
        public string BdLeadName { get; set; }
        public string Geography { get; set; }
        public decimal AllocatedBudget { get; set; }
        public string Industry { get; set; }
        public int PaidCalls { get; set; }
         [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RequestedDate { get; set; }
        public string Comments { get; set; }
        public CostSharingType CostSharingType { get; set; }
        public string Description { get; set; }
        public IEnumerable<ExpertViewModel> Experts { get; set; }
        public DateTime? RestartEndDate { get; set; }
        public List<string> ToAddRegions = new List<string>();
        public string Designation { get; set; }
        public List<string> ToAddDesignations = new List<string>();
        public List<string> ToAddOrganisations = new List<string>();
        public string BudgetCurrencyName { get; set; }
        public IEnumerable<int> ExpertIds { get; set; }
        public int ExpertId { get; set; }



    }

    public class ExpertViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PrimaryContact { get; set; }
        public LeadStatus LeadStatus { get; set; }
        public int? GeographicId { get; set; }
    }
}