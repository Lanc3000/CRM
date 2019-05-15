using System;
using System.Collections.Generic;
using System.Text;
using CRMCore.Enums;
using CRMCore.Extensions;
using CRMCore.Objects;


namespace CRMCore.Services
{
    public interface IStatusService
    {
        List<Grouping<RootTypes, ObjStatus>> GetAllStatuses();
        void AddStatus(ObjStatus objStatus);
        ObjStatus Get(int id);
        void Update(ObjStatus model);
        void Delete(int id);
        List<ObjStatus> GetStatusesByRootType(RootTypes potentialCLient);
    }
}
