using CRMCore.Enums;
using System;
using System.Collections.Generic;
using CRMCore.Objects;
using CRMCore.Objects.Comments;

namespace CRMCore.Services
{
    public interface ICommentsService
    {
        ObjCommentList GetListComments(int rootID, RootTypes rootType, int lastId = 0);
        ObjComment AddComment(ObjComment obj, List<ObjFileData> dataList);
    }
}
