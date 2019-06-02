using System;
using System.Collections.Generic;
using System.Linq;

using CRMCore.Repositories;
using CRMCore.DB;
using CRMCore.Objects;
using CRMCore.Enums;
using CRMCore.Helpers;
using Microsoft.Extensions.Logging;
using CRMCore.Extensions;

namespace CRMCore.Services.Impl
{
    public class TaskService : ITaskService
    {
        ILogger<TaskService> _Logger { get; }

        ITaskRepository _taskRepository { get; }
        IParticipantRepository _participantRepository { get; }
        ITaskTypeRepository _taskTypeRepository { get; }
        IClientRepository _clientRepository { get; }
        IStatusRepository _statusRepository { get; }
        IUserRepository _userRepository { get; }
        IRootTypesService _rootTypesService { get; }

        public TaskService(ITaskRepository taskRepository,
            IParticipantRepository participantRepository,
            ITaskTypeRepository taskTypeRepository,
            IClientRepository clientRepository,
            IStatusRepository statusRepository,
            IUserRepository userRepository,
             IRootTypesService rootTypesService,
            ILogger<TaskService> logger)
        {
            _Logger = logger;

            _taskRepository = taskRepository;
            _participantRepository = participantRepository;
            _taskTypeRepository = taskTypeRepository;
            _clientRepository = clientRepository;
            _statusRepository = statusRepository;
            _userRepository = userRepository;
            _rootTypesService = rootTypesService;
        }

        public List<ObjTask> GetTaskList()
        {
            var tasks = _taskRepository.AllFull();

            return tasks
                .Select(x => Map(x))
                .ToList();

        }

        public List<ObjTaskSecond> GetObjTaskSc()
        {
            var tasks = _taskRepository.AllFull();

            var result = tasks.Select(x => new ObjTaskSecond
            {
                Id = x.Id,
                RootId = x.RootId,
                RootType = x.RootType,

                Title = x.Title,
                Description = x.Description,

                Created = x.Created,

                StatusColor = x.Status.Color,
                StatusId = x.Status.Id,
                StatusName = x.Status.Name,

                CompeteProcent = x.CompeteProcent,          
                DeadLine = x.DeadLine,
                TaskTypeName = x.TaskType.Name,
                TaskTypeId = x.TaskTypeId,
                UserId = x.UserId,
                UserFio = x.User?.Fio,
                Participants = _participantRepository.AllFull()
               .Where(particapant => particapant.RootType == RootTypes.Project && particapant.RootId == x.Id)
               .Select(participant => new ObjParticipant()
               {
                   Id = participant.Id,
                   FIO = participant.User.Fio,
                   RootType = participant.RootType,
                   Task = participant.Task,
                   WorkSum = participant.WorkSum,
                   Currency = participant.Currency,
                   WorkPeriod = participant.WorkPeriod,
                   Residue = participant.Residue,
                   CreatedId = participant.CreatedId,
                   UserId = participant.UserId,
               })
                   .ToList()
            })
            .ToList();
            result.ForEach(u => u.RootName = _rootTypesService.GetRootName(u.RootType, u.RootId));
            result.ForEach(u => u.RootUrl = _rootTypesService.GetRootUrl(u.RootType, u.RootId));

            return result;

        }

