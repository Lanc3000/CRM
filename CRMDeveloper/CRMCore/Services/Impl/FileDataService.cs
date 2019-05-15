using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

using CRMCore.Objects;
using CRMCore.Enums;
using CRMCore.Services;
using CRMCore.Repositories;
using System.Linq;
using CRMCore.DB;
using CRMCore.Extensions;
using CRMCore.Helpers;

namespace CRMCore.Services.Impl
{
    public class FileDataService : IFileDataService
    {
        FileHelper _fileHelper = new FileHelper();

        IFileDataRepository _fileDataRepository { get; }
        IHostingEnvironment _hostingEnvironment { get; }
        IUserRepository _userRepository { get; }

        public FileDataService(IFileDataRepository fileDataRepository,
            IHostingEnvironment hostingEnvironment, IUserRepository userRepository)
        {
            _fileDataRepository = fileDataRepository;
            _hostingEnvironment = hostingEnvironment;
            _userRepository = userRepository;
        }


        public ObjFileDataList GetFiles(int rootId, RootTypes rootType)
        {
            ObjFileDataList dataList = new ObjFileDataList();
            dataList.Files = new List<ObjFileData>();

            var files = _fileDataRepository.GetFiles(rootId, rootType);

            foreach (var file in files)
            {
                dataList.Files.Add(Map(file));
            }
            return dataList;
        }

        public ServiceResult Delete(int id)
        {
            try
            {
                var file = _fileDataRepository.Get(id);
                var path = Path.Combine(CoreConfiguration.PathStorage, file.FileName);
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                {
                    fileInf.Delete();
                }
                _fileDataRepository.Delete(id);
                _fileDataRepository.SaveChanges();

                return ServiceResult.SuccessResult();
            }
            catch (Exception ex)
            {
                return ServiceResult.ErrorResult("Ошибка удаления файла");
            }
        }


        private ObjFileData Map(FileData file)
        {
            var result = new ObjFileData
            {
                Id = file.Id,
                RootId = file.RootId,
                RootType = file.RootType,
                OriginalName = file.OriginalName,
                FileName = file.FileName,
                CreatedId = file.CreatedId
            };

            if (result.CreatedId.HasValue)
            {
                var user = _userRepository.Get(result.CreatedId.Value);
                result.CreatedName = user.Fio;
                result.CreatedPhotoUrl = user.Avatar;
            }

            return result;
        }
        private FileData Map(ObjFileData obj)
        {
            return new FileData
            {
                Id = obj.Id,
                RootId = obj.RootId,
                RootType = obj.RootType,
                OriginalName = obj.OriginalName,
                FileName = obj.FileName,
                CreatedId = obj.CreatedId
            };
        }

        public ServiceResult<ObjFileData> UploadFile(int rootId, RootTypes rootType, IFormFile file, int? createdId)
        {
            var originalName = file.FileName.Trim();

            var extension = Path.GetExtension(originalName);
            var fileName = Guid.NewGuid().ToString("N") + extension;
            var path = Path.Combine(CoreConfiguration.PathStorage, fileName);

            _fileHelper.SaveFile(file, path);

            var dbFile = new FileData()
            {
                FileName = fileName,
                OriginalName = originalName,
                CreatedId = createdId,
                RootId = rootId,
                RootType = rootType
            };
            _fileDataRepository.Insert(dbFile);
            _fileDataRepository.SaveChanges();

            return ServiceResult<ObjFileData>.SuccessResult(Map(dbFile));

        }

        public ObjFileStream GetFileStream(int id)
        {
            var file = _fileDataRepository.Get(id);
            if (string.IsNullOrEmpty(file.FileName))
            {
                return null;
            }
            var path = Path.Combine(CoreConfiguration.PathStorage, file.FileName);

            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                var result = new ObjFileStream();
                result.OriginalName = file.OriginalName;
                result.Data = File.ReadAllBytes(path);
                return result;
            }
            else
            {
                return null;
            }

        }
    }
}

