using System.Collections.Generic;
using System.Linq;
using CRMCore.DB;
using CRMCore.Enums;
using Microsoft.EntityFrameworkCore;

namespace CRMCore.Repositories.Impl
{
    public class CommentRepository :  Repository<Comment> ,ICommentRepository
    {
        public CommentRepository(DBContext context) : base(context)
        {
        }

        public IQueryable<Comment> GetAllQueryable()
        {
            return Queryable();
        }

        public IEnumerable<Comment> GetByRootIDAndType(int rootId, RootTypes rootType)
        {
            var result = Queryable().Where(x => x.RootId == rootId && x.RootType == rootType).ToList();
            return result;
        }
    }
}