        public ObjTaskList GetTaskList(int rootID, RootTypes rootType)
        {
            ObjTaskList result = new ObjTaskList();
            try
            {
                result.list = new List<ObjTask>();

                var tasks = new List<Task>();

                if (rootType == RootTypes.User)
                {
                    var TasksIds = _participantRepository.GetList(rootID)
                        .Select(x => x.RootId)
                        .ToList();

                    foreach (var taskId in TasksIds)
                    {
                        tasks.Add(_taskRepository.GetFull(taskId));
                    }
                }
                else
                    tasks = _taskRepository.GetList(rootID, rootType);

                result.list = tasks.Select(x => new ObjTask
                {
                    Id = x.Id,
                    RootId = x.RootId,
                    RootType = x.RootType,
                    Title = x.Title,
                    Description = x.Description,

                    StatusColor = x.Status.Color,
                    StatusId = x.Status.Id,
                    StatusName = x.Status.Name,

                    CompeteProcent = x.CompeteProcent,
                    DeadLine = x.DeadLine,
                    TaskTypeName = x.TaskType.Name,
                    TaskTypeId = x.TaskTypeId,
                    UserId = x.UserId,
                    Participants = _participantRepository.AllFull()
                    .Where(particapant => particapant.RootType == RootTypes.Project && particapant.RootId == x.Id)
                    .Select(participant => new ObjParticipant()
                    {
                        Id = participant.Id,
                        FIO = participant.User.Fio,
                        RootType = participant.RootType,
                        Task = participant.Task,
                        WorkSum = participant.WorkSum,
                        Currency = participant.Currency,
                        WorkPeriod = participant.WorkPeriod,
                        Residue = participant.Residue,
                        CreatedId = participant.CreatedId,
                        UserId = participant.UserId,
                    })
                        .ToList()
                }).ToList();
            }
            catch (Exception ex)
            {
                _Logger.LogError("Getting Task list error rootId = {0} / RootType = {1} : {2}", rootID, rootType, ex);
            }


            return result;
        }

        public ObjTask Get(int id)
        {
            return Map(_taskRepository.GetFull(id));
        }

        public ServiceResult SaveTask(ObjTask obj)
        {
            try
            {
                var task = Map(obj);
                _taskRepository.Insert(task);
                _taskRepository.SaveChanges();


                //if (obj.RootType == RootTypes.Client)
                //{
                //    var client = _clientRepository.Get(obj.RootId);
                //    client.Credit += obj.Cost * CurrencyConverter.ConvertValute(obj.Currency, CurrencyType.Rub);
                //    _clientRepository.Update(client);
                //    _clientRepository.SaveChanges();
                //}
                
                return ServiceResult.SuccessResult(task.Id);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.ToString());
                return ServiceResult.ErrorResult("Ошибка сохранения задачи");
            }

        }

        public ServiceResult EditTask(ObjTask obj)
        {
            try
            {
                var task = _taskRepository.Get(obj.Id);
                UpdateMap(task, obj);
                _taskRepository.Update(task);
                _taskRepository.SaveChanges();
                //if (obj.RootType == RootTypes.Client)
                //{
                //    var client = _clientRepository.Get(obj.RootId);
                //    client.Credit += (obj.Cost * CurrencyConverter.ConvertValute(obj.Currency, CurrencyType.Rub)) -
                //        (task.Cost * CurrencyConverter.ConvertValute(task.Currency, CurrencyType.Rub));
                //    _clientRepository.Update(client);
                //    _clientRepository.SaveChanges();
                //}
                return ServiceResult.SuccessResult(obj.Id);
            }
            catch (Exception ex)
            {
                _Logger.LogError("Edit Task error TaskId = {0} : {1}", obj.Id, ex);
                return ServiceResult.ErrorResult("Ошибка при сохранении задачи");
            }
        }

        public ServiceResult Delete(int id)
        {
            try
            {
                var task = _taskRepository.Get(id);

                //if (task.RootType == RootTypes.Client)
                //{
                //    var client = _clientRepository.Get(task.RootId);
                //    client.Credit -= task.Residue * CurrencyConverter.ConvertValute(task.Currency, CurrencyType.Rub);
                //}
                _taskRepository.Delete(id);
                _taskRepository.SaveChanges();

                return ServiceResult.SuccessResult();
            }
            catch (Exception ex)
            {
                _Logger.LogError("Delete Task error TaskId = {0} : {1}", id, ex);
                return ServiceResult.ErrorResult("Ошибка при удалении задачи");
            }
        }

        #region TaskType

        public ObjTaskType GetTaskType(int id)
        {
            var taskType = _taskTypeRepository.Get(id);
            return Map(taskType);

        }

