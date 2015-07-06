using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain {
  public class ExpertNote : BaseEntity {
     
      [ForeignKey("Expert")]
      public int ExpertId { get; set; }
      public virtual Expert Expert { get; set; }
      
      [ForeignKey("TeamMember")]
      public int TeamMemberId { get; set; }
      public virtual TeamMember TeamMember { get; set; }

      public string Content { get; set; }
    }
}
