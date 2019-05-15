using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.DB;
using CRMCore.Enums;

namespace CRMCore.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        IEnumerable<Contact> GetByRootIdAndType(int rootID, RootTypes rootType);
    }
}
