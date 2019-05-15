using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.Enums;
using System.Linq;
using CRMCore.DB;

namespace CRMCore.Repositories
{
    public interface IFileDataRepository : IRepository<FileData>
    {
        IEnumerable<FileData> GetFiles(int rootId, RootTypes rootType);

    }
}
