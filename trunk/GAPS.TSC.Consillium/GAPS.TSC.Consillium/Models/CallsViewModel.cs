using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CsvHelper.Configuration;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.Models {
    public class CallsViewModel {
        public CallsViewModel() {
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
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal FeesPerHour { get; set; }
        public IDictionary<int, string> CostSharingOptions { get; set; }
        public CostSharingType CostBorneBy { get; set; }
        public string Details { get; set; }
        public int? GeographicId { get; set; }
        [Required(ErrorMessage = "Please select Amount")]
        [Range(1,int.MaxValue,ErrorMessage = "Amount must be greater than zero.")]
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

    public class CallsExpertViewModel {
        public int ExpertId { get; set; }
        public string ExpertName { get; set; }
        public CallType CallType { get; set; }
        public DateTime InterviewDate { get; set; }
        public decimal CallDuration { get; set; }
        public decimal FeesPerHour { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentMode { get; set; }
        public CostSharingType CostBorneBy { get; set; }
        public int CallFacilitatedById { get; set; }
        public string CallFacilitatedBy { get; set; }
        public string Details { get; set; }
    }

    public sealed class CallsExpertMapModel : CsvClassMap<CallsExpertViewModel> {
        public CallsExpertMapModel() {
            Map(x => x.ExpertName);
            Map(x => x.CallType).Name("Nature of Interview");
            Map(x => x.InterviewDate);
            Map(x => x.CallDuration);
            Map(x => x.FeesPerHour).Name("Honorarium per hour");
            Map(x => x.PaymentMode);
            Map(x => x.CostBorneBy);
            Map(x => x.CallFacilitatedBy);
            Map(x => x.Details).Name("Payment Details");
        }
    }
}