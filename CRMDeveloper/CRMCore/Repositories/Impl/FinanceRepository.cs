using CRMCore.DB;
using CRMCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CRMCore.Repositories.Impl
{
    public class FinanceRepository : Repository<Finance>, IFinanceRepository
    {
        public FinanceRepository(DBContext context) : base(context)
        {
        }
        public List<Finance> GetList()
        {
            var result = Queryable()
                .Include(x => x.FinanceType)
                .Include(x => x.Place)
                .Include(x => x.Project)
                .Include(x => x.SubType)
                .Include(x => x.User)
                .ToList();
            return result;
        }

        public List<Finance> GetListByClientId(int rootId)
        {
            var result = Queryable()
                  .Include(x => x.Project)
                .Include(x => x.SubType)
                .Include(x => x.User)
                 .Where(x => x.Project.RootId == rootId && x.Project.RootType == RootTypes.Client)
                 .ToList();
            return result;
        }

        public List<Finance> GetListByProjectId(int rootId)
        {
            var result = Queryable()
                .Where(x => x.ProjectId == rootId)
                .Include(x => x.Project)
                .Include(x => x.SubType)
                .Include(x => x.User)
                 .ToList();

            return result;
        }

        public List<Finance> GetListByUserId(int rootId)
        {
            var result = Queryable()
                .Where(x => x.UserId.HasValue && x.UserId == rootId)
                .Include(x => x.User)
                .Include(x => x.Project)
                .Include(x => x.SubType)
                .ToList();

            return result;
        }

        public List<Finance> AllFull()
        {
            var result = GetIncludeFull()
                .ToList();

            return result;
        }

        private IQueryable<Finance> GetIncludeFull()
        {
            var result = Queryable()
                .Include(x => x.User)
                .Include(x => x.Project)
                .Include(x => x.SubType);

            return result;
        }
    }
}
