using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GAPS.TSC.Consillium.Models
{
    public class ExpertDashboardViewModel
    {
        public ExpertDashboardViewModel()
        {
            IndustryList=new Dictionary<int, string>();
            GeographicList =new Dictionary<int, string>();
            ProjectList=new Dictionary<string, string>();
            ClientList =new Dictionary<string,string>();
            ExpertRequestlist =new Dictionary<int, string>();
           ToAddOrganisations=new List<string>();
        }
       
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public int? ProjectId { get; set; }
        public int? ClientId { get; set; }
        public Dictionary<int, string> ExpertRequestlist { get; set; } 
        public int? GeographicId { get; set; }
        public int? IndustryId { get; set; }
        [Required(ErrorMessage="Please Select Expert Request")]
        public int RequestId { get; set; }
        public string SearchString { get; set; }
        public Dictionary<int, string> IndustryList { get; set; }
        public Dictionary<int, string> GeographicList { get; set; }
        public Dictionary<string, string> ProjectList { get; set; }
        public Dictionary<string, string> ClientList { get; set; }
        public IEnumerable<ExpertSingleViewModel> Experts { get; set; }
        public bool Check { get; set; }
        public IEnumerable<int> Expert { get; set; }
        public List<string> ToAddOrganisations { get; set; } 
       
    }

    public class ExpertSingleViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int? GeographicId { get; set; }
        public int? IndustryId { get; set; }
        public int Id { get; set; }

    }
}