using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using CRMCore.Services;
using CRMCore.Objects;
using CRMCore.Helpers;
using CRMCore.Enums;
using Newtonsoft.Json;
using CRMDeveloper.Config;

namespace CRMDeveloper.Controllers
{
    [Authorize]
    public class PotentialClientsController : Base.BaseController
    {
        private IPotentialClientService _potentialClientService { get; }
        private IUserService _userService { get; }
        IStatusService _statusService { get; }
        IProjectService _projectService { get; }
        ISourceService _sourceService;

        public PotentialClientsController(IPotentialClientService potentialClientService,
            IUserService userService,
            IStatusService statusService,
            IProjectService projectService,
            ISourceService sourceService)
        {
            _potentialClientService = potentialClientService;
            _userService = userService;
            _statusService = statusService;
            _projectService = projectService;
            _sourceService = sourceService;
        }

        [ActivityAuth(ObjActivities.PotentialClient)]
        public IActionResult Index(PotentialClientsModel model)
        {
            model = _potentialClientService.GetViewModel(model);
            return View(model);
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.PotentialClient)]
        public IActionResult Details(int Id)
        {
            ViewBag.Statuses = _statusService.GetStatusesByRootType(RootTypes.PotentialCLient);
            ViewBag.Managers = _userService.GetManagers();
            ViewBag.ProjectTypes = _projectService.GetProjectTypes();
            ViewBag.Sources = _sourceService.All();
            return View(_potentialClientService.GetById(Id));
        }


        [HttpGet]
        [ActivityAuth(ObjActivities.PotentialClient)]
        public IActionResult AddPotentialClient()
        {
            ViewBag.Statuses = _statusService.GetStatusesByRootType(RootTypes.PotentialCLient);
            ViewBag.Managers = _userService.GetManagers();
            ViewBag.ProjectTypes = _projectService.GetProjectTypes();
            ViewBag.Sources = _sourceService.All();
            return View(new ObjPotentialClient());
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.PotentialClient)]
        public IActionResult AddPotentialClient(ObjPotentialClient obj)
        {
            //if(!ModelState.IsValid)
            //{
            //    ViewBag.Managers = _userService.GetManagers();
            //    return View(obj);
            //}

            var result = _potentialClientService.SavePCLient(obj);
            if (result.Success)
                return RedirectToActionOk("Details", "PotentialClients", new { id = result.Id }, "Потенциальный клиент успешно создан");
            
            return RedirectToActionError("AddPotentialClient", "PotentialClients", result.ErrorMessage);
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.PotentialClient)]
        public IActionResult Details(ObjPotentialClient obj)
        {
            
            _potentialClientService.EditPClient(obj);
            return RedirectToAction("Details", new { id = obj.Id });
        }
    }
}