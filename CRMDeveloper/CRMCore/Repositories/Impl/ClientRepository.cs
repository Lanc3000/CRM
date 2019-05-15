using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.DB;
using CRMCore.Repositories;

namespace CRMCore.Repositories.Impl
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(DBContext context) : base(context)
        {
        }
    }
}
