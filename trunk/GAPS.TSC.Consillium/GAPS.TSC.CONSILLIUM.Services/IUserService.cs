using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Domain.ApiModels;

namespace GAPS.TSC.CONSILLIUM.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel FindById(int id);
        UserModel FindByEmail(string email);
        bool Authenticate(string email, string password, out UserModel user);
        IEnumerable<UserModel> GetUserICanApprove(int userId);
        IEnumerable<UserModel> GetUserICanView(int userId);
        IDictionary GetUser();
        IEnumerable<TeamMember> GetAllTeamMembers();
        bool AddTeamMember(TeamMember teamMember);
        IEnumerable<SpecialProjectLeadMap> GetAllProjectLeads();
        bool AddSpecialProjectLeads(SpecialProjectLeadMap projectLead);

    }
}
