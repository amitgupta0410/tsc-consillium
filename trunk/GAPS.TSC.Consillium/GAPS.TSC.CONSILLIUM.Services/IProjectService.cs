using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain.ApiModels;

namespace GAPS.TSC.CONSILLIUM.Services
{
    public interface IProjectService
    {
        List<Project> GetAllMasterProjects(IEnumerable<int> projectsToInclue = null);
        IEnumerable<Project> GetAllReviewableProjects(IEnumerable<int> projectsToInclue = null);
        IEnumerable<ProjectStaffing> GetProjectStaffing(int projectId);
        IEnumerable<int> GetDevelopersForProject(int projectId);
        IEnumerable<ProjectStaffing> GetEmployeeStaffing(int userId);
        IEnumerable<int> GetProjectsWithPlRole(int userId);
        IEnumerable<int> GetProjectsWithGroupUnitHeadRole(int userId);
        IEnumerable<int> AllProjectsICanView(int userId);
        IDictionary<int, string> GetQualityManagerReviewer(int pId);
        Project FindById(int id);
        int? GetGroupHeadForProject(int projectId);
        int? GetUnitHeadForProject(int projectId);
    }
}
