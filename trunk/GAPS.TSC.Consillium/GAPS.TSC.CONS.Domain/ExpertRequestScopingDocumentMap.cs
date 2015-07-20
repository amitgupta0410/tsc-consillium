using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain
{
  public class ExpertRequestScopingDocumentMap:BaseEntity
    {
      [ForeignKey("ExpertRequest")]
      public int ExpertRequestId { get; set; }
      public virtual ExpertRequest ExpertRequest { get; set; }
     
      [ForeignKey("Attachment")]
      public int AttachmentId { get; set; }
      public virtual Attachment Attachment { get; set; }
    }
}
