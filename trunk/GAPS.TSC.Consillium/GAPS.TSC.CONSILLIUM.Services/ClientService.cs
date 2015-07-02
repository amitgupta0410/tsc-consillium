using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain.ApiModels;
using GAPS.TSC.CONS.Repositories;

namespace GAPS.TSC.CONSILLIUM.Services
{
    public class ClientService : IClientService
    {

        private const string ClientsCacheKey = "Clients";
        private readonly IUnitOfWork _uow;
        public ClientService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        //==========Request to RestServices (Clients)===========//
        public IEnumerable<ClientModel> GetAllClients()
        {
            if (!CacheHelper.HasKey(ClientsCacheKey))
            {

                var data = RestService.Get<List<ClientModel>>("clients");
                CacheHelper.Add(ClientsCacheKey, data);
            }
            var str = CacheHelper.Get(ClientsCacheKey) as List<ClientModel>;
            if (str != null)
            {
                return str;
            }
            return new List<ClientModel>();
        }

        public IQueryable<ClientModel> Find(Expression<Func<ClientModel, bool>> filter)
        {
            return GetAllClients().AsQueryable().Where(filter);
        }

        public ClientModel FindById(int id)
        {
            return GetAllClients().FirstOrDefault(x => x.Id == id);
        }
    }
}
