using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.CONS.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<TeamMember> TeamMembers { get; }
        int Save();
        IRepository<Expert> Experts { get; }
        IRepository<ExpertNote> ExpertNotes { get; }
        IRepository<TeamMember> TeamMembers { get; }
        IRepository<ExpertRequest> ExpertRequests { get; }
        IRepository<Attachment> Attachments { get; }
        IRepository<Call> Calls { get; }
        IRepository<PaymentMode> PaymentModes { get; }
        IRepository<WorkExperience> WorkExperiences { get; }
    }
}
