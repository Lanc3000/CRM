using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using CRMCore.Services;
using CRMCore.Objects;
using CRMCore.Enums;

namespace CRMDeveloper.Controllers
{
    [Authorize]
    public class ContactsController : Base.BaseController
    {
        private IContactService _contactService { get; }
        private IUserService _userService { get; }

        public ContactsController(IContactService contactService,
            IUserService userService)
        {
            _contactService = contactService;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult GetContactList(int rootId, RootTypes rootType)
        {
            return Json(_contactService.GetListContacts(rootId, rootType));
        }
        [HttpPost]
        public IActionResult AddContact(ObjContact obj)
        {
            obj.CreatedId = _userService.GetUserByEmail(HttpContext.User.Identity.Name).Id;
            _contactService.AddContact(obj);
            return Json(_contactService.GetListContacts(obj.RootId, obj.RootType));
        }
        [HttpPost]
        public IActionResult DeleteContact(int id, int rootId, RootTypes rootType)
        {
            _contactService.DeleteContact(id);
            return Json(_contactService.GetListContacts(rootId,rootType));
        }
        [HttpPost]
        public IActionResult EditContact(ObjContact obj)
        {
            obj.CreatedId = _userService.GetUserByEmail(HttpContext.User.Identity.Name).Id;
            _contactService.EditContact(obj);
            return Json(_contactService.GetListContacts(obj.RootId, obj.RootType));
        }
    }
}