using CRMCore.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Repositories.Impl
{
    public class ProjectTypeRepository : Repository<ProjectType>, IProjectTypeRepository
    {
        public ProjectTypeRepository(DBContext context) : base(context)
        {
        }
    }
}
