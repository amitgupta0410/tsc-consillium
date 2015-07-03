using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONS.Domain.ApiModels
{
    public class Employees
    {
        public Employees()
        {
            employees = new List<UserModel>();
        }
       public IEnumerable<UserModel> employees { get; set;} 
    }
}
