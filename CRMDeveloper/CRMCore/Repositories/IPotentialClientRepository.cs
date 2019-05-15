using CRMCore.DB;
using System.Collections.Generic;
using System.Linq;

namespace CRMCore.Repositories
{
    public interface IPotentialClientRepository : IRepository<PotentialClient>
    {
        List<PotentialClient> AllFull();
        PotentialClient GetFull(int id);
        List<PotentialClient> Search(string searchString);
        IQueryable<PotentialClient> GetIncludeFull();
    }
}
