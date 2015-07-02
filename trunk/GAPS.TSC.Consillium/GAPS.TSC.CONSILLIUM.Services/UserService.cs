using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain.ApiModels;

namespace GAPS.TSC.CONSILLIUM.Services
{
    public class UserService : IUserService
    {
        private const string CacheKey = "Users";


        public UserService() { }

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

        public IEnumerable<UserModel> GetAllEtUsers()
        {
            return GetAllUsers().Where(x => x.DesignationId.HasValue && Designation.EtTeam.Contains(x.DesignationId.Value));
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
