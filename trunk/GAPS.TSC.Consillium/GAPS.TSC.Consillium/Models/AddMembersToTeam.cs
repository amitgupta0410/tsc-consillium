using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Domain.ApiModels;

namespace GAPS.TSC.Consillium.Models
{
    public class AddMembersToTeam
    {
        public AddMembersToTeam()
        {
            Employees = new List<TeamMember>();
        }
        [Required(ErrorMessage = "Please Select User ")]
        public int? UserId { get; set; }

        public IDictionary<int,string> UserOptions { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only alphabets")]
        public string Name { get; set; }
        public TeamMemberType TeamMemberType { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<TeamMember> Employees { get; set; } 
    }
}