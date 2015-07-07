using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain {
    public class ExpertRequest : BaseEntity {

        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public int IndustryId { get; set; }
        public string Comments { get; set; }
        public int GeographicId { get; set; }
        public decimal BudgetAmount { get; set; }
        public int BudgetCurrencyId { get; set; }
        public CostSharingType CostSharingType { get; set; }
        public decimal TscShare { get; set; }
        public decimal ClientShare { get; set; }

    //    [ForeignKey("ScopingDocument")]
        public int ScopingDocumentId { get; set; }
      //  public virtual Attachment ScopingDocument { get; set; }

      //  [ForeignKey("ApprovalDocument")]
        public int ApprovalDocumentId { get; set; }
     //   public virtual Attachment ApprovalDocument { get; set; }

        public virtual ICollection<Expert> Experts { get; set; } 

        public int? BdLeadId { get; set; }
        public int? ProjectLeadId { get; set; }
        public int? UnitId { get; set; }
        public ExpertRequestType ExpertRequestType { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RestartDate { get; set; }

        [ForeignKey("AssignedTo")]
        public int? AssignedToId { get; set; }
        public virtual TeamMember AssignedTo { get; set; }

    }
}
