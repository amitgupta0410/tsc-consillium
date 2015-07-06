using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Domain.ApiModels;
using GAPS.TSC.CONS.Repositories;

namespace GAPS.TSC.CONSILLIUM.Services
{
    public class UserService : IUserService
    {
        private const string CacheKey = "Users";
        private readonly IUnitOfWork _uow;
        private readonly IMainMastersService _mainMastersService;

        public UserService(IUnitOfWork uow, IMainMastersService mainMastersService)
        {
            _uow = uow;
            _mainMastersService = mainMastersService;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            if (!CacheHelper.HasKey(CacheKey))
            {
                CacheHelper.Add(CacheKey, RestService.Get<List<UserModel>>("employees"));
            }

            var str = CacheHelper.Get(CacheKey) as List<UserModel>;
            if (str != null)
            {
                return str.Where(x => x.IsActive.HasValue && x.IsActive.Value);
            }
            return new List<UserModel>();
        }

   

        public IDictionary GetUser()
        {
            var allEmployees = GetAllUsers();
            var userDictionary = new Dictionary<int, UserModel>();
            foreach (var employee in allEmployees)
            {
                userDictionary[employee.Id] = employee;
            }
            return userDictionary;
        }
        public IEnumerable<TeamMember> GetAllTeamMembers()
        {
            var teamMembers = _uow.TeamMembers.Get();
            return teamMembers;
        }
        public bool AddTeamMember(TeamMember teamMember)
        {
            try
            {
                _uow.TeamMembers.Add(teamMember);
                _uow.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<SpecialProjectLeadMap> GetAllProjectLeads()
        {
            var projectLeads = _uow.ProjectLeads.Get();
            return projectLeads;
        }

        public bool AddSpecialProjectLeads(SpecialProjectLeadMap projectLead)
        {
            try
            {
                _uow.ProjectLeads.Add(projectLead);
                _uow.Save();
                return true;
            }
            catch (Exception ex)
            {
                
                return false;
            }
            
        }

        public UserModel FindById(int id)
        {
            return GetAllUsers().FirstOrDefault(x => x.Id == id);
        }

        public UserModel FindByEmail(string email)
        {
            return GetAllUsers().FirstOrDefault(x => x.OfficialEmail == email);
        }


        public bool Authenticate(string email, string password, out UserModel user)
        {
            return RestService.Post("employees/authenticate", new Dictionary<string, string> {
                {"userName",email},
                {"password",password}
            }, out user);
        }


        public IEnumerable<UserModel> GetUserICanApprove(int userId)
        {
            return
                GetAllUsers()
                    .Where(x => x.ReportingManagerId == userId || x.GroupHeadId == userId || x.UnitHeadId == userId);
        }

        public IEnumerable<UserModel> GetUserICanView(int userId)
        {
            return
                GetAllUsers()
                    .Where(
                        x =>
                            x.ReportingManagerId == userId || x.GroupHeadId == userId || x.UnitHeadId == userId ||
                            x.Id == userId);
        }

       
        
             
    }
}
