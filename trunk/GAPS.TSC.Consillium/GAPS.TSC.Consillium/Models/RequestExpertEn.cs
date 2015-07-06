using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GAPS.TSC.Consillium.Models
{
    public class RequestExpertEn
    {
        public RequestExpertEn()
        {
            Clients=new Dictionary<int, string>();
        }

        public Dictionary<int, string> Clients { get; set; }
        [Required(ErrorMessage = "Please select a Client")]
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Please select a Project")]
        public int ProjectId { get; set; }
        public int? ProjectLeadId { get; set; }
        public string ProjectLeadName { get; set; }
        public int? BdLeadId { get; set; }
        public string BdLeadName { get; set; }
    }
}