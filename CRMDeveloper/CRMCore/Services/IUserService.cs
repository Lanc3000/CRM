using CRMCore.Extensions;
using CRMCore.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Services
{
    public interface IUserService 
    {
        ServiceResult CreateUser(ObjUser obj);

        ServiceResult EditUser (ObjUser obj);

        ObjUser GetUserById(int id);

        ObjUser Login(string email, string pass);

        ObjUser GetUserByEmail(string Email);

        List<ObjUser> GetAllUsers();

        ServiceResult DeleteUser(int id);

        List<ObjUser> GetManagers();

        /// <summary>
        /// Роли с активити
        /// </summary>
        /// <returns></returns>
        List<ObjRole> GetRoles();

        ObjRole GetRole(int roleId);

        ServiceResult AddRole(ObjRoleEdit objRoleEdit);

        ObjRoleEdit GetRoleForEdit(int id);

        ServiceResult EditRole(ObjRoleEdit objRoleEdit);

        ServiceResult DeleteRole(int id);

        ServiceResult ChangePassword(ObjChangePassword model);

        List<ObjUser> Search(string searchString);

        ObjFileStream GetFileAvatarStream(int id);
    }
}
