using System;
using System.Collections.Generic;

using CRMCore.DB;
using CRMCore.Enums;
using CRMCore.Objects;
using CRMCore.Extensions;

namespace CRMCore.Services
{
    public interface ITaskService
    {
        List<ObjTask> GetTaskList();
        ObjTaskList GetTaskList(int rootID, RootTypes rootType);
        ObjTask Get(int id);
        List<ObjTaskSecond> GetObjTaskSc();

        ServiceResult SaveTask(ObjTask obj);
        ServiceResult EditTask(ObjTask obj);
        ServiceResult Delete(int id);

        ObjTaskType GetTaskType(int id);
        void Add(ObjTaskType obj);
        void DeleteTaskType(int id);
        void Edit(ObjTaskType obj);
        List<ObjTaskType> GetTaskTypes();
        TasksModel GetViewModel(TasksModel model);
    }
}
