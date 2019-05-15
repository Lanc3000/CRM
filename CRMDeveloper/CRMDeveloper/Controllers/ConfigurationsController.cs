using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CRMCore.Services;
using CRMCore.Objects;
using CRMCore.Enums;
using Microsoft.AspNetCore.Authorization;
using CRMDeveloper.Config;
using CRMDeveloper.Models;

namespace CRMDeveloper.Controllers
{
    [Authorize]
    public class ConfigurationsController : Base.BaseController
    {
        readonly IUserService _userService;
        readonly IStatusService _statusService;
        readonly IParticipantService _participantService;

        readonly IFinanceService _financeService;
        readonly IProjectService _projectService;

        ISourceService _sourceService;

        readonly ICustomOptionsService _customOptionsService;

        public ConfigurationsController(IUserService userService,
            IStatusService statusService,
            IParticipantService participantService,
            IFinanceService financeService,
            IProjectService projectService,
            ISourceService sourceService,
            ICustomOptionsService customOptionsService)
        {
            _userService = userService;
            _statusService = statusService;
            _participantService = participantService;
            _financeService = financeService;
            _projectService = projectService;
            _sourceService = sourceService;
            _customOptionsService = customOptionsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ConfigurationModel model = new ConfigurationModel();
            model.Sources = _sourceService.All();
            model.objFinanceSubTypes = _financeService.GetFSubTypes();
            model.Tasks = _participantService.GetTasks();
            model.ProjectTypes = _projectService.GetProjectTypes();
            return View(model);
        }


        [HttpGet]
        public IActionResult CustomOptions()
        {
            var model = _customOptionsService.GetVM();

            return View(model);
        }

        [HttpGet]
        public IActionResult EditCustomOption(int id)
        {
            var customOption = _customOptionsService.Get(id);
            return View(customOption);
        }

        [HttpPost]
        public IActionResult EditCustomOption(EditCustomOption editCustom)
        {
            if(ModelState.IsValid)
            {
                if (editCustom.Id == 0)
                {
                    var result = _customOptionsService.Save(editCustom);
                    if (result.Success)
                        return RedirectToActionOk("CustomOptions", "Элемент успешно сохранен");
                    else
                        return RedirectToActionError("CustomOptions", result.ErrorMessage);
                }
                else
                {
                    var result = _customOptionsService.Update(editCustom);
                    if (result.Success)
                        return RedirectToActionOk("CustomOptions", "Элемент успешно обновлен");
                    else
                        return RedirectToActionError("CustomOptions", result.ErrorMessage);
                }
            }

            return View(editCustom);
            
        }

        [HttpGet]
        public IActionResult DeleteCustomOption(int id)
        {
            var result = _customOptionsService.Delete(id);
            if (result.Success)
                return RedirectToActionOk("CustomOptions", "Элеменнт успешно удален");
            else
                return RedirectToActionError("CustomOptions", result.ErrorMessage);
        }


