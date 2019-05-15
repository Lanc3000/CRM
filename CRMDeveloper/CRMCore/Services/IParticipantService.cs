using System;
using System.Collections.Generic;
using System.Text;
using CRMCore.Enums;
using CRMCore.Objects;

namespace CRMCore.Services
{
    public interface IParticipantService
    {
        ObjParticipantList GetList(int rootId, RootTypes rootType);
        void Add(ObjParticipant obj);
        void Delete(int id);
        void Edit(ObjParticipant obj);

        List<ObjPTask> GetTasks();
        ObjPTask GetPTask(int id);
        void UpdatePTask(ObjPTask model);
        void DeletePTask(int id);
        void AddPTask(ObjPTask objPTask);
        string[] GetNamePtasks();
    }
}
