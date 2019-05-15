using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

using CRMCore.DB;
using CRMCore.Objects;

namespace CRMCore.Repositories.Impl
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DBContext context) : base(context)
        {

        }
        public User GetByEmailAndPassword(string Email, string PassHash)
        {
            var result = Queryable()
                .Include(user=>user.Role)
                .Include(user=>user.Role.RoleActivitys)
               .FirstOrDefault(x => x.Email == Email && x.PassHash == PassHash);
            return result;
        }

        public User GetByEmail(string email)
        {
            var result = Queryable()
                .Include(user=>user.Role)
                .FirstOrDefault(x => x.Email == email);
            return result;
        }
        public User GetRoleByUser(long id)
        {
            var result = Queryable()
                .FirstOrDefault(x => x.Id == id);
            return result;
        }

        public IQueryable<User> GetUsers(string roleName)
        {
            var result = Queryable()
                .Include(user=>user.Role)
                .Where(x => x.Role.Title == roleName);
            return result;
        }

        public IQueryable<User> GetFullUsers()
        {
            var result = Queryable()
                .Include(user => user.Role);
            return result;
        }

        public User GetFull(int id)
        {
            var result = Queryable()
                .Include(user => user.Role)
                .FirstOrDefault(user => user.Id == id);
            return result;
        }

        public List<User> GetManagers()
        {
            return GetFullUsers()
                    .Where(x => x.IsManager)
                    .ToList();
        }

    }
}
