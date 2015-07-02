using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.CONS.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
       
        public UnitOfWork
            (
            AppCtx context
           
           )
        {
            _context = context;
           
        }

     


        public int Save()
        {

            foreach (var entry in _context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added && entry.Entity is ITrackable)
                {
                    ((ITrackable)entry.Entity).CreatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified && entry.Entity is ITrackable)
                {
                    ((ITrackable)entry.Entity).UpdatedAt = DateTime.UtcNow;
                }
            }

            return _context.SaveChanges();
        }
    }
}
