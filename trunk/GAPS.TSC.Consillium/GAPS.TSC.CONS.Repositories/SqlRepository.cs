using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.CONS.Repositories
{
    public class SqlRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly DbContext Context;
        protected readonly IDbSet<TEntity> DbSet;

        public SqlRepository(AppCtx ctx)
        {
            Context = ctx;
            DbSet = ctx.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter, params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includeProperties)
        {

            foreach (var property in includeProperties)
            {
                DbSet.Include(property);
            }

            return DbSet.Where(filter);
        }

        public IQueryable<TEntity> Get(params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includeProperties)
        {

            foreach (var property in includeProperties)
            {
                DbSet.Include(property);
            }

            return DbSet;
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        //public virtual void Delete(TKey id)
        //{
        //    TEntity entityToDelete = DbSet.Find(id);
        //    Delete(entityToDelete);
        //}
        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }

            DbSet.Remove(entityToDelete);
        }

        public void Update(TEntity entity)
        {
            DbSet.AddOrUpdate(entity);
        }


        //public void Update(TEntity entityToUpdate)
        //{
        //    //todo: for naveen, think about this
        //    var update = entityToUpdate as ITrackable;
        //    if (update != null)
        //    {
        //        update.CreatedAt = DateTime.UtcNow;
        //    }

        //    if (entityToUpdate is UserModel)
        //    {
        //        var existingEntity = DbSet.FirstOrDefault(x => x.Id.ToString() == entityToUpdate.Id.ToString());
        //        if (existingEntity != null)
        //        {
        //            Context.Entry(existingEntity).State = EntityState.Detached;
        //        }
        //    }
        //    else
        //    {
        //        var existingEntity = DbSet.Local.FirstOrDefault(x => (x as IEntity<int>).Id == (entityToUpdate as IEntity<int>).Id);
        //        if (existingEntity != null)
        //        {
        //            Context.Entry(existingEntity).State = EntityState.Detached;
        //        }
        //    }

        //    //            if (!DbSet.Local.Any(x => x.Id == entityToUpdate.Id))
        //    DbSet.Attach(entityToUpdate);
        //    Context.Entry(entityToUpdate).State = EntityState.Modified;
        //}

        public int Save()
        {
            return Context.SaveChanges();
        }


        /*  public IEnumerable<UserModel> GetEmployees() {
              return Context.Database.SqlQuery<DbUser>("SELECT * FROM VW_GetEmployees").ToList().Select(x => new UserModel {
                  Code = x.Code,
                  CreatedAt = DateTime.Now,
                  Email = x.Email,
                  Id = x.Code,
                  IdentityId = x.Id,
                  Name = x.Name
              });
          }*/
    }
}
