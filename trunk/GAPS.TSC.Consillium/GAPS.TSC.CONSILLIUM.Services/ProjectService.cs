using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Domain.ApiModels;
using GAPS.TSC.CONS.Repositories;

namespace GAPS.TSC.CONSILLIUM.Services
{
    public class ProjectService : IProjectService
    {
        private const string ProjectsCacheKey = "AllProjects";
        private const string ReviewableProjectsCacheKey = "ReviewableProjects";

        private readonly IUnitOfWork _uow;
        private readonly IUserService _userService;
        private readonly IMainMastersService _mainMastersService;

        public ProjectService(IUnitOfWork uow, IUserService userService, IMainMastersService mainMastersService)
        {
            _uow = uow;
            _userService = userService;
            _mainMastersService = mainMastersService;
        }

        public List<Project> GetAllMasterProjects(IEnumerable<int> projectsToInclue = null)
        {
            if (!CacheHelper.HasKey(ProjectsCacheKey))
            {
                var data = RestService.Get<List<Project>>("projects");
                CacheHelper.Add(ProjectsCacheKey, data);
            }
            var str = CacheHelper.Get(ProjectsCacheKey) as List<Project>;
            if (str != null)
            {
                if (projectsToInclue != null)
                {
                    return str.Where(x => projectsToInclue.Contains(x.Id)).ToList();
                }
                return str; //.Where(x => x.IsActive);
            }
            return new List<Project>();
        }

        public IEnumerable<Project> GetAllReviewableProjects(IEnumerable<int> projectsToInclue = null)
        {
            if (!CacheHelper.HasKey(ReviewableProjectsCacheKey))
            {
                var data = RestService.Get<List<Project>>("projects/reviewable");
                CacheHelper.Add(ReviewableProjectsCacheKey, data);
            }
            var str = CacheHelper.Get(ReviewableProjectsCacheKey) as List<Project>;
            if (str != null)
            {
                if (projectsToInclue != null)
                {
                    return str.Where(x => projectsToInclue.Contains(x.Id));
                }
                return str; //.Where(x => x.IsActive);
            }
            return new List<Project>();
        }

        public Project FindById(int id)
        {
            return GetAllMasterProjects().FirstOrDefault(x => x.Id == id);
        }

        public Project FindByReview(bool value)
        {
            return GetAllMasterProjects().FirstOrDefault(x => x.IsReview == value);
        }
        public IDictionary<int, string> GetQualityManagerReviewer(int id)
        {
            var qmReviewers = GetDevelopersForProject(id);
            var qmReviewerDictionary = new Dictionary<int, string>();
            var allDevelopers = _userService.GetAllUsers().ToDictionary(x => x.Id, x => x.FullName);
            foreach (var qmreviewer in qmReviewers)
            {
                if (allDevelopers.ContainsKey(qmreviewer))
                {
                    qmReviewerDictionary.Add(qmreviewer, allDevelopers[qmreviewer]);
                }
            }

            return qmReviewerDictionary;
        }
        
        public IEnumerable<ProjectLeadModel> GetProjectLeads(int projectId, DateTime? date = null)
        {
            var url = date.HasValue
                ? string.Format("projects/{0}/projectlead/{1}", projectId, date.Value.ToString("yyyy-MM-dd"))
                : string.Format("projects/{0}/projectlead", projectId);
            return RestService.Get<List<ProjectLeadModel>>(url);
        }


        public IEnumerable<ProjectStaffing> GetProjectStaffing(int projectId)
        {
            var url = string.Format("projects/{0}/staffing", projectId);
            return RestService.Get<List<ProjectStaffing>>(url);
        }


        public IEnumerable<int> GetDevelopersForProject(int projectId)
        {
            var staffing = GetProjectStaffing(projectId);
            return staffing.Where(x => x.StaffTypeId == (int)ProjectStaffType.Developer).Select(x => x.UserId);
        }

        public IEnumerable<ProjectStaffing> GetEmployeeStaffing(int userId)
        {
            var url = string.Format("employees/{0}/staffing", userId);
            return RestService.Get<List<ProjectStaffing>>(url);
        }

        public IEnumerable<int> GetProjectsWithPlRole(int userId)
        {
            return
                GetEmployeeStaffing(userId)
                    .Where(x => x.StaffTypeId == (int)ProjectStaffType.PL).Select(x => x.ProjectId);
        }

        public int? GetGroupHeadForProject(int projectId)
        {
            var project = GetAllMasterProjects().FirstOrDefault(x => x.Id == projectId);
            if (null == project || !project.GroupId.HasValue)
            {
                return null;
            }
            var myGroup = _mainMastersService.GetAllGroups().FirstOrDefault(x => x.Id == project.GroupId.Value);
            if (null == myGroup)
            {
                return null;
            }
            return myGroup.GroupHeadId;
        }
        public int? GetUnitHeadForProject(int projectId)
        {
            var project = GetAllMasterProjects().FirstOrDefault(x => x.Id == projectId);
            if (null == project || !project.UnitId.HasValue)
            {
                return null;
            }
            var myUnit = _mainMastersService.GetAllUnits().FirstOrDefault(x => x.Id == project.UnitId.Value);
            if (null == myUnit)
            {
                return null;
            }
            return myUnit.UnitHeadId;
        }
        public IEnumerable<int> GetProjectsWithGroupUnitHeadRole(int userId)
        {
            var myGroups = _mainMastersService.GetAllGroups().Where(x => x.GroupHeadId == userId).Select(x => x.Id);
            var myUnits = _mainMastersService.GetAllUnits().Where(x => x.UnitHeadId == userId).Select(x => x.Id);
            return
                GetAllMasterProjects()
                    .Where(x =>
                        (x.GroupId.HasValue && myGroups.Contains(x.GroupId.Value)) ||
                        (x.UnitId.HasValue && myUnits.Contains(x.UnitId.Value))
                    ).Select(x => x.Id);
        }

        public IEnumerable<int> AllProjectsICanView(int userId)
        {
            return GetProjectsWithGroupUnitHeadRole(userId).Union(GetProjectsWithPlRole(userId));
        }


        
    }
}