        public void Add(ObjTaskType obj)
        {
            var taskType = Map(obj);
            _taskTypeRepository.Insert(taskType);
            _taskTypeRepository.SaveChanges();
        }

        public void DeleteTaskType(int id)
        {
            _taskTypeRepository.Delete(id);
            _taskTypeRepository.SaveChanges();
        }

        public void Edit(ObjTaskType obj) { 
            var taskType = _taskTypeRepository.Get(obj.Id);
            taskType.Name = obj.Name;
            taskType.Description = obj.Description;
            taskType.Posititon = obj.Position;

            _taskTypeRepository.Update(taskType);
            _taskTypeRepository.SaveChanges();
        }

        public List<ObjTaskType> GetTaskTypes()
        {
            var taskTypes = _taskTypeRepository.All();

            return taskTypes
                .Select(x => Map(x))
                .OrderBy(x => x.Position)
                .ToList();
        }


        #endregion



        #region Map
        private ObjTask Map(Task task)
        {
            return new ObjTask
            {
                Id = task.Id,
                RootId = task.RootId,
                RootType = task.RootType,
                Title = task.Title,
                Description = task.Description,

                StatusColor = task.Status.Color,
                StatusId = task.Status.Id,
                StatusName = task.Status.Name,

                CompeteProcent = task.CompeteProcent,
                DeadLine = task.DeadLine,
                TaskTypeName = task.TaskType.Name,
                TaskTypeId = task.TaskTypeId,
                UserId = task.UserId,
                RootName = _rootTypesService.GetRootName(task.RootType, task.RootId),
                RootUrl = _rootTypesService.GetRootUrl(task.RootType, task.RootId)

            };
        }
        private Task Map(ObjTask obj)
        {
            return new Task
            {
                Id = obj.Id,
                RootId = obj.RootId,
                RootType = obj.RootType,
                Title = obj.Title,
                Description = obj.Description,

                StatusId = obj.StatusId,

                CompeteProcent = obj.CompeteProcent,
                DeadLine = obj.DeadLine,
                TaskTypeId = obj.TaskTypeId,
                UserId = obj.UserId,
            };
        }

        private void UpdateMap(Task task, ObjTask obj)
        {
          task.Title = obj.Title;
            task.Description = obj.Description;
            task.StatusId = obj.StatusId;
            task.CompeteProcent = obj.CompeteProcent;
            task.DeadLine = obj.DeadLine;
            task.TaskTypeId = obj.TaskTypeId;
        }

        private ObjTaskType Map(TaskType model)
        {
            return new ObjTaskType()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Position = model.Posititon,
            };
        }

        private TaskType Map(ObjTaskType obj)
        {
            return new TaskType()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                Posititon = obj.Position,
            };
        }

        public TasksModel GetViewModel(TasksModel model)
        {
            var result = GetObjTaskSc();

            if (model.selectedStatuses == null || !model.selectedStatuses.Any())
            {
                //defaul filters
                model.selectedStatuses = _statusRepository.Get(RootTypes.Project)
                    .Where(x => !x.IsHide)
                    .Select(x => x.Id)
                    .ToList();
            }

            result = result.Where(x => model.selectedStatuses.Contains(x.StatusId)).ToList();

            if (model.selectedManagers != null && model.selectedManagers.Any())
                result = result.Where(x => model.selectedManagers.Contains(x.UserId ?? -1)
                || x.Participants.Any(v => model.selectedManagers.Contains(v.UserId))).ToList();

            if (!string.IsNullOrEmpty(model.q))
                result = result.Where(x => x.Title.ToLower().Contains(model.q.ToLower())).ToList();


            model.Tasks = result
                //.Skip((model.pageViewModel.PageNumber-1)*PotentialClientsModel.ItemsPerPage)
                //.Take(PotentialClientsModel.ItemsPerPage)
                .ToList()
                .OrderByDescending(x => x.Created)
                .ToList();


            model.PStatuses = _statusRepository.Get(RootTypes.Project);
            model.userList = _userRepository.GetFullUsers().ToList();


            return model;
        }




        #endregion
    }
}
