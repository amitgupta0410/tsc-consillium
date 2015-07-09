using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain;
using GAPS.TSC.CONS.Repositories;

namespace GAPS.TSC.CONS.Services{
    public interface IGenericService<T> where T : BaseEntity{
        IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter = null);
        T GetById(int id);
        T Add(T obj);
        T Update(T obj);
        bool Delete(T obj);
    }

    public abstract class GenericService<T> : IGenericService<T> where T : BaseEntity{
        protected readonly IRepository<T> Repository;

        public GenericService(IRepository<T> repository) {
            Repository = repository;
        }

        public IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter = null) {
            return filter == null ? Repository.Get() : Repository.Get(filter);
        }

        public T GetById(int id) {
            return Repository.Get(x => x.Id == id && x.DeletedAt == null).FirstOrDefault();
        }

        public T Add(T obj) {
            Repository.Add(obj);
            Repository.Save();
            return obj;
        }

        public T Update(T obj) {
            Repository.Update(obj);
            Repository.Save();
            return obj;
        }

        public bool Delete(T obj) {
            Repository.Delete(obj);
            Repository.Save();
            return true;
        }
    }
}