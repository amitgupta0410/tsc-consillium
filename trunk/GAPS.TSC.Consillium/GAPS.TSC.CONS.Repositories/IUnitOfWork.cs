﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAPS.TSC.CONS.Domain;

namespace GAPS.TSC.CONS.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<TeamMember> TeamMembers { get; }
        int Save();
    }
}
