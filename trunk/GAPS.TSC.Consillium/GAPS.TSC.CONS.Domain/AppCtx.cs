using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain
{
    public class AppCtx : DbContext
    {
        public AppCtx() : base("AppCtx") { }
        

    }
}
