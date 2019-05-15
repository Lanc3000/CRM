using CRMCore.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        /// Роли с RoleActivitys
        /// </summary>
        /// <returns></returns>
        List<Role> GetRoles();

        /// <summary>
        /// Роль с RoleActivitys
        /// </summary>
        /// <returns></returns>
        Role GetFull(int RoleId);
    }
}
