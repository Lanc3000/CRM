using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using CRMDeveloper.Models;
using CRMCore.Services;
using CRMCore.Enums;
using CRMCore.Helpers;
using Microsoft.Extensions.Logging;

namespace CRMDeveloper.Controllers
{
    public class HomeController : Base.BaseController
    {
        private IDeleteEntityService _deleteEntityService { get; }
        //private readonly ILogger<HomeController> _logger;

        public HomeController(IDeleteEntityService deleteEntityService
           /* ILogger<HomeController> logger*/)
        {
            _deleteEntityService = deleteEntityService;
            //_logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DeleteEntity(int Id, RootTypes rootType)
        {
            var result =  _deleteEntityService.Delete(Id, rootType);
            if (result.Success)
                return RedirectToActionOk("Index",result.Result,"Объект удален");
            else
                return RedirectToActionError("Index", "Home",result.ErrorMessage);
        }
    }
}
