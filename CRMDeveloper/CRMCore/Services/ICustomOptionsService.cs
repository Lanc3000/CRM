using CRMCore.Enums;
using CRMCore.Extensions;
using CRMCore.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Services
{
    public interface ICustomOptionsService
    {
        List<Grouping<CustomOptionType,ObjCustomOption>> GetVM();
        EditCustomOption Get(int id);
        ServiceResult Save(EditCustomOption editCustomOption);
        ServiceResult Update(EditCustomOption editCustomOption);
        ServiceResult Delete(int id);
    }
}
