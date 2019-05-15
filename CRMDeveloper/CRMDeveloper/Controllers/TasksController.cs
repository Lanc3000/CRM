using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CRMDeveloper.Controllers.Base;

using CRMCore.Services;
using CRMCore.Enums;
using CRMCore.Objects;
using Microsoft.Extensions.Logging;
using CRMDeveloper.Config;

namespace CRMDeveloper.Controllers
{
    [Authorize]
    public class TasksController : Base.BaseController
    {
        ITaskService _taskService { get; }
        IUserService _userService { get; }
        IParticipantService _participantService { get; }
        IStatusService _statusService { get; }
        IFinanceService _financeService { get; set; }
        IClientService _clientService { get; set; }

        public TasksController(ITaskService taskService,
            IUserService userService,
            IParticipantService participantService,
            IStatusService statusService,
            IFinanceService financeService,
            IClientService clientService)
        {
            _taskService = taskService;
            _userService = userService;
            _participantService = participantService;
            _statusService = statusService;
            _financeService = financeService;
            _clientService = clientService;
        }

        [ActivityAuth(ObjActivities.Task)]
        public IActionResult Index(TasksModel model)
        {
            var taskList = _taskService.GetViewModel(model);
            return View(taskList);
        }

        //Todo переделать для выборки из всех пользователей
        [HttpGet]
        [ActivityAuth(ObjActivities.TaskEdit)]
        public IActionResult Details(int id)
        {
            ViewBag.Managers = _userService.GetManagers();
            ViewBag.Users = _userService.GetAllUsers();
            ViewBag.Subtypes = _financeService.GetFSubTypes();
            ViewBag.Statuses = _statusService.GetStatusesByRootType(RootTypes.Project);
            ViewBag.Tasks = _taskService.GetTaskList();
            ViewBag.TaskTypes = _taskService.GetTaskTypes();

            return View(_taskService.Get(id));
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.TaskEdit)]
        public IActionResult TaskAdd()
        {
            ViewBag.Statuses = _statusService.GetStatusesByRootType(RootTypes.Project);
            ViewBag.ProjectTypes = _taskService.GetTaskTypes();
            ViewBag.Managers = _userService.GetManagers();
            ViewBag.Clients = _clientService.GetListClientP();
            return View();
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.TaskEdit)]
        public IActionResult TaskAdd(ObjTask obj)
        {
            var result = _taskService.SaveTask(obj);
            if (result.Success)
                return RedirectToActionOk("Details", "Tasks", new { id = result.Id }, "Задача добавлена");

            return RedirectToActionError("TaskAdd", "Ошибка сохранения задачи");
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.TaskEdit)]
        public IActionResult EditTask(ObjTask obj)
        {
            var result = _taskService.EditTask(obj);
            if (result.Success)
                return RedirectToActionOk("Details", "Tasks", new { id = result.Id }, "Задача добавлена");

            return RedirectToActionError("Details", "Ошибка редактирования пзадачи");
        }

        #region Modul
        [HttpPost]
        public IActionResult EditTaskJS(ObjTask obj)
        {
            var result = _taskService.EditTask(obj);
            if (result.Success)
                return Json(_taskService.GetTaskList(obj.RootId, obj.RootType));
            return Json(result);
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.TaskEdit)]
        public IActionResult AddTask(ObjTask obj)
        {
            var result = _taskService.SaveTask(obj);
            if (result.Success)
                return Json(_taskService.GetTaskList(obj.RootId, obj.RootType));
            return Json(result);

        }

        public IActionResult GetTaskList(int rootId, RootTypes rootType)
        {
            var taskList = _taskService.GetTaskList(rootId, rootType);
            return Json(taskList);
        }
        [HttpPost]
        public IActionResult DeleteTask(int id, int rootId, RootTypes rootType)
        {
            var result = _taskService.Delete(id);
            if (result.Success)
                return Json(_taskService.GetTaskList(rootId, rootType));
            return Json(result);
        }
        #endregion

    }
}