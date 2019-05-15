using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using CRMCore.DB;
using CRMCore.Enums;

namespace CRMCore.Repositories.Impl
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(DBContext context) : base(context)
        {
        }

        public IEnumerable<Contact> GetByRootIdAndType(int rootID, RootTypes rootType)
        {
            return Queryable().Where(x => x.RootID == rootID && x.RootType == rootType).ToList();
        }
    }
}
