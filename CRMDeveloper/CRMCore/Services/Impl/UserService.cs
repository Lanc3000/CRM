using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;

using CRMCore.DB;
using CRMCore.Objects;
using CRMCore.Repositories;
using CRMCore.Helpers;
using CRMCore.Extensions;
using System.IO;

namespace CRMCore.Services.Impl
{
    public class UserService : IUserService
    {
        FileHelper _fileHelper = new FileHelper();

        private IUserRepository _userRepository { get; set; }
        private IRoleRepository _roleRepository { get; set; }
        private IRoleActivityRepository _roleActivityRepository { get; set; }
        //private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository,
            IRoleRepository roleRepository,
            //ILogger<UserService> logger,
            IRoleActivityRepository roleActivityRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            //_logger = logger;
            _roleActivityRepository = roleActivityRepository;

        }

        public ServiceResult CreateUser(ObjUser obj)
        {
            try
            {
                obj.Email = obj.Email.Trim();
                var users = _userRepository.All();
                if (users.Any(x => string.Equals(x.Email, obj.Email)))
                    return ServiceResult.ErrorResult("Указанный Email уже зарегистрирован");

                obj.PassHash = HelperMd5.CalculateMD5Hash(obj.PassHash);

                //сохраняем аватар
                if (obj.file != null)
                {
                    var originalName = obj.file.FileName.Trim();

                    var extension = Path.GetExtension(originalName);
                    var fileName = Guid.NewGuid().ToString("N") + extension;
                    var path = Path.Combine(CoreConfiguration.PathAvatar, fileName);

                    _fileHelper.SaveFile(obj.file, path);
                    obj.AvatarFileName = fileName;
                }

                var model = Map(obj);


                _userRepository.Insert(model);
                _userRepository.SaveChanges();

                

                return ServiceResult.SuccessResult(model.Id);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error with Create User : {0}", ex.Message);

                return ServiceResult.ErrorResult("Ошибка при создании новго пользователя");
            }

        }

        public ServiceResult EditUser(ObjUser obj)
        {
            try
            {
                obj.Email = obj.Email.Trim();
                var users = _userRepository.All();
                if (users.Any(x => string.Equals(x.Email, obj.Email) && x.Id != obj.Id))
                    return ServiceResult.ErrorResult("Указанный Email уже зарегистрирован");

                var model = _userRepository.Get(obj.Id);

                //сохраняем аватар
                if (obj.file != null)
                {
                    var originalName = obj.file.FileName.Trim();

                    var extension = Path.GetExtension(originalName);
                    var fileName = Guid.NewGuid().ToString("N") + extension;
                    var path = Path.Combine(CoreConfiguration.PathAvatar, fileName);

                    _fileHelper.SaveFile(obj.file, path);
                    obj.AvatarFileName = fileName;
                }

                UpdateMapping(obj, model);
                _userRepository.Update(model);
                _userRepository.SaveChanges();

                return ServiceResult.SuccessResult(model.Id);
            }
            catch(Exception ex)
            {
                //_logger.LogError("Error editing user id = {0} / message  = {1}", obj.Id, ex.Message);
                return ServiceResult.ErrorResult("Ошибка сохранения сотрудника");
            }
           
        }


        public ObjUser GetUserById(int id)
        {
            return Map(_userRepository.GetFull(id));
        }

        public ObjUser Login(string email, string pass)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
                return null;

