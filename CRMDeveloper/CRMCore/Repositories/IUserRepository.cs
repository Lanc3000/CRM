using CRMCore.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRMCore.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmailAndPassword(string Email, string Passhash);
        User GetByEmail(string email);
        IQueryable<User> GetUsers(string roleName);
        IQueryable<User> GetFullUsers();
        User GetFull(int id);
        List<User> GetManagers();
    }
}
