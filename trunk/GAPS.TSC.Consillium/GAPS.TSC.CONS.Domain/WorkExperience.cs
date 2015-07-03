using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain {
  public class WorkExperience : BaseEntity {
      
      [ForeignKey("Expert")]
      public int ExpertId { get; set; }
      public Expert Expert { get; set; }

      public string Designation { get; set; }
      public string Organisation { get; set; }
      public DateTime StartDate { get; set; }
      public DateTime? EndDate { get; set; }
    }
}
