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
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(DBContext context) : base(context)
        {
        }

        public List<Project> AllFull()
        {
            var result = Queryable()
                .Include(x=>x.Status)
                .Include(x => x.ProjectType)
                .Include(x=>x.User)
                .ToList();
            return result;
        }

        public Project GetFull(int id)
        {
            var result = Queryable()
                .Include(x => x.Status)
                .Include(x => x.ProjectType)
                .FirstOrDefault(x => x.Id == id);
            return result;
        }

        public List<Project> GetList(int rootId, RootTypes rootType)
        {
            return Queryable()
                .Include(x => x.Status)
                .Include(x=>x.ProjectType)
                .Where(x => x.RootId == rootId && x.RootType == rootType)
                .ToList();
        }
    }
}
