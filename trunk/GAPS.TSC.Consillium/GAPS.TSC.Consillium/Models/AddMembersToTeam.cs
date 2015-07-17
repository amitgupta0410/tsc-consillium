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
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Id must be a natural number")]
        public int? UserId { get; set; }

        public IDictionary<int,string> UserOptions { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only alphabets")]
        public string Name { get; set; }
        public TeamMemberType TeamMemberType { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<TeamMember> Employees { get; set; } 
    }
}