using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.Consillium.Models {
    public class AddLeadModel {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PrimaryContact { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string SecondaryContact { get; set; }
        [DataType(DataType.Url)]
        public string LinkedInUrl { get; set; }
        [Required]
        public DateTime ContactedOn { get; set; }
        [Required]
        public decimal FeesAmount { get; set; }
        [Required]
        public int FeesCurrencyId { get; set; }

        public IDictionary<string, string> TitleOptions { get; set; }
        public IDictionary<int,string> CountryOptions { get; set; }
        public IDictionary<int,string> CurrencyOptions { get; set; }
        public IDictionary<int,string> RecruiterOptions { get; set; }
        public LeadStatus LeadStatus { get; set; }
        public string LeadComments { get; set; }
       public int RecruiterId { get; set; }
        public string JobHistory { get; set; }
        public string Source { get; set; }
        public string Notes { get; set; }
        public ExpertType ExpertType { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int ResumeId { get; set; }
    }
}