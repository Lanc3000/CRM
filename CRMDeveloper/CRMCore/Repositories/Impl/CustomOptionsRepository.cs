using CRMCore.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Repositories.Impl
{
    public class CustomOptionsRepository : Repository<CustomOption>, ICustomOptionRepository
    {
        public CustomOptionsRepository(DBContext context) : base(context)
        {
        }
    }
}
