using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CRMCore.Services;
using CRMCore.Enums;
using CRMCore.Objects;
using Microsoft.AspNetCore.Authorization;
using CRMDeveloper.Config;

namespace CRMDeveloper.Controllers
{
    [Authorize]
    public class FinanceController : Base.BaseController
    {
        IFinanceService _financeService { get; set; }
        IUserService _userService { get; set; }

        public FinanceController(IFinanceService financeService,
            IUserService userService)
        {
            _financeService = financeService;
            _userService = userService;
        }


        [HttpGet]
        [ActivityAuth(ObjActivities.Finance)]
        public IActionResult Index()
        {
            var result = _financeService.All();
            return View(result);
        }

        [HttpGet]
        public IActionResult GetFinancesList (int rootId, RootTypes rootType)
        {
            var result = _financeService.GetList(rootId, rootType);
            return Json(result);
        }

        [HttpPost]
        public IActionResult AddFinance (ObjFinance objFinance)
        {
            objFinance.CreatedId = _userService.GetUserByEmail(HttpContext.User.Identity.Name).Id;
            var result = _financeService.Add(objFinance);
            if (result.Success)
                return RedirectToAction("GetFinancesList", new { rootId = objFinance.RootId, rootType = objFinance.RootType });
            return Json(result);

        }

        [HttpPost]
        public IActionResult DeleteFinance (int id, int rootId, RootTypes rootType)
        {
            var result = _financeService.Delete(id);
            if (result.Success)
                return RedirectToAction("GetFinancesList", new { rootId, rootType });

            return Json(result);

        }
    }
}