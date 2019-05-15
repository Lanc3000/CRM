using CRMCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Services
{
    public interface IRootTypesService
    {
        string GetRootName (RootTypes rootType, int rootId);
        string GetRootUrl(RootTypes rootType, int rootId);
    }
}
