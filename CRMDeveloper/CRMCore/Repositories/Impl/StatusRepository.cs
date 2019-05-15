using CRMCore.DB;
using CRMCore.Enums;
using CRMCore.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRMCore.Repositories.Impl
{
    public class StatusRepository : Repository<Status>, IStatusRepository
    {
        public StatusRepository(DBContext context) : base(context)
        {
        }

        public List<Status> Get(RootTypes rootType)
        {
            var result = Queryable()
                .Where(status => status.rootType == rootType)
                .ToList();
            return result;
        }

        public Status Get(string v)
        {
            var result = Queryable()
                .FirstOrDefault(status => status.Name == v);

            return result;
        }
    }
}
