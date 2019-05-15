using CRMCore.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Repositories.Impl
{
    public class TaskTypeRepository : Repository<TaskType>, ITaskTypeRepository
    {
        public TaskTypeRepository(DBContext context) : base(context)
        {
        }
    }
}
