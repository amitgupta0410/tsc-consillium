using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Repositories
{
    public interface IUnitOfWork
    {
        int Save();
    }
}
