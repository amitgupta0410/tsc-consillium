using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Repositories;

namespace GAPS.TSC.CONS.Services{
    public interface IExpertService : IGenericService<Expert> {
        
    }

    public class ExpertService : GenericService<Expert>, IExpertService{
        public ExpertService(IRepository<Expert> repository) : base(repository) {}

      
    }
}