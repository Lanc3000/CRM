using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using CRMCore.Services;
using CRMCore.Objects;
using CRMCore.Enums;
using CRMDeveloper.Config;

namespace CRMDeveloper.Controllers
{
    [Authorize]
    public class ClientsController : Base.BaseController
    {
        IClientService _clientService { get; }
        IUserService _userService { get; }
        IProjectService _projectService { get; }
        IFinanceService _financeService { get; set; }
        IStatusService _statusService { get; }

        public ClientsController(IClientService clientService, 
            IUserService userService,
            IProjectService projectService,
            IFinanceService financeService,
            IStatusService statusService)
        {
            _clientService = clientService;
            _userService = userService;
            _projectService = projectService;
            _financeService = financeService;
            _statusService = statusService;
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.Client)]
        public IActionResult Index()
        {
            return View(_clientService.GetListClientWithProjects());
        }

        [HttpGet]
        [ActivityAuth(ObjActivities.Client)]
        public IActionResult Details(int id)
        {
            ViewBag.Managers = _userService.GetManagers();
            ViewBag.ProjectTypes = _projectService.GetProjectTypes();
            ViewBag.Users = _userService.GetAllUsers();
            ViewBag.Subtypes = _financeService.GetFSubTypes();
            ViewBag.Projects = _projectService.GetProjectList();
            ViewBag.ProjectTypes = _projectService.GetProjectTypes();
            ViewBag.Statuses = _statusService.GetStatusesByRootType(RootTypes.Project);
            return View(_clientService.Get(id));
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.ClientEdit)]
        public IActionResult Details(ObjClient objClient)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Managers = _userService.GetManagers();
                return View(objClient);
            }
            _clientService.EditClient(objClient);
            return RedirectToAction("Details", new { id = objClient.Id });
        }
        [HttpGet]
        [ActivityAuth(ObjActivities.ClientEdit)]
        public IActionResult AddClient()
        {
            ViewBag.Managers = _userService.GetManagers();
            return View(new ObjClient());
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.ClientEdit)]
        public IActionResult AddClient(ObjClient obj)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Managers = _userService.GetManagers();
                return View(obj);
            }
            var result = _clientService.AddClient(obj);
            if (result.Success)
                return RedirectToActionOk("Details", "Clients", new { id = result.Id }, "Клиент успешно создан");

            return RedirectToActionError("AddClient", result.ErrorMessage);
        }

        [HttpPost]
        [ActivityAuth(ObjActivities.ClientEdit)]
        /// <summary>
        /// Первод лида в клиенты
        /// </summary>
        /// <param name="id">PotentialClientId</param>
        /// <returns></returns>
        public IActionResult ConvertToClients(int PClientid)
        {
           var result = _clientService.ConvertToClient(PClientid);
            if (result.Success)
            {
                return RedirectToActionOk("Details", "Clients", new { id = result.Id }, "Клиент успешно создан из лида");
            }
                

            return RedirectToActionError("Details", "PotentialClients", new { id = PClientid }, result.ErrorMessage);
        }
    }
}