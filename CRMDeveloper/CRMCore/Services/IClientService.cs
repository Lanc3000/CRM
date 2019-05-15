using System;
using System.Collections.Generic;
using System.Text;
using CRMCore.Extensions;
using CRMCore.Objects;

namespace CRMCore.Services
{
    public interface IClientService
    {
        List<ObjClient> GetListClientP();
        ServiceResult ConvertToClient(int pClientId);
        ObjClient Get(int id);
        ServiceResult EditClient(ObjClient objClient);
        ServiceResult AddClient(ObjClient objClient);
        List<ObjClientP> GetListClientWithProjects();
    }
}
