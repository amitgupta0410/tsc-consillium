using System.Collections.Generic;
using GAPS.TSC.CONS.Domain.ApiModels;

namespace GAPS.TSC.CONS.Services
{
    public interface IProjectService
    {
        List<Project> GetAllMasterProjects(IEnumerable<int> projectsToInclue = null);
        IEnumerable<ProjectStaffing> GetProjectStaffing(int projectId);
        IEnumerable<ProjectStaffing> GetEmployeeStaffing(int userId);
        IEnumerable<int> GetProjectsWithPlRole(int userId);
        IEnumerable<int> GetProjectsWithGroupUnitHeadRole(int userId);
        IEnumerable<int> AllProjectsICanView(int userId);
        Project FindById(int id);
        int? GetGroupHeadForProject(int projectId);
        int? GetUnitHeadForProject(int projectId);
    }
}
