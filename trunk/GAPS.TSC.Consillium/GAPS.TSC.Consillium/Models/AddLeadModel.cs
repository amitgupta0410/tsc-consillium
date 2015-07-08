using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.Models {
    public class AddLeadModel {
        [Required]
        public string Title { get; set; }
        [Required]
        [Remote("LeadNameExist", "Employees", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        public string Name { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid Email.")]
        [Remote("EmailExist", "Employees", HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different email.")]
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
        public decimal FeesAmount { get; set; }
        [Required]
        public int FeesCurrencyId { get; set; }

//        [Required]
//        public HttpPostedFileBase ResumeFile { get; set; }

        public IDictionary<string, string> TitleOptions { get; set; }
        public IDictionary<string, string> ExpertTypeOptions { get; set; } 
        public IDictionary<string, string> LeadStatusOptions { get; set; }
        public IDictionary<int, string> CountryOptions { get; set; }
        public IDictionary<int,string> CurrencyOptions { get; set; }
        public IDictionary<int,string> RecruiterOptions { get; set; }
        public LeadStatus LeadStatus { get; set; }
        public string LeadComments { get; set; }
       public int? RecruiterId { get; set; }
        public string JobHistory { get; set; }
        public string Source { get; set; }
        public string Notes { get; set; }
        public ExpertType ExpertType { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int ResumeId { get; set; }
    }
}