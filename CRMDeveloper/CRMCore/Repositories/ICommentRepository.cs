using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CRMCore.DB;
using CRMCore.Enums;

namespace CRMCore.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IQueryable<Comment> GetAllQueryable();

        IEnumerable<Comment> GetByRootIDAndType(int rootId, RootTypes rootType);
    }
}
