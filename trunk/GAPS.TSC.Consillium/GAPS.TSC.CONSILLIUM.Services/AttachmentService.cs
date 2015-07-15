using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Repositories;

namespace GAPS.TSC.CONS.Services{
    public interface IAttachmentService : IGenericService<Attachment> {
        Attachment SaveAttachment(string originalName, string newName);
    }

    public class AttachmentService : GenericService<Attachment>, IAttachmentService{
        public AttachmentService(IRepository<Attachment> repository) : base(repository) {}

        public Attachment SaveAttachment(string originalName, string newName) {
            var attachement = new Attachment {
                ActualName = originalName,
                FileName = newName,
                CreatedAt = DateTime.Now
            };
            return Add(attachement);
        }

    }
}