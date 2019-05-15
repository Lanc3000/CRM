using Microsoft.AspNetCore.Mvc;
using CRMCore.Extensions;

namespace CRMDeveloper.Controllers.Base
{
    public class BaseController : Controller
    {
        protected IActionResult RedirectToActionOk(string action, string controller, string message)
        {
            TempDataExtensions.SetRedirectMessage(TempData, message);
            TempDataExtensions.SetRedirectStatus(TempData, RedirectStatus.Success);
            return RedirectToAction(action, controller);
        }

        protected IActionResult RedirectToActionOk(string action, string controller, object routeValues, string message)
        {
            TempDataExtensions.SetRedirectMessage(TempData, message);
            TempDataExtensions.SetRedirectStatus(TempData, RedirectStatus.Success);
            return RedirectToAction(action, controller, routeValues);
        }

        protected IActionResult RedirectToActionOk(string action, string message)
        {
            TempDataExtensions.SetRedirectMessage(TempData, message);
            TempDataExtensions.SetRedirectStatus(TempData, RedirectStatus.Success);
            return RedirectToAction(action);
        }

        protected IActionResult RedirectToActionError(string action, string message)
        {
            TempDataExtensions.SetRedirectMessage(TempData, message);
            TempDataExtensions.SetRedirectStatus(TempData, RedirectStatus.Error);
            return RedirectToAction(action);
        }

        protected IActionResult RedirectToActionError(string action, string controller, string message)
        {
            TempDataExtensions.SetRedirectMessage(TempData, message);
            TempDataExtensions.SetRedirectStatus(TempData, RedirectStatus.Error);
            return RedirectToAction(action, controller);
        }

        protected IActionResult RedirectToActionError(string action, string controller, object routeValues, string message)
        {
            TempDataExtensions.SetRedirectMessage(TempData, message);
            TempDataExtensions.SetRedirectStatus(TempData, RedirectStatus.Error);
            return RedirectToAction(action, controller, routeValues);
        }

        protected IActionResult ViewError(string viewName, object routeValues, string message)
        {
            TempDataExtensions.SetRedirectMessage(TempData, message);
            TempDataExtensions.SetRedirectStatus(TempData, RedirectStatus.Error);
            return View(viewName, routeValues);
        }

        protected IActionResult ViewOk(string viewName, object routeValues, string message)
        {
            TempDataExtensions.SetRedirectMessage(TempData, message);
            TempDataExtensions.SetRedirectStatus(TempData, RedirectStatus.Success);
            return View(viewName, routeValues);
        }
    }
}
