using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.Models {
    public class AddLeadModel {

        public AddLeadModel() {
            WorkExperiences = new List<WorkExperienceModel>();
        }

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public bool IsLead { get; set; }
        [Required]
        [Remote("LeadNameExist", "Employees", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name." , AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required (ErrorMessage = "The country field is required")]
        public int CountryId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid Email.")]
        [Remote("EmailExist", "Employees", HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different email." , AdditionalFields = "Id")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Please enter a valid contact number.")]
        public string PrimaryContact { get; set; }
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Please enter a valid contact number.")]
        public string SecondaryContact { get; set; }
        [Url(ErrorMessage = "Please enter a valid url.")]
        public string LinkedInUrl { get; set; }
        [Required]
        public DateTime ContactedOn { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "Amount must be greater than zero.")]
        public decimal FeesAmount { get; set; }
        [Required(ErrorMessage = "The Currency field is required.")]
        public int FeesCurrencyId { get; set; }
        public IDictionary<string, string> TitleOptions { get; set; }
        public IDictionary<string, string> ExpertTypeOptions { get; set; }
        public IDictionary<string, string> LeadStatusOptions { get; set; }
        public IDictionary<int, string> CountryOptions { get; set; }
        public IDictionary<int, string> CurrencyOptions { get; set; }
        public IDictionary<int, string> RecruiterOptions { get; set; }

        public IEnumerable<WorkExperienceModel> WorkExperiences { get; set; }
        public LeadStatus LeadStatus { get; set; }
        public string LeadComments { get; set; }
        [Required(ErrorMessage = "The Recruiter field is required.")]
        public int RecruiterId { get; set; }
        public string JobHistory { get; set; }
        public string Source { get; set; }
        public string Notes { get; set; }
        public ExpertType ExpertType { get; set; }
        public int? ResumeId { get; set; }
        public string FileName { get; set; }
        public string FileGuidName { get; set; }
//        [Required]
        public string Designation { get; set; }
//        [Required]
        public string Organisation { get; set; }
//        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }

    public class WorkExperienceModel {
        public int Id { get; set; }
        public int ExpertId { get; set; }
        public string Designation { get; set; }
        public string Organisation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}