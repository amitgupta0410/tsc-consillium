using System;
using System.Linq;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Repositories;

namespace GAPS.TSC.CONS.Services {
    public interface IExpertService : IGenericService<Expert> {
        bool AddExpert(Expert expert);
        bool LeadNameExist(string name);
        bool EmailExist(string email);
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

        public bool LeadNameExist(string name)
        {
            var user = Get(x => x.Name == name);
         return !user.Any();
        }

        public bool EmailExist(string email) {
            var user = Get(x => x.Email == email);
            return !user.Any();
        }

    }
}