using System;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Repositories;

namespace GAPS.TSC.CONS.Services {
    public interface IExpertService : IGenericService<Expert> {
        bool AddExpert(Expert expert);
    }

    public class ExpertService : GenericService<Expert>, IExpertService {
        private readonly IUnitOfWork _unitOfWork;

        public ExpertService(IRepository<Expert> repository, IUnitOfWork unitOfWork)
            : base(repository) {
            _unitOfWork = unitOfWork;

        }

        public bool AddExpert(Expert expert) {
            try {
                Add(expert);
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

    }
}