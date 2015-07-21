﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.CONS.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
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
        private readonly IRepository<ExpertRequestScopingDocumentMap> _expertRequestScopingDocuments;
        private readonly IRepository<ExpertRequestTeamMemberMap> _expertRequestTeamMemberMaps;

       
        public UnitOfWork(AppCtx context,
            IRepository<Expert> experts,
            IRepository<ExpertNote> expertNotes,
            IRepository<TeamMember> teamMembers,
            IRepository<ExpertRequest> expertRequests,
            IRepository<Attachment> attachments,
            IRepository<Call> calls,
            IRepository<PaymentMode> paymentModes,
            IRepository<WorkExperience> workExperiences,
            IRepository<SpecialProjectLeadMap> projectLeads,
            IRepository<ExpertRequestScopingDocumentMap> expertRequestScopingDocuments)
        {
            IRepository<ExpertRequestTeamMemberMap> expertRequestTeamMemberMaps,
            IRepository<SpecialProjectLeadMap> projectLeads)
        {
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
            _expertRequestScopingDocuments = expertRequestScopingDocuments;
            _expertRequestTeamMemberMaps = expertRequestTeamMemberMaps;

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
        public IRepository<ExpertRequestScopingDocumentMap> ExpertRequestScopingDocuments  { get  { return _expertRequestScopingDocuments;  } }

        public IRepository<ExpertRequestTeamMemberMap> ExpertRequestTeamMemberMaps
        {
            get { return _expertRequestTeamMemberMaps; }
        }


        public int Save()
        {

            return _context.SaveChanges();
        }
    }
}
