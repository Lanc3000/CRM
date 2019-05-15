using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.DB;
using CRMCore.Enums;
using CRMCore.Objects;
namespace CRMCore.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        List<Project> GetList(int rootId, RootTypes rootType);
        List<Project> AllFull();
        Project GetFull(int id);
    }
}
