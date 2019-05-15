using CRMCore.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace CRMCore.Repositories.Impl
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DBContext context) : base(context)
        {
        }

        public Role GetFull(int roleId)
        {
            var result = Queryable().Include(x => x.RoleActivitys).FirstOrDefault(x => x.Id == roleId);
            return result;
        }

        public List<Role> GetRoles()
        {
            var result = Queryable().Include(x => x.RoleActivitys).ToList();
            return result;
        }
    }
}
