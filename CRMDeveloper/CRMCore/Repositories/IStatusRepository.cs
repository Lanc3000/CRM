using CRMCore.DB;
using CRMCore.Enums;
using CRMCore.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Repositories
{
    public interface IStatusRepository : IRepository<Status>
    {
        List<Status> Get(RootTypes rootType);
        Status Get(string v);
    }
}