        #region Roles
        [HttpGet]
        [ActivityAuth(ObjActivities.Role)]
        public ActionResult Roles()
        {
            var ObjRoles = _userService.GetRoles();
            return View(ObjRoles);
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.RoleEdit)]
        public ActionResult NewRole()
        {
            return View(new ObjRoleEdit());
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.RoleEdit)]
        public IActionResult NewRole(ObjRoleEdit objRoleEdit)
        {
            if (!ModelState.IsValid)
            {
                return View(objRoleEdit);
            }

            var newId = _userService.AddRole(objRoleEdit);

            return RedirectToAction("Roles");
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.RoleEdit)]
        public IActionResult EditRole(int id)
        {
            var role = _userService.GetRoleForEdit(id);
            return View(role);
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.RoleEdit)]
        public IActionResult EditRole(ObjRoleEdit objRoleEdit)
        {
            if (!ModelState.IsValid)
            {
                return View(objRoleEdit);
            }
            var result = _userService.EditRole(objRoleEdit);
            if (result.Success)
                return RedirectToActionOk("Roles", "Роль успешно создана");
            return RedirectToActionError("EditRole", "Configurations", objRoleEdit, result.ErrorMessage);
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.RoleEdit)]
        public IActionResult DeleteRole(int id)
        {
            _userService.DeleteRole(id);
            return RedirectToAction("Roles");

        }
        #endregion

        #region Statuses
        [ActivityAuth(ObjActivities.Status)]
        public ActionResult Statuses()
        {
            var model = _statusService.GetAllStatuses();
            return View(model);
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.StatusEdit)]
        public ActionResult AddStatus()
        {
            return View();
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.StatusEdit)]
        public ActionResult AddStatus(ObjStatus objStatus)
        {

            _statusService.AddStatus(objStatus);
            return RedirectToAction("Statuses");
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.StatusEdit)]
        public ActionResult EditStatus(int id)
        {
            var status = _statusService.Get(id);
            return View(status);
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.StatusEdit)]
        public ActionResult EditStatus(ObjStatus model)
        {
            _statusService.Update(model);
            return RedirectToAction("Statuses");
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.StatusEdit)]
        public ActionResult DeleteStatus(int id)
        {
            _statusService.Delete(id);
            return RedirectToAction("Statuses");
        }       
        #endregion

        #region PTask
        [HttpGet]
        [ActivityAuth(ObjActivities.Task)]
        public ActionResult PTasks()
        {
            var model = _participantService.GetTasks();
            return View(model);
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.TaskEdit)]
        public ActionResult AddPTask()
        {
            return View(new ObjPTask());
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.TaskEdit)]
        public ActionResult AddPTask(ObjPTask objPTask)
        {
            _participantService.AddPTask(objPTask);
            return RedirectToAction("PTasks");
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.TaskEdit)]
        public ActionResult EditPTask(int id)
        {
            var model = _participantService.GetPTask(id);
            return View(model);
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.TaskEdit)]
        public ActionResult EditPTask(ObjPTask model)
        {
            _participantService.UpdatePTask(model);
            return RedirectToAction("PTasks");
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.TaskEdit)]
        public ActionResult DeletePTask (int id)
        {
            _participantService.DeletePTask(id);
            return RedirectToAction("PTasks");
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            var result = _participantService.GetNamePtasks();

            return Json(result);
        }

        #endregion

        #region FSubTypes

        [HttpGet]
        [ActivityAuth(ObjActivities.FinanceSubType)]
        public IActionResult FSubTypes()
        {
            var subTypes = _financeService.GetFSubTypes();
            return View(subTypes);
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.FinanceSubTypeEdit)]
        public IActionResult AddSubType(int id)
        {
            if (id == -1)
            {
                return View(new ObjFinanceSubType());
            }
            else
            {
                var subtype = _financeService.GetSubType(id);
                return View(subtype);
            }
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.FinanceSubTypeEdit)]
        public IActionResult AddSubType(ObjFinanceSubType obj)
        {
            if(obj.Id == -1)
            {
                obj.Id = 0;
                _financeService.Add(obj);
            }
            else
            {
                _financeService.Edit(obj);
            }
            return RedirectToAction("FSubTypes");
        }
       

        [HttpPost]
        [ActivityAuth(ObjActivities.FinanceSubTypeEdit)]
        public IActionResult DeleteSubType (int id)
        {
            _financeService.DeleteSubType(id);
            return RedirectToAction("FSubTypes");
        }
        #endregion

        #region ProjectTypes
        [ActivityAuth(ObjActivities.ProjectType)]
        public IActionResult ProjectTypes()
        {
            var projectTypes = _projectService.GetProjectTypes();
            return View(projectTypes);
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.ProjectTypeEdit)]
        public IActionResult EditProject(int id)
        {
            if(id == -1)
            {
                return View(new ObjProjectType());
            }
            else
            {
                var projectType = _projectService.GetProjectType(id);
                return View(projectType);
            }
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.ProjectTypeEdit)]
        public IActionResult EditProject(ObjProjectType obj)
        {
            if(obj.Id == -1)
            {
                obj.Id = 0;
                _projectService.Add(obj);
            }
            else
            {
                _projectService.Edit(obj);
            }
            return RedirectToAction("ProjectTypes");
        }
        [ActivityAuth(ObjActivities.ProjectTypeEdit)]
        public IActionResult DeleteProject(int id)
        {
            _projectService.DeleteProjectType(id);

            return RedirectToAction("ProjectTypes");
        }

        #endregion

        #region Source
        [ActivityAuth(ObjActivities.Source)]
        public IActionResult Sources()
        {
            var result = _sourceService.All();
            return View(result);
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.SourceEdit)]
        public IActionResult EditSource(int id)
        {
            if(id == -1)
            {
                return View(new ObjSource());
            }
            var source = _sourceService.Get(id);
            if (source == null)
                return RedirectToActionError("EditSource", "Редактируемый пользователь не найден");
            return View(source);
            
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.SourceEdit)]
        public IActionResult EditSource(ObjSource objSource)
        {
            if(objSource.Id == -1)
            {
                objSource.Id = 0;
                var result = _sourceService.Add(objSource);
                if (result.Success)
                    return RedirectToActionOk("Sources", "Источник сохранен");
                return RedirectToActionError("Sources", result.ErrorMessage);
            }
            else
            {
                var result = _sourceService.Edit(objSource);
                if(result.Success)
                    return RedirectToActionOk("Sources", "Источник сохранен");
                return RedirectToActionError("Sources", result.ErrorMessage);
            }
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.SourceEdit)]
        public IActionResult DeleteSource(int id)
        {
            var result = _sourceService.Delete(id);
            if (result.Success)
                return RedirectToActionOk("Sources", "Источник успешно удален");

            return RedirectToActionError("Sources", result.ErrorMessage);
        }

        #endregion
    }
}