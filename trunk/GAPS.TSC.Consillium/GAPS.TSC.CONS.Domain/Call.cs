using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain {
  public class Call : BaseEntity {
      
      [ForeignKey("Expert")]
      public int ExpertId { get; set; }
      public virtual Expert Expert { get; set; }

      [ForeignKey("CallFacilitatedBy")]
      public int CallFacilitatedById { get; set; }
      public virtual TeamMember CallFacilitatedBy { get; set; }

      public CallType CallType { get; set; }
      public DateTime InterviewDate { get; set; }
      public decimal CallDuration { get; set; }
      public decimal FeesPerHour { get; set; }
      public CostSharingType CostBorneBy { get; set; }
      public string Details { get; set; }
      public int? GeographicId { get; set; }
      public decimal Amount { get; set; }
      public int AmountCurrencyId { get; set; }
      public PaymentStatus PaymentStatus { get; set; }
      public DateTime? PaymentInitiationDate { get; set; }
      public DateTime? PaymentConfirmationDate { get; set; }
      
      [ForeignKey("PaymentConfirmedBy")]
      public int? PaymentConfirmedById { get; set; }
      public virtual  TeamMember PaymentConfirmedBy { get; set; }

      [ForeignKey("PaymentInitiatedBy")]
      public int? PaymentInitiatedById { get; set; }
      public virtual TeamMember PaymentInitiatedBy { get; set; }

      [ForeignKey("PaymentMode")]
      public int PaymentModeId { get; set; }
      public virtual PaymentMode PaymentMode { get; set; }

  }
}
