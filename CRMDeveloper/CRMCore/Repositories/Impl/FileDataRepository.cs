using CRMCore.DB;
using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.Enums;
using System.Linq;

namespace CRMCore.Repositories.Impl
{
    public class FileDataRepository : Repository<FileData>, IFileDataRepository
    {
        public FileDataRepository(DBContext context) : base(context)
        {
        }

        public IEnumerable<FileData> GetFiles(int rootId, RootTypes rootType)
        {
            var result = Queryable().Where(fd => fd.RootId == rootId && fd.RootType == rootType );
            return result;
        }
    }
}
