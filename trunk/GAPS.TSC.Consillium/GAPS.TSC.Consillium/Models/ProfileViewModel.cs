using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.Models {
    public class ProfileViewModel {

        public ProfileViewModel() {
            ExpertNoteModels = new List<ExpertNoteModel>();
            WorkExperienceModels = new List<WorkExperienceModel>();
            ExpertCallsModels = new List<ExpertCallsModel>();
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
       [Required(ErrorMessage = "Please add some note.")]
        public string NoteToAdd { get; set; }
        [Required(ErrorMessage = "PLease select a file.")]
        public HttpPostedFileBase File { get; set; }
        public IEnumerable<ExpertNoteModel> ExpertNoteModels { get; set; }
        public IEnumerable<WorkExperienceModel> WorkExperienceModels { get; set; }
        public IEnumerable<ExpertCallsModel> ExpertCallsModels { get; set; }
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

    public class ExpertCallsModel {
        public int Id { get; set; }
        public string ExpertRequestName { get; set; }
        public CallType CallType { get; set; }
        public DateTime InterviewDate { get; set; }
        public decimal CallDuration { get; set; }
        public decimal FeesPerHour { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentMode { get; set; }
        public CostSharingType CostBorneBy { get; set; }
        public int CallFacilitatedById { get; set; }
        public string CallFacilitatedBy { get; set; }
        public string Details { get; set; }
    }


}