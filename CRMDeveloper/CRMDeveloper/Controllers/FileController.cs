using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using CRMDeveloper.Config;
using CRMCore.Objects;
using CRMCore.Services;
using CRMCore.Enums;
using Microsoft.AspNetCore.Authorization;

namespace CRMDeveloper.Controllers
{
    [Authorize]
    public class FileController : Base.BaseController
    {
        IFileDataService _fileDataService { get; }
        IUserService _userService { get; }
        IHostingEnvironment _hostingEnvironment { get; }


        public FileController(IFileDataService fileDataService,
            IUserService userService, IHostingEnvironment hostingEnvironment)
        {
            _fileDataService = fileDataService;
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [RequestFormSizeLimit]
        [RequestSizeLimit(1024000000)]
        public IActionResult New(int rootId, RootTypes rootType, IFormFile file)
        {
            string email = HttpContext.User.Identity.Name;
            var createdId = _userService.GetUserByEmail(HttpContext.User.Identity.Name).Id;
            var result = _fileDataService.UploadFile(rootId, rootType, file, createdId);
            return Json(result);
        }

        public IActionResult DownLoadFile(int id)
        {
            var file = _fileDataService.GetFileStream(id);
            if (file == null)
            {
                return NotFound();
            }
            return File(file.Data,"application/octet-stream", file.OriginalName);
        }

        public IActionResult DeleteFile(int id, int rootId, RootTypes rootType)
        {
            var result = _fileDataService.Delete(id);
            if(result.Success)
                return Json(_fileDataService.GetFiles(rootId, rootType));

            return Json(result);

        }

        public IActionResult GetFileList(int rootId, RootTypes rootType)
        {
            return Json(_fileDataService.GetFiles(rootId, rootType));
        }
    }
}