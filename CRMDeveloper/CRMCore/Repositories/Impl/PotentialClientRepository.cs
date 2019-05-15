using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using CRMCore.DB;

namespace CRMCore.Repositories.Impl
{
    public class PotentialClientRepository : Repository<PotentialClient>, IPotentialClientRepository
    {
        public PotentialClientRepository(DBContext context) : base(context)
        {
        }

        public List<PotentialClient> AllFull()
        {
            var result = GetIncludeFull()
                        .ToList();
            return result;
        }

        public PotentialClient GetFull(int id)
        {
            var result = GetIncludeFull()
                .FirstOrDefault(x => x.Id == id);
            return result;
        }

        public List<PotentialClient> Search(string searchString)
        {
            var result = GetIncludeFull()
                .Where(x => x.CompanyName.Contains(searchString)
                || x.Name.Contains(searchString))
                .ToList();
            return result;
        }

        public IQueryable<PotentialClient> GetIncludeFull()
        {
            var result = Queryable()
                .Include(x => x.Status)
                .Include(x => x.Manager)
                .Include(x=>x.Source);

            return result;
        }
    }
}
