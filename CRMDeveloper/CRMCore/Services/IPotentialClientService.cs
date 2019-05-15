using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.DB;
using CRMCore.Extensions;
using CRMCore.Objects;

namespace CRMCore.Services
{
    public interface IPotentialClientService
    {
        IEnumerable<ObjPotentialClient> All();

        ObjPotentialClient GetById(int Id);

        ServiceResult SavePCLient(ObjPotentialClient obj);

        ServiceResult EditPClient(ObjPotentialClient obj);

        List<ObjPotentialClient> Search(string searchString);

        List<ObjPotentialClient> FilterData(List<int> statuses, string q, List<int> users);
        
        PotentialClientsModel GetViewModel(PotentialClientsModel model);
    }
}
