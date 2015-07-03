using System;
using System.Collections.Generic;
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
        public int? UserId { get; set; }
        public string Name { get; set; }
        public TeamMemberType TeamMemberType { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<TeamMember> Employees { get; set; } 
    }
}