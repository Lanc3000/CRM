using System;
using System.Collections.Generic;

using CRMCore.DB;
using CRMCore.Enums;
using CRMCore.Objects;
using CRMCore.Extensions;

namespace CRMCore.Services
{
    public interface IProjectService
    {
        List<ObjProject> GetProjectList();
        ObjProjectList GetProjectList(int rootID, RootTypes rootType);
        ObjProject Get(int id);
        List<ObjProjectP> GetObjProjectPs();

        ServiceResult SaveProject(ObjProject obj);
        ServiceResult EditProject(ObjProject obj);
        ServiceResult Delete(int id);

        ObjProjectType GetProjectType(int id);
        void Add(ObjProjectType obj);
        void DeleteProjectType(int id);
        void Edit(ObjProjectType obj);
        List<ObjProjectType> GetProjectTypes();
        ProjectsModel GetViewModel(ProjectsModel model);
    }
}
