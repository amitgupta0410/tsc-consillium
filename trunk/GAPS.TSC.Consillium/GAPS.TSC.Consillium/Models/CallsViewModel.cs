using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.Models
{
    public class CallsViewModel
    {
        public CallsViewModel()
        {
            ExpertList = new Dictionary<int, string>();
            Geography = new Dictionary<int, string>();
            Currency = new Dictionary<int, string>();
            TeamMembers = new Dictionary<int, string>();
            PaymentStatusOptions = new Dictionary<int, string>();
            PaymentModeDictionary = new Dictionary<int, string>();
        }
        public IEnumerable<CallsExpertViewModel> ExpertCalls { get; set; }
        public int ExpertRequestId { get; set; }
        [Required(ErrorMessage = "Please select a Expert")]
        public int ExpertId { get; set; }
        public IDictionary<int, string> TeamMembers { get; set; }
        [Required(ErrorMessage = "Please select a Team Member")]
        public int CallFacilitatedById { get; set; }
        public IDictionary<int, string> CallTypeOptions { get; set; }
        public CallType CallType { get; set; }
        [Required(ErrorMessage = "Please select a Date")]
        public DateTime InterviewDate { get; set; }
        [Required(ErrorMessage = "Please fill call duration")]
        public decimal CallDuration { get; set; }
         [Required(ErrorMessage = "Please fill fees")]
        public decimal FeesPerHour { get; set; }
        public IDictionary<int, string> CostSharingOptions { get; set; }
        public CostSharingType CostBorneBy { get; set; }
        public string Details { get; set; }
        public int? GeographicId { get; set; }
        [Required(ErrorMessage = "Please select Amount")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Please select currency ")]
        public int AmountCurrencyId { get; set; }
        public IDictionary<int, string> PaymentStatusOptions { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
         [Required(ErrorMessage = "Please select a payment mode")]
        public int PaymentModeId { get; set; }
        public Dictionary<int, string> ExpertList { get; set; }
        public Dictionary<int, string> Geography { get; set; }
        public Dictionary<int, string> Currency { get; set; }
        public Dictionary<int, string> PaymentModeDictionary { get; set; } 

    }

    public class CallsExpertViewModel
    {
        public int ExpertId { get; set; }
        public string ExpertName { get; set; }
        public CallType CallType { get; set; }
        public DateTime InterviewDate { get; set; }
        public decimal CallDuration { get; set; }
        public decimal FeesPerHour { get; set; }
        public int PaymentModeId { get; set; }
        public CostSharingType CostBorneBy { get; set; }
        public int CallFacilitatedById { get; set; }
        public string Details { get; set; }
        
    }
}