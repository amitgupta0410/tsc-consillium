using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //RETRIEVE METHODS
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> Get(params Expression<Func<TEntity, object>>[] includeProperties);


        //MODIFICATION METHODS
        void Add(TEntity entity);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        //SAVE 
        int Save();
    }
}