            var passHash = HelperMd5.CalculateMD5Hash(pass);

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(passHash))
                return null;

            string lowerEmail = email.ToLower();

            ObjUser model = null;
            var User = _userRepository.GetByEmailAndPassword(lowerEmail, passHash);

            model = Map(User);
            //_logger.LogInformation("Логин юзер");
            return model;
        }


        public ObjUser GetUserByEmail(string Email)
        {
            string lowerEmail = Email.ToLower();

            ObjUser model = null;
            var User = _userRepository.GetByEmail(lowerEmail);

            model = Map(User);

            return model;
        }

        public List<ObjUser> GetAllUsers()
        {
            var UserList = _userRepository.GetFullUsers().ToList();
            var ObjUserList = new List<ObjUser>();
            foreach (var user in UserList)
            {
                ObjUserList.Add(Map(user));
            }
            return ObjUserList;
        }

        public ServiceResult DeleteUser(int id)
        {
            try
            {
                _userRepository.Delete(id);
                _userRepository.SaveChanges();

                return ServiceResult.SuccessResult();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Deleting User id = {0} / error: {1}",id, ex.Message);
                return ServiceResult.ErrorResult("Ошибка при удалении Пользователя");
            }
            
        }

        public List<ObjUser> GetManagers()
        {
            var users = _userRepository.GetManagers();
            return users.Select(x => Map(x))
                .ToList();
        }

        public ServiceResult ChangePassword( ObjChangePassword model)
        {
            try
            {
                var user = _userRepository.Get(model.Id);
                if (user == null)
                    return ServiceResult.ErrorResult("Сотрудник не найден");
                user.PassHash = HelperMd5.CalculateMD5Hash(model.NewPassword);
                _userRepository.Update(user);
                _userRepository.SaveChanges();

                return ServiceResult.SuccessResult(user.Id);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error with edit password for user id = {0} / message: {1}", model.Id, ex.Message);
                return ServiceResult.ErrorResult("Ошибка при сохранении пароля");
            }
        }

        public List<ObjUser> Search(string searchString)
        {
            var Users = _userRepository.GetFullUsers()
                .Where(x => x.Fio.Contains(searchString))
                .ToList();
           
            return Users
                .Select(x => Map(x))
                .ToList();


        }

        #region Role
        public List<ObjRole> GetRoles()
        {
            var roles = _roleRepository.GetRoles();
            var activities = ActivityHelper.GetActivities();
            return roles.Select(x => new ObjRole()
            {
                Id = x.Id,
                Title = x.Title,
                Name = x.Name,
                RoleActivitys = activities
                .Where(a => a.Permisioins.Any(p => x.RoleActivitys.Any(ac => ac.Activity == p.Key)))
                .Select(a => $"{a.Title} ({string.Join(", ", a.Permisioins.Where(p => x.RoleActivitys.Any(ac => ac.Activity == p.Key)).Select(p => p.Title.ToLower()))})")
                                .ToList()
            })
            .ToList();

        }

        public ObjRole GetRole(int roleId)
        {
            var role = _roleRepository.GetFull(roleId);
            var model = new ObjRole()
            {
                Id = role.Id,
                Title = role.Title,
                Name = role.Name,
                RoleActivitys = ActivityHelper.GetActivities()
                .Where(a => a.Permisioins.Any(p => role.RoleActivitys.Any(ac => ac.Activity == p.Key)))
                .Select(a => $"{a.Title} ({string.Join(", ", a.Permisioins.Where(p => role.RoleActivitys.Any(ac => ac.Activity == p.Key)).Select(p => p.Title.ToLower()))})")
                .ToList()

            };
            return model;
        }

        public ObjRole GetRoleByLogin(int roleId)
        {
            var role = _roleRepository.GetFull(roleId);
            var model = new ObjRole()
            {
                Id = role.Id,
                Title = role.Title,
                Name = role.Name,
                RoleActivitys =role.RoleActivitys.Select(x=>x.Activity)
                .ToList(),

            };
            return model;
        }

        public ServiceResult AddRole(ObjRoleEdit objRoleEdit)
        {
            try
            {
                //_logger.LogTrace("Save role start");
                var roleTitle = objRoleEdit.Title.Trim();
                var roleName = objRoleEdit.Name.Trim();
                var roles = _roleRepository.GetRoles().ToList();
                if (roles.Any(r => string.Equals(r.Title, roleTitle)))
                {
                    return ServiceResult.ErrorResult("Роль с таким именем уже существует");
                }
                var role = new Role()
                {
                    Title = roleTitle,
                    Name = roleName,
                };
                _roleRepository.Insert(role);
                _roleRepository.SaveChanges();
                //_logger.LogTrace("Role Saving");
           

                //Получили названия всех выбранных разрешений
                var selectedPemissionNames = objRoleEdit.SelectedActivities
                    .Where(x => x.Checked)
                    .Select(x => x.Name)
                    .ToList();

                //Отобрали по именам 
                var selectedPermissions = ActivityHelper.GetActivities()
                    .SelectMany(x => x.Permisioins)
                    .Where(x => selectedPemissionNames.Contains(x.Key))
                    .Select(x => x.Key)
                    .ToList();

                if(selectedPermissions.Any())
                {
                    foreach (var permission in selectedPermissions)
                    {
                        var RoleActivity = new RoleActivity()
                        {
                            RoleId = role.Id,
                            Activity = permission
                        };
                        _roleActivityRepository.Insert(RoleActivity);
                    }
                    _roleActivityRepository.SaveChanges();
                }
                //_logger.LogTrace("RoleActivity save success!!");
                return ServiceResult.SuccessResult(role.Id);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.ToString());
                return ServiceResult.ErrorResult("Ошибка при создании роли");
            }
        }


        public ObjRoleEdit GetRoleForEdit(int id)
        {
            var role = _roleRepository.GetFull(id);
            var model = new ObjRoleEdit()
            {
                Id = role.Id,
                Title = role.Title,
                Name = role.Name,
                SelectedActivities = role.RoleActivitys
                .Select(x => new ObjActivitySelect()
                {
                    Name = x.Activity,
                    Checked = true
                })
                .ToList()
            };
            return model;
        }

        public ServiceResult EditRole(ObjRoleEdit model)
        {
            try
            {
                //_logger.LogTrace("Start edti role saving");
                //обновляем роль
                var role = _roleRepository.GetFull(model.Id);

                if (role == null)
                {
                    return ServiceResult.ErrorResult("Отсутствует редактируемая роль");
                }
                //_logger.LogTrace("Change Role {0}", role.Title);
                var roleTitle = model.Title.Trim();
                var roleName = model.Name.Trim();
                var roles = _roleRepository.GetRoles();
                if (roles.Any(r => string.Equals(r.Title, roleTitle) && r.Id != model.Id))
                {
                    return ServiceResult.ErrorResult("Роль с таким именем уже существует");
                }

                role.Title = roleTitle;
                role.Name = roleName;

                _roleRepository.Update(role);
                _roleRepository.SaveChanges();
                //_logger.LogTrace("Edit role save");
                role.RoleActivitys.ForEach(x => _roleActivityRepository.Delete(x.Id));

                //Получаем названия всех выбранных разрешений
                var selectedPemissionNames = model.SelectedActivities
                    .Where(x => x.Checked)
                    .Select(x => x.Name)
                    .ToList();

                //Получаем по именам 
                var selectedPermissions = ActivityHelper.GetActivities()
                    .SelectMany(x => x.Permisioins)
                    .Where(x => selectedPemissionNames.Contains(x.Key))
                    .Select(x => x.Key)
                    .ToList();

                if (selectedPermissions.Any())
                {
                    foreach (var permission in selectedPermissions)
                    {
                        var RoleActivity = new RoleActivity()
                        {
                            RoleId = role.Id,
                            Activity = permission
                        };
                        _roleActivityRepository.Insert(RoleActivity);
                    }
                    _roleActivityRepository.SaveChanges();
                }
                //_logger.LogTrace("Role activity save");
                return ServiceResult.SuccessResult(role.Id);
            }
            catch(Exception ex)
            {
                //_logger.LogError("Saving Role error id = {0} / error: {1}", model.Id, ex);
                return ServiceResult.ErrorResult("Ошибка при сохранении изменений роли");
            }
           
        }

        public ServiceResult DeleteRole(int id)
        {
            try
            {
                _roleRepository.Delete(id);
                _roleRepository.SaveChanges();

                return ServiceResult.SuccessResult();
            }
            catch(Exception ex)
            {
                //_logger.LogError("Delete Role id = {0} / message: {1}", id, ex);

                return ServiceResult.ErrorResult("Ошибка при удалении роли");
            }
            
        }
