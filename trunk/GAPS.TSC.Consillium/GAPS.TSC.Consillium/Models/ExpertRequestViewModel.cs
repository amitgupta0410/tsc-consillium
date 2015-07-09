using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ExpressiveAnnotations.Attributes;
using GAPS.TSC.CONS.Domain;


namespace GAPS.TSC.Consillium.Models
{
    public class ExpertRequestViewModel
    {
        public ExpertRequestViewModel()
        {
            Clients=new Dictionary<int, string>();
            Units=new Dictionary<int, string>();
            Industry = new Dictionary<int, string>();
            Geography=new Dictionary<int, string>();
            Currency = new Dictionary<int, string>();
            ProjectLeadList = new Dictionary<int, string>();
            CostSharingTypeValue=CostSharingType.TSC;
            TscShare = 100;
            ClientShare = 0;
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
        public int BudgetCurrencyId { get; set; }
        public string Comments { get; set; }
        public decimal BudgetAmount { get; set; }
        public IDictionary<int, string> CostSharingOptions { get; set; }
        public CostSharingType CostSharingTypeValue { get; set; }
        public decimal TscShare { get; set; }

        [AssertThat("(CostSharingTypeValue == CostSharingType.TSC && ClientShare== 0 && TscShare== 100) || (CostSharingTypeValue == CostSharingType.Client && ClientShare == 100 && TscShare == 0) || (CostSharingTypeValue == CostSharingType.Both && ClientShare+TscShare== 100) || (CostSharingTypeValue == CostSharingType.ManDayBilling && ClientShare== 0 && TscShare== 0)", ErrorMessage = "Please select a valid share")]
      
        public decimal ClientShare { get; set; }

        private bool Some() {
            return (CostSharingTypeValue == CostSharingType.TSC && ClientShare == 0 && TscShare == 100) ||
                   (CostSharingTypeValue == CostSharingType.Client && ClientShare == 100 && TscShare == 0) ||
                   (CostSharingTypeValue == CostSharingType.Both && ClientShare + TscShare == 100) ||
                   (CostSharingTypeValue == CostSharingType.ManDayBilling && ClientShare == 0 && TscShare == 0);
        }


        public HttpPostedFileBase ScopingDocumentFile { get; set; }

        [Required(ErrorMessage = "Please upload a file.")]
        public HttpPostedFileBase ApprovalDocumentFile { get; set; }
        public int ScopingDocumentId { get; set; }
        public int ApprovalDocumentId { get; set; }
        public bool IsRequestExpertManual { get; set; }
        public Dictionary<int, string> ProjectLeadList { get; set; }
        public string ClientName { get; set; }
        public string ProjectName { get; set; }

    }
}