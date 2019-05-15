using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.Enums;
using CRMCore.Objects;
using CRMCore.Extensions;
using System.IO;

namespace CRMCore.Services
{
    public interface IFileDataService
    {
        ServiceResult<ObjFileData> UploadFile(int rootId, RootTypes rootType, IFormFile file, int? createdId);
        ObjFileDataList GetFiles(int rootId, RootTypes rootType);
        ServiceResult Delete(int id);
        ObjFileStream GetFileStream(int id);
    }
}
