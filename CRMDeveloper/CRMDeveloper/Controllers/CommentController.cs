using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMCore.Enums;
using CRMCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


using CRMCore.Objects.Comments;
using CRMCore.Objects;
using Microsoft.AspNetCore.Authorization;

namespace CRMDeveloper.Controllers.BaseController
{
    [Authorize]
    public class CommentController : Base.BaseController
    {
        public ICommentsService _commentService { get; }
        public IUserService _userService { get; }

        public CommentController(ICommentsService commentsService,
            IUserService userService)
        {
            _commentService = commentsService;
            _userService = userService;
        }

        public IActionResult GetCOmments(int rootID, RootTypes rootType, int lastId = 0)
        {
            return Json(_commentService.GetListComments(rootID, rootType, lastId));
        }

        public IActionResult AddComment(ObjComment obj, int lastId = 0)
        {
            List<ObjFileData> files = new List<ObjFileData>();
            string email = HttpContext.User.Identity.Name;
            obj.CreatedId = _userService.GetUserByEmail(HttpContext.User.Identity.Name).Id;
            _commentService.AddComment(obj, files);
            return Json(_commentService.GetListComments(obj.RootId, obj.RootType, lastId));
        }
    }
}