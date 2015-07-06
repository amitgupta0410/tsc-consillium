using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain {
   public class Attachment: BaseEntity {
       public string FileName { get; set; }
       public string ActualName { get; set; }
    }
}