#endregion
        #region Map
        private User Map(ObjUser objUser)
        {
            User user = new User
            {
                Id = objUser.Id,
                Fio = objUser.Fio,
                Position = objUser.Position,
                Phone = objUser.Phone,
                Email = objUser.Email,
                Grade = objUser.Grade,
                Skype = objUser.Skype,
                Telegram = objUser.Telegram,
                Other = objUser.Other,
                PassHash = objUser.PassHash,
                RoleId = objUser.RoleId,
                IsManager = objUser.IsManager,
                AvatarFileName = objUser.AvatarFileName
            };
            return user;
        }

        private ObjUser Map(User user)
        {
            if (user == null)
                return null;

            ObjUser objUser = new ObjUser
            {
                Id = user.Id,
                Fio = user.Fio,
                Position = user.Position,
                Phone = user.Phone,
                Email = user.Email,
                Grade = user.Grade,
                Skype = user.Skype,
                Telegram = user.Telegram,
                Other = user.Other,
                PassHash = user.PassHash,
                Role = GetRoleByLogin(user.RoleId),
                RoleId = user.RoleId,
                IsManager = user.IsManager,
                AvatarFileName = user.AvatarFileName
            };
            return objUser;
        }

        private void UpdateMapping(ObjUser editUser, User dbUser)
        {
            dbUser.Fio = editUser.Fio;
            dbUser.Email = editUser.Email;
            dbUser.Grade = editUser.Grade;
            dbUser.Other = editUser.Other;
            dbUser.Phone = editUser.Phone;
            dbUser.Position = editUser.Position;
            dbUser.Skype = editUser.Skype;
            dbUser.Telegram = editUser.Telegram;
            dbUser.RoleId = editUser.RoleId;
            dbUser.IsManager = editUser.IsManager;
            dbUser.AvatarFileName = editUser.AvatarFileName;
        }

        public ObjFileStream GetFileAvatarStream(int id)
        {
            if (id==0)
            {
                return null;
            }
            var file = _userRepository.Get(id);
            if (string.IsNullOrEmpty(file.AvatarFileName))
            {
                return null;
            }
            var path = Path.Combine(CoreConfiguration.PathAvatar, file.AvatarFileName);

            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                var result = new ObjFileStream();
                result.OriginalName = file.AvatarFileName;
                result.Data = File.ReadAllBytes(path);
                return result;
            }
            else
            {
                return null; 
            }
        }

        #endregion
    }
}
