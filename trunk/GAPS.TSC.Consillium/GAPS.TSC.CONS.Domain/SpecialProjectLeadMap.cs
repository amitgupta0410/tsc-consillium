using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain
{
   public class SpecialProjectLeadMap :BaseEntity
    {
        public int UserId { get; set; }
        public bool IsActive { get; set; }
    }
}
