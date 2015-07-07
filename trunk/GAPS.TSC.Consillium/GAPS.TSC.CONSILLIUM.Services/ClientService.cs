using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GAPS.TSC.CONS.Domain.ApiModels;
using GAPS.TSC.CONS.Repositories;

namespace GAPS.TSC.CONS.Services{
    public class ClientService : IClientService{
        private const string ClientsCacheKey = "Clients";
        private readonly IUnitOfWork _uow;

        public ClientService(IUnitOfWork uow) {
            _uow = uow;
        }

        //==========Request to RestServices (Clients)===========//
        public IEnumerable<ClientModel> GetAllClients() {
            return RestService.Get<List<ClientModel>>("clients");
        }

        public IQueryable<ClientModel> Find(Expression<Func<ClientModel, bool>> filter) {
            return GetAllClients().AsQueryable().Where(filter);
        }

        public ClientModel FindById(int id) {
            return GetAllClients().FirstOrDefault(x => x.Id == id);
        }
    }
}