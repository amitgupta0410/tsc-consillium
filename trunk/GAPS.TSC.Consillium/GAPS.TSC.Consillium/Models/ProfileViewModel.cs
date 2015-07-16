using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.Models {
    public class ProfileViewModel {

        public ProfileViewModel() {
            ExpertNoteModels = new List<ExpertNoteModel>();
            WorkExperienceModels = new List<WorkExperienceModel>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string PrimaryContact { get; set; }
        public string SecondaryContact { get; set; }
        public string LinkedInUrl { get; set; }
        public DateTime? ContactedOn { get; set; }
        public decimal FeesAmount { get; set; }
        public string FeesCurrency { get; set; }
        public LeadStatus LeadStatus { get; set; }
        public string LeadComments { get; set; }
        public string Geography { get; set; }
        public string Industry { get; set; }
        public string RecruiterName { get; set; }
        public string NoteToAdd { get; set; }
        public IEnumerable<ExpertNoteModel> ExpertNoteModels { get; set; }
        public IEnumerable<WorkExperienceModel> WorkExperienceModels { get; set; }

        public string JobHistory { get; set; }
        public string Source { get; set; }
        public string Notes { get; set; }
        public ExpertType ExpertType { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int? ResumeId { get; set; }
        public string FileName { get; set; }
        public string ActualFileName { get; set; }
    }

    public class ExpertNoteModel {
        public string TeamMember { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }


}