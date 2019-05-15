using CRMCore.DB;
using CRMCore.Enums;
using CRMCore.Objects;
using CRMCore.Objects.Comments;
using CRMCore.Repositories;
using CRMCore.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace CRMCore.Services.Impl
{
    public class CommentService : ICommentsService
    {
        private ICommentRepository _commentRepository { get; }
        private IUserRepository _userRepository { get; }

        //TOdo ПОменять на репу
        private IFileDataService _fileDataService { get; }

        public CommentService(ICommentRepository commentRepository,
            IUserRepository userRepository, IFileDataService fileDataService)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _fileDataService = fileDataService;
        }

       public ObjCommentList GetListComments(int rootID, RootTypes rootType, int lastId = 0)
        {
            ObjCommentList result = new ObjCommentList();

            var listComment = _commentRepository.GetByRootIDAndType(rootID, rootType);

            if (lastId != 0)
                listComment.Where(c => c.Id == lastId);

            listComment = listComment.OrderBy(c => c.Id);

            result.items = new List<ObjComment>();

            foreach (var item in listComment)
            {
                result.items.Add(MapComment(item));
            }


            return result;
        }

        public ObjComment AddComment (ObjComment obj, List<ObjFileData> dataList)
        {
            var Comment = new Comment
            {
                Created = DateTime.Now,
                CreateId = obj.CreatedId,
                RootId = obj.RootId,
                Text = obj.Text,
                RootType = obj.RootType,
            };

            _commentRepository.Insert(Comment);
            _commentRepository.SaveChanges();

            return new ObjComment();
        }

        private ObjComment MapComment(Comment model)
        {
            ObjComment result = new ObjComment();

            // заполняем стандартные даные
            result.Id = model.Id;
            result.RootId = model.RootId;
            result.RootType = model.RootType;
            result.Text = model.Text;
            result.Create = model.Created;
            result.CreateStr = result.Create.ToString("dd.MM.yyyy HH:ss");
            result.CreatedId = model.CreateId;
            if (result.CreatedId.HasValue && result.CreatedId != 0)
            {
                var user = _userRepository.Get(result.CreatedId.Value);
                
                    if (user != null)
                    {
                        result.CreatedName = user.Fio;
                        result.CreatedPhotoUrl = user.Id.ToString();
                    }
            }
            //файлы
            //result.FilesList = _fileDataService.GetListFileDataLite(result.Id, FileRootType.Comment, model.RootType);
            return result;
        }
    }
}
