using CRMCore.DB;
using CRMCore.Enums;
using CRMCore.Objects;
using CRMCore.DB;
using CRMCore.Enums;
using CRMCore.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CRMCore.Repositories.Impl
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository(DBContext context) : base(context)
        {
        }

        public List<Task> AllFull()
        {
            var result = Queryable()
                .Include(x => x.Status)
                .Include(x => x.TaskType)
                .Include(x => x.User)
                .ToList();
            return result;
        }

        public Task GetFull(int id)
        {
            var result = Queryable()
                .Include(x => x.Status)
                .Include(x => x.TaskType)
                .FirstOrDefault(x => x.Id == id);
            return result;
        }

        public List<Task> GetList(int rootId, RootTypes rootType)
        {
            return Queryable()
                .Include(x => x.Status)
                .Include(x => x.TaskType)
                .Where(x => x.RootId == rootId && x.RootType == rootType)
                .ToList();
        }
    }
}
