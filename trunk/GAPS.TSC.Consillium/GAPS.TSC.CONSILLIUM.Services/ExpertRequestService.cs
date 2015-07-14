using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Repositories;

namespace GAPS.TSC.CONS.Services
{
    public interface IExpertRequestService : IGenericService<ExpertRequest>
    {
        IEnumerable<int> GetProjectLeads();
        IEnumerable<ExpertRequest> GetAllExpertsProjects();
        IEnumerable<Expert> GetExpertsForRequest(int requestId);
        IEnumerable<WorkExperience> GetAllDesignations(int id);
        void AddExpertToRequest(int requestId, int expertId);
        void RemoveExpertFromRequest(int requestId, int expertId);
    }

    public class ExpertRequestService : GenericService<ExpertRequest>, IExpertRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpertRequestService(IRepository<ExpertRequest> repository, IUnitOfWork unitOfWork)
            : base(repository)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<int> GetProjectLeads()
        {
            return Repository.Get(x => x.ProjectLeadId.HasValue).Select(x => x.ProjectLeadId.Value).Distinct();
        }

        public IEnumerable<ExpertRequest> GetAllExpertsProjects()
        {
            var projects = _unitOfWork.ExpertRequests.Get();
            return projects;

        }
        public IEnumerable<Expert> GetExpertsForRequest(int requestId)
        {
            var req= Repository.Get(x => x.Id == requestId && x.DeletedAt == null,p=>p.Experts).FirstOrDefault();
            if (null == req)
            {
                return null;
            }
            return req.Experts;
        }
        public void AddExpertToRequest(int requestId, int expertId)
        {
            var req = GetById(requestId);
            if (null == req)
            {
                throw new Exception();
            }
            var exists = req.Experts.Any(x => x.Id == expertId);
            if (!exists)
            {
                var expert = _unitOfWork.Experts.Get(x => x.Id == expertId).FirstOrDefault();
                if (null == expert)
                {
                    throw new Exception();
                }
                req.Experts.Add(expert);
                _unitOfWork.Save();
            }


        }
     
        public void RemoveExpertFromRequest(int requestId, int expertId)
        {
            var req = GetById(requestId);
            if (null == req)
            {
                throw new Exception();
            }
            var exists = req.Experts.Any(x => x.Id == expertId);
            if (exists)
            {
                var expert = _unitOfWork.Experts.Get(x => x.Id == expertId).FirstOrDefault();
                if (null == expert)
                {
                    throw new Exception();
                }
                req.Experts.Remove(expert);
                _unitOfWork.Save();
            }


        }

        public IQueryable<Call> GetCallsForRequest(int id)
        {
            return _unitOfWork.Calls.Get(x => x.ExpertRequestId == id);
        }

        public void AddCallsToRequest(int id, Call call)
        {
            call.ExpertRequestId = id;
            _unitOfWork.Calls.Add(call);
            _unitOfWork.Save();
        }
        public IEnumerable<WorkExperience> GetAllDesignations(int id)
        {
            return _unitOfWork.WorkExperiences.Get(x => x.Designation).Where(x => x.ExpertId == id);
        }
    }
}