using CRMCore.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Repositories.Impl
{
    public class SourceRepository : Repository<Source>, ISourceRepository
    {
        public SourceRepository(DBContext context) : base(context)
        {
        }
    }
}
