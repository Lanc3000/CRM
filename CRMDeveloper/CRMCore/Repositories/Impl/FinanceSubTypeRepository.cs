using CRMCore.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Repositories.Impl
{
    public class FinanceSubTypeRepository : Repository<FinanceSubType>, IFinanceSubTypeRepository
    {
        public FinanceSubTypeRepository(DBContext context) : base(context)
        {
        }
    }
}
