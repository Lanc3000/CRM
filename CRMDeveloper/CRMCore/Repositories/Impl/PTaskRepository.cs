using CRMCore.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Repositories.Impl
{
    public class PTaskRepository : Repository<PTask>, IPTaskRepository
    {
        public PTaskRepository(DBContext context) : base(context)
        {
        }
    }
}
