using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain
{
   public class ExpertRequestTeamMemberMap:BaseEntity
    {
       [ForeignKey("TeamMember")]
       public int AssignedToId { get; set; }
       [ForeignKey("ExpertRequest")]
       public int ExpertRequestId { get; set; }
       public virtual ExpertRequest ExpertRequest { get; set; }
       public virtual TeamMember TeamMember { get; set; }
    }
}
