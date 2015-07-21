using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.Models {
    public class CallDashboardModel {
        public CallDashboardModel() {
            Calls = new List<CallSingleViewModel>();
            Filter = new CallDashboardFilterModel();
        }


        public IEnumerable<CallSingleViewModel> Calls { get; set; }
        public CallDashboardFilterModel Filter { get; set; }

    }

    public class CallSingleViewModel {
        public string ExpertRequest { get; set; }

        public string Expert { get; set; }

        public string CallFacilitatedBy { get; set; }

        public CallType CallType { get; set; }
        public DateTime InterviewDate { get; set; }
        public decimal CallDuration { get; set; }
        public decimal FeesPerHour { get; set; }
        public CostSharingType CostBorneBy { get; set; }
        public string Details { get; set; }
        public decimal Amount { get; set; }
        public int AmountCurrencyId { get; set; }
        public string AmountCurrency { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }

    public class CallDashboardFilterModel {
        public CallDashboardFilterModel() {
            ProjectList = new Dictionary<int, string>();
            ClientList = new Dictionary<string, string>();
        }


        public DateTime? Date { get; set; }
        public int? Project { get; set; }
        public string Client { get; set; }
        public Dictionary<int, string> ProjectList { get; set; }
        public Dictionary<string, string> ClientList { get; set; }
    }
}