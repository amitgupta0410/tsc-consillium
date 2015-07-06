using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain {
    public class Expert : BaseEntity {

        public string Title { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string PrimaryContact { get; set; }
        public string SecondaryContact { get; set; }
        public string LinkedInUrl { get; set; }
        public DateTime? ContactedOn { get; set; }
        public decimal FeesAmount { get; set; }
        public int FeesCurrencyId { get; set; }
        public LeadStatus LeadStatus { get; set; }
        public string LeadComments { get; set; }
        public int? GeographicId { get; set; }
        public int? IndustryId { get; set; }

        [ForeignKey("Recruiter")]
        public int RecruiterId { get; set; }
        public TeamMember Recruiter { get; set; }

        public virtual ICollection<ExpertRequest> ExpertRequests { get; set; }

        public virtual ICollection<ExpertNote> ExpertNotes { get; set; }

        public string JobHistory { get; set; }
        public string Source { get; set; }
        public string Notes { get; set; }
        public ExpertType ExpertType { get; set; }
        public DateTime? JoiningDate { get; set; }
        public bool IsLead { get { return !JoiningDate.HasValue; } }

        [ForeignKey("Resume")]
        public int? ResumeId { get; set; }
        public virtual Attachment Resume { get; set; }

        public virtual ICollection<WorkExperience> WorkExperiences { get; set; }

    }
}
