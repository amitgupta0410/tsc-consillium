using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GAPS.TSC.CONS.Domain.ApiModels;

namespace GAPS.TSC.CONS.Services
{
    public interface IClientService
    {
        IEnumerable<ClientModel> GetAllClients();
        ClientModel FindById(int id);
        IQueryable<ClientModel> Find(Expression<Func<ClientModel, bool>> filter);
    }
}
