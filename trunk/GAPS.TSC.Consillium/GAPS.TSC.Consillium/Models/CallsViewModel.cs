using System;
using System.Collections.Generic;
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
            PaymentStatusOptions = new Dictionary<int, string>();
        }
        public int ExpertRequestId { get; set; }
        public int ExpertId { get; set; }
        public int CallFacilitatedById { get; set; }
        public IDictionary<int, string> CallTypeOptions { get; set; }
        public CallType CallType { get; set; }
        public DateTime InterviewDate { get; set; }
        public decimal CallDuration { get; set; }
        public decimal FeesPerHour { get; set; }
        public IDictionary<int, string> CostSharingOptions { get; set; }
        public CostSharingType CostBorneBy { get; set; }
        public string Details { get; set; }
        public int? GeographicId { get; set; }
        public decimal Amount { get; set; }
        public int AmountCurrencyId { get; set; }
        public IDictionary<int, string> PaymentStatusOptions { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int PaymentModeId { get; set; }
        public Dictionary<int, string> ExpertList { get; set; }
        public Dictionary<int, string> Geography { get; set; }
        public Dictionary<int, string> Currency { get; set; }

    }
}