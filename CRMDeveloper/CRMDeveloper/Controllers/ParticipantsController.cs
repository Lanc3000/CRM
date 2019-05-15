using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CRMCore.Enums;
using CRMCore.Services;
using CRMCore.Objects;
using Microsoft.AspNetCore.Authorization;

namespace CRMDeveloper.Controllers
{
    [Authorize]
    public class ParticipantsController : Base.BaseController
    {
        private IParticipantService _participantService { get; }
        private IUserService _userService { get; }
        private ICurrentUser _currentUser { get; }

        public ParticipantsController(IParticipantService participantService, IUserService userService,
            ICurrentUser currentUser)
        {
            _participantService = participantService;
            _userService = userService;
            _currentUser = currentUser;
        }


        [HttpGet]
        public IActionResult GetParticipantList(int rootId, RootTypes rootType)
        {
            return Json(_participantService.GetList(rootId, rootType));
        }

        [HttpPost]
        public IActionResult AddParticipant(ObjParticipant obj)
        {
            obj.CreatedId = _userService.GetUserByEmail(_currentUser.Email).Id;
            _participantService.Add(obj);
            return Json(_participantService.GetList(obj.RootId, obj.RootType));
        }

        [HttpPost]
        public IActionResult DeleteParticipant(int id, int rootId, RootTypes rootType)
        {
            _participantService.Delete(id);
            return Json(_participantService.GetList(rootId, rootType));
        }

        [HttpPost]
        public IActionResult EditParticipant(ObjParticipant obj)
        {
            obj.CreatedId = _userService.GetUserByEmail(HttpContext.User.Identity.Name).Id;
            _participantService.Edit(obj);
            return Json(_participantService.GetList(obj.RootId, obj.RootType));
        }

    }
}