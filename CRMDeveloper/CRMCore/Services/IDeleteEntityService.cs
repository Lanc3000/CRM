using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.Enums;
using CRMCore.Extensions;

namespace CRMCore.Services
{
    public interface IDeleteEntityService
    {
        ServiceResult<string> Delete(int Id, RootTypes rootType);
    }
}
