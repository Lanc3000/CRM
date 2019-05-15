using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CRMCore.Objects;
using CRMCore.Services;
using CRMDeveloper.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace CRMDeveloper.Controllers
{
    [Authorize]
    public class UsersController : Base.BaseController
    {
        private IUserService _userService { get; set; }
        IFinanceService _financeService { get; set; }
        IProjectService _projectService { get; set; }

        public UsersController(IUserService userService,
            IFinanceService financeService,
            IProjectService projectService)
        {
            _userService = userService;
            _financeService = financeService;
            _projectService = projectService;
        }

        [HttpGet]
        public IActionResult Index(string objUsers = "")
        {

            var result = JsonConvert.DeserializeObject<List<ObjUser>>(objUsers) ?? null;
            if (result == null || result.Count == 0)
                result = _userService.GetAllUsers();
            

            return View(result);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            ViewBag.Roles = _userService.GetRoles();
            ViewBag.Users = _userService.GetAllUsers();
            ViewBag.Subtypes = _financeService.GetFSubTypes();
            ViewBag.Projects = _projectService.GetProjectList();
            ViewBag.ProjectTypes = _projectService.GetProjectTypes();
            var user = _userService.GetUserById(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Details(ObjUser newUser)
        {
            //if(!ModelState.IsValid)
            //{
            //    ViewBag.Roles = _userService.GetRoles();
            //    return View(newUser);
            //}
            var result = _userService.EditUser(newUser);
            if(result.Success)
            {
                Response.Cookies.Append("Name", newUser.Fio ?? "Ваше имя");
                Response.Cookies.Append("Position", newUser.Position ?? "Должность");
                return RedirectToActionOk("Details", "Users", new { id = result.Id }, "Изменения сохранены");
            }
            return RedirectToActionError("Details", "Users", new { id = newUser.Id }, result.ErrorMessage);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            ViewBag.Roles = _userService.GetRoles();
            return View(new ObjUser());
        }

        public IActionResult AddUser(ObjUser objUser)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _userService.GetRoles();
                return View(objUser);
            }
            var result = _userService.CreateUser(objUser);
            if(result.Success)
            {
                return RedirectToActionOk("Details", "Users", new { id = result.Id }, "Пользователь успешно создан");
            }

            return RedirectToActionError("AddUser", result.ErrorMessage);
        }

        [HttpGet]
        public IActionResult ChangePassword(int id)
        {
            return View(new ObjChangePassword()
            {
                Id = id,
            });
        }

        [HttpPost]
        public IActionResult ChangePassword(ObjChangePassword model)
        {
            if(model.NewPassword != model.NewPasswordConfirm)
            {
                return RedirectToActionError("ChangePassword", "Пароли не совпадают");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _userService.ChangePassword(model);
            if(result.Success)
            {
                return RedirectToActionOk("Details", "Users", new { id = result.Id }, "Пароль изменен");
            }

            return RedirectToActionError("ChangePassword", result.ErrorMessage);
            
        }

        public IActionResult SearchUser(string SearchString)
        {
            var users = _userService.Search(SearchString);
            var result = JsonConvert.SerializeObject(users);

            return RedirectToAction("Index", new { objUsers = result });
        }


        public IActionResult Avatar(int id)
        {
            var file = _userService.GetFileAvatarStream(id); if (file == null)
            {
                string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\user.jpg"}";
                var fileDefault= System.IO.File.ReadAllBytes(path);
                return File(fileDefault, "application/octet-stream", "user.jpg");
            }
            return File(file.Data, "application/octet-stream", file.OriginalName);
        }
    }
}


