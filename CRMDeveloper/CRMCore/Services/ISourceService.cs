using System;
using System.Collections.Generic;
using System.Text;
using CRMCore.Extensions;
using CRMCore.Objects;

namespace CRMCore.Services
{
    public interface ISourceService
    {
        List<ObjSource> All();
        ObjSource Get(int? id);
        ServiceResult Delete(int id);
        ServiceResult Add(ObjSource objSource);
        ServiceResult Edit(ObjSource objSource);
    }
}
