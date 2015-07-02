using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain.ApiModels;

namespace GAPS.TSC.CONSILLIUM.Services
{
    public interface IClientService
    {
        IEnumerable<ClientModel> GetAllClients();
        ClientModel FindById(int id);
        IQueryable<ClientModel> Find(Expression<Func<ClientModel, bool>> filter);
    }
}
