using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GAPS.TSC.CONS.Domain;
using Hangfire.Annotations;

namespace GAPS.TSC.Consillium.Models
{
    public class RequestExpertEn
    {
        public RequestExpertEn()
        {
            Clients=new Dictionary<int, string>();
            Units=new Dictionary<int, string>();
            Industry = new Dictionary<int, string>();
            Geography=new Dictionary<int, string>();
            Currency = new Dictionary<int, string>();
        }

        public Dictionary<int, string> Clients { get; set; }
        [Required(ErrorMessage = "Please select a Client")]
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Please select a Project")]
        public Dictionary<int, string> Units { get; set; }
        public int UnitId { get; set; }
        public int ProjectId { get; set; }
        public int? ProjectLeadId { get; set; }
        public string ProjectLeadName { get; set; }
        public int? BdLeadId { get; set; }
        public string BdLeadName { get; set; }
        public string Description { get; set; }
        public Dictionary<int, string> Industry { get; set; }
        public int IndustryId { get; set; }
        public Dictionary<int, string> Geography { get; set; }
        public int GeographyId { get; set; }
        public Dictionary<int, string> Currency { get; set; }
        public int CurrencyId { get; set; }
        public string Comments { get; set; }
        public decimal BudgetAmount { get; set; }
        public IDictionary<string, string> CostSharingOptions { get; set; }
        public CostSharingType CostSharing { get; set; }
        public decimal TscShare { get; set; }
        public decimal ClientShare { get; set; }       
        public HttpPostedFileBase ScopingDocumentFile { get; set; }

        [Required(ErrorMessage = "Please upload a file.")]
        public HttpPostedFileBase ApprovalDocumentFile { get; set; }
        public int ScopingDocumentId { get; set; }
        public int ApprovalDocumentId { get; set; }

    }
}