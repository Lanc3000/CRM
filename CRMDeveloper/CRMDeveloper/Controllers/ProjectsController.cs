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
    public class ProjectsController : Base.BaseController 
    {
        IProjectService _projectService { get; }
        IUserService _userService { get; }
        IParticipantService _participantService { get; }
        IStatusService _statusService { get; }
        IFinanceService _financeService { get; set; }
        IClientService _clientService { get; set; }

        public ProjectsController(IProjectService projectService,
            IUserService userService,
            IParticipantService participantService,
            IStatusService statusService,
            IFinanceService financeService,
            IClientService clientService)
        {
            _projectService = projectService;
            _userService = userService;
            _participantService = participantService;
            _statusService = statusService;
            _financeService = financeService;
            _clientService = clientService;
        }

        [ActivityAuth(ObjActivities.Project)]
        public IActionResult Index(ProjectsModel model)
        {
            var projectList = _projectService.GetViewModel(model);
            return View(projectList);
        }

        //Todo переделать для выборки из всех пользователей
        [HttpGet]
        [ActivityAuth(ObjActivities.ProjectEdit)]
        public IActionResult Details(int id)
        {
            ViewBag.Managers = _userService.GetManagers();
            ViewBag.Users = _userService.GetAllUsers();
            ViewBag.Subtypes = _financeService.GetFSubTypes();
            ViewBag.Statuses = _statusService.GetStatusesByRootType(RootTypes.Project);
            ViewBag.Projects = _projectService.GetProjectList();
            ViewBag.ProjectTypes = _projectService.GetProjectTypes();

            return View(_projectService.Get(id));
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.ProjectEdit)]
        public IActionResult ProjectAdd()
        {
            ViewBag.Statuses = _statusService.GetStatusesByRootType(RootTypes.Project);
            ViewBag.ProjectTypes = _projectService.GetProjectTypes();
            ViewBag.Managers = _userService.GetManagers();
            ViewBag.Clients = _clientService.GetListClientP();
            return View();
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.ProjectEdit)]
        public IActionResult ProjectAdd(ObjProject obj)
        {
            var result = _projectService.SaveProject(obj);
            if(result.Success)
                return RedirectToActionOk("Details", "Projects", new { id = result.Id }, "Проект добавлен");

            return RedirectToActionError("ProjectAdd", "Ошибка сохранения проекта");
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.ProjectEdit)]
        public IActionResult EditProject(ObjProject obj)
        {
            var result = _projectService.EditProject(obj);
            if(result.Success)
                return RedirectToActionOk("Details", "Projects", new { id = result.Id }, "Проект добавлен");

            return RedirectToActionError("Details", "Ошибка редактирования проекта");
        }

        #region Modul
        [HttpPost]
        public IActionResult EditProjectJS(ObjProject obj)
        {
            var result = _projectService.EditProject(obj);
            if(result.Success)
                return Json(_projectService.GetProjectList(obj.RootId, obj.RootType));
            return Json(result);
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.ProjectEdit)]
        public IActionResult AddProject(ObjProject obj)
        {
            var result = _projectService.SaveProject(obj);
            if (result.Success)
                return Json(_projectService.GetProjectList(obj.RootId, obj.RootType));
            return Json(result);

        }

        public IActionResult GetProjectList(int rootId, RootTypes rootType)
        {
            var projectList = _projectService.GetProjectList(rootId, rootType);
            return Json(projectList);
        }
        [HttpPost]
        public IActionResult DeleteProject(int id, int rootId, RootTypes rootType)
        {
            var result = _projectService.Delete(id);
            if(result.Success)
                return Json(_projectService.GetProjectList(rootId, rootType));
            return Json(result);
        }
        #endregion

    }
}