using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.CONS.Repositories {
    public class UnitOfWork : IUnitOfWork {
        private readonly DbContext _context;
        private readonly IRepository<Expert> _experts;
        private readonly IRepository<ExpertNote> _expertNotes;
        private readonly IRepository<TeamMember> _teamMembers;
        private readonly IRepository<ExpertRequest> _expertRequests;
        private readonly IRepository<Attachment> _attachments;
        private readonly IRepository<Call> _calls;
        private readonly IRepository<PaymentMode> _paymentModes;
        private readonly IRepository<WorkExperience> _workExperiences;
        private readonly IRepository<SpecialProjectLeadMap> _projectLeads; 

       
        public UnitOfWork(AppCtx context,
            IRepository<Expert> experts,
            IRepository<ExpertNote> expertNotes,
            IRepository<TeamMember> teamMembers,
            IRepository<ExpertRequest> expertRequests,
            IRepository<Attachment> attachments,
            IRepository<Call> calls,
            IRepository<PaymentMode> paymentModes,
            IRepository<WorkExperience> workExperiences,
            IRepository<SpecialProjectLeadMap>projectLeads){
            _context = context;
            _experts = experts;
            _expertNotes = expertNotes;
            _teamMembers = teamMembers;
            _expertRequests = expertRequests;
            _attachments = attachments;
            _calls = calls;
            _paymentModes = paymentModes;
            _workExperiences = workExperiences;
            _projectLeads = projectLeads;

    }


        public IRepository<Expert> Experts { get { return _experts; } }
        public IRepository<ExpertNote> ExpertNotes { get { return _expertNotes; } }
        public IRepository<TeamMember> TeamMembers { get { return _teamMembers; } }
        public IRepository<ExpertRequest> ExpertRequests { get { return _expertRequests; } }
        public IRepository<Attachment> Attachments { get { return _attachments; } }
        public IRepository<Call> Calls { get { return _calls; } }
        public IRepository<PaymentMode> PaymentModes { get { return _paymentModes; } }
        public IRepository<WorkExperience> WorkExperiences { get { return _workExperiences; } }
        public IRepository<SpecialProjectLeadMap> ProjectLeads { get { return _projectLeads; } }


        public int Save() {

            foreach (var entry in _context.ChangeTracker.Entries()) {
                if (entry.State == EntityState.Added && entry.Entity is ITrackable) {
                    ((ITrackable)entry.Entity).CreatedAt = DateTime.UtcNow;
                } else if (entry.State == EntityState.Modified && entry.Entity is ITrackable) {
                    ((ITrackable)entry.Entity).UpdatedAt = DateTime.UtcNow;
                }
            }

            return _context.SaveChanges();
        }
    }
}
