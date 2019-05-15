using CRMCore.DB;
using CRMCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Repositories
{
    public interface IFinanceRepository : IRepository<Finance>
    { 
        List<Finance> GetListByUserId(int rootId);
        List<Finance> GetListByClientId(int rootId);
        List<Finance> GetListByProjectId(int rootId);
        List<Finance> GetList();
        List<Finance> AllFull();
    }
}
