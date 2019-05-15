using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.DB;

namespace CRMCore.Repositories.Impl
{
    public class RoleActivityRepository : Repository<RoleActivity>, IRoleActivityRepository
    {
        public RoleActivityRepository(DBContext context) : base(context)
        {
        }

       

    }
}
