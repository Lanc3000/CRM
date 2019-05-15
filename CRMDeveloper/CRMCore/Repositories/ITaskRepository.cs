using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.DB;
using CRMCore.Enums;
using CRMCore.Objects;
namespace CRMCore.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        List<Task> GetList(int rootId, RootTypes rootType);
        List<Task> AllFull();
        Task GetFull(int id);
    }
}
