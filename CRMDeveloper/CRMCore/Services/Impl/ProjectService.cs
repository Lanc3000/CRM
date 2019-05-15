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
    public class ProjectService : IProjectService
    {
        ILogger <ProjectService> _Logger { get; }

        IProjectRepository _projectRepository { get; }
        IParticipantRepository _participantRepository { get; }
        IProjectTypeRepository _projectTypeRepository { get; }
        IClientRepository _clientRepository { get; }
        IStatusRepository _statusRepository { get; }
        IUserRepository _userRepository { get; }
        IRootTypesService _rootTypesService { get; }

        public ProjectService(IProjectRepository projectRepository,
            IParticipantRepository participantRepository,
            IProjectTypeRepository projectTypeRepository,
            IClientRepository clientRepository,
            IStatusRepository statusRepository,
            IUserRepository userRepository,
             IRootTypesService rootTypesService,
            ILogger<ProjectService> logger)
        {
            _Logger = logger;

            _projectRepository = projectRepository;
            _participantRepository = participantRepository;
            _projectTypeRepository = projectTypeRepository;
            _clientRepository = clientRepository;
            _statusRepository = statusRepository;
            _userRepository = userRepository;
            _rootTypesService = rootTypesService;
        }

        public List<ObjProject> GetProjectList()
        {
            var projects = _projectRepository.AllFull();

            return projects
                .Select(x => Map(x))
                .ToList();
                
        }

        public List<ObjProjectP> GetObjProjectPs()
        {
            var projects = _projectRepository.AllFull();

                var result =  projects.Select(x => new ObjProjectP
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
                    Cost = x.Cost,
                    Currency = x.Currency,
                    DeadLine = x.DeadLine,
                    ProjectTypeName = x.ProjectType.Name,
                    ProjectTypeId = x.ProjectTypeId,
                    UserId = x.UserId,
                    UserFio = x.User?.Fio,
                    Residue = x.Residue,
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

        public ObjProjectList GetProjectList(int rootID, RootTypes rootType)
        {
            ObjProjectList result = new ObjProjectList();
            try
            {
                result.list = new List<ObjProject>();

                var projects = new List<Project>();

                //список проектов для сотрудника
                if (rootType == RootTypes.User)
                {
                    var ProjectsIds = _participantRepository.GetList(rootID)
                        .Select(x => x.RootId)
                        .ToList();

                    foreach (var projectId in ProjectsIds)
                    {
                        projects.Add(_projectRepository.GetFull(projectId));
                    }
                }
                else
                    projects = _projectRepository.GetList(rootID, rootType);

                result.list = projects.Select(x => new ObjProject
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
                    Cost = x.Cost,
                    Currency = x.Currency,
                    DeadLine = x.DeadLine,
                    ProjectTypeName = x.ProjectType.Name,
                    ProjectTypeId = x.ProjectTypeId,
                    UserId = x.UserId,
                    Residue = x.Residue,
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
            catch(Exception ex)
            {
                _Logger.LogError("Getting Project list error rootId = {0} / RootType = {1} : {2}", rootID, rootType , ex);
            }
            

            return result;
        }

        public ObjProject Get(int id)
        {
            return Map(_projectRepository.GetFull(id));
        }

        public ServiceResult SaveProject(ObjProject obj)
        {
            try
            {
                var project = Map(obj);
                project.Residue = project.Cost;
                _projectRepository.Insert(project);
                _projectRepository.SaveChanges();

                //Добавить проверку на статус
                if (obj.RootType == RootTypes.Client)
                {
                    var client = _clientRepository.Get(obj.RootId);
                    client.Credit += obj.Cost * CurrencyConverter.ConvertValute(obj.Currency, CurrencyType.Rub);
                    _clientRepository.Update(client);
                    _clientRepository.SaveChanges();
                }

                return ServiceResult.SuccessResult(project.Id);
            }
            catch(Exception ex)
            {
                _Logger.LogError(ex.ToString());
                return ServiceResult.ErrorResult("Ошибка сохранения проекта");
            }
            
        }

        public ServiceResult EditProject(ObjProject obj)
        {
            try
            {
                var project = _projectRepository.Get(obj.Id);
                UpdateMap(project, obj);
                _projectRepository.Update(project);
                _projectRepository.SaveChanges();
                if (obj.RootType == RootTypes.Client)
                {
                    var client = _clientRepository.Get(obj.RootId);
                    client.Credit += (obj.Cost * CurrencyConverter.ConvertValute(obj.Currency, CurrencyType.Rub)) -
                        (project.Cost * CurrencyConverter.ConvertValute(project.Currency, CurrencyType.Rub));
                    _clientRepository.Update(client);
                    _clientRepository.SaveChanges();
                }
                return ServiceResult.SuccessResult(obj.Id);
            }
            catch(Exception ex)
            {
                _Logger.LogError("Edit Project error ProjectId = {0} : {1}", obj.Id, ex);
                return ServiceResult.ErrorResult("Ошибка при сохранении проекта");
            }
        }

        public ServiceResult Delete(int id)
        {
            try
            {
                var project = _projectRepository.Get(id);

                if(project.RootType == RootTypes.Client)
                {
                    var client = _clientRepository.Get(project.RootId);
                    client.Credit -= project.Residue * CurrencyConverter.ConvertValute(project.Currency, CurrencyType.Rub);
                }
                _projectRepository.Delete(id);
                _projectRepository.SaveChanges();

                return ServiceResult.SuccessResult();
            }
            catch (Exception ex)
            {
                _Logger.LogError("Delete project error ProjectId = {0} : {1}", id, ex);
                return ServiceResult.ErrorResult("Ошибка при удалении проекта");
            }
        }

        #region ProjectType

        public ObjProjectType GetProjectType(int id)
        {
            var projectType = _projectTypeRepository.Get(id);
            return Map(projectType);
               
        }

        public void Add(ObjProjectType obj)
        {
            var projectType = Map(obj);
            _projectTypeRepository.Insert(projectType);
            _projectTypeRepository.SaveChanges();
        }

        public void DeleteProjectType(int id)
        {
            _projectTypeRepository.Delete(id);
            _projectTypeRepository.SaveChanges();
        }

        public void Edit(ObjProjectType obj)
        {
            var projecttType = _projectTypeRepository.Get(obj.Id);
            projecttType.Name = obj.Name;
            projecttType.Description = obj.Description;
            projecttType.Posititon = obj.Position;

            _projectTypeRepository.Update(projecttType);
            _projectTypeRepository.SaveChanges();
        }

        public List<ObjProjectType> GetProjectTypes()
        {
            var projectTypes = _projectTypeRepository.All();

            return projectTypes
                .Select(x => Map(x))
                .OrderBy(x => x.Position)
                .ToList();
        }


        #endregion



        #region Map
        private ObjProject Map(Project project)
        {
            return new ObjProject
            {
                Id = project.Id,
                RootId = project.RootId,
                RootType = project.RootType,
                Title = project.Title,
                Description = project.Description,

                StatusColor = project.Status.Color,
                StatusId = project.Status.Id,
                StatusName = project.Status.Name,

                CompeteProcent = project.CompeteProcent,
                Cost = project.Cost,
                Currency = project.Currency,
                DeadLine = project.DeadLine,
                ProjectTypeName = project.ProjectType.Name,
                ProjectTypeId = project.ProjectTypeId,
                UserId = project.UserId,
                Residue = project.Residue,
                RootName = _rootTypesService.GetRootName(project.RootType, project.RootId),
                RootUrl = _rootTypesService.GetRootUrl(project.RootType, project.RootId)

        };
        }
        private Project Map(ObjProject obj)
        {
            return new Project
            {
                Id = obj.Id,
                RootId = obj.RootId,
                RootType = obj.RootType,
                Title = obj.Title,
                Description = obj.Description,
                
                StatusId = obj.StatusId,

                CompeteProcent = obj.CompeteProcent,
                Cost = obj.Cost,
                Currency = obj.Currency,
                DeadLine = obj.DeadLine,
                ProjectTypeId = obj.ProjectTypeId,
                UserId = obj.UserId,
                Residue = obj.Residue,
            };
        }

        private void UpdateMap(Project project, ObjProject obj)
        {
            project.Title = obj.Title;
            project.Description = obj.Description;
            project.StatusId = obj.StatusId;
            project.CompeteProcent = obj.CompeteProcent;
            project.Cost = obj.Cost;
            project.Currency = obj.Currency;
            project.DeadLine = obj.DeadLine;
            project.ProjectTypeId = obj.ProjectTypeId;
            project.Residue = obj.Residue;
        }

        private ObjProjectType Map(ProjectType model)
        {
            return new ObjProjectType()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Position = model.Posititon,
            };
        }

        private ProjectType Map(ObjProjectType obj)
        {
            return new ProjectType()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                Posititon = obj.Position,
            };
        }

        public ProjectsModel GetViewModel(ProjectsModel model)
        {
            var result = GetObjProjectPs();

            if (model.selectedStatuses==null||!model.selectedStatuses.Any())
            {
                //defaul filters
                model.selectedStatuses = _statusRepository.Get(RootTypes.Project)
                    .Where(x => !x.IsHide)
                    .Select(x => x.Id)
                    .ToList();
            }

            result = result.Where(x => model.selectedStatuses.Contains(x.StatusId)).ToList();

            if (model.selectedManagers != null && model.selectedManagers.Any())
                result = result.Where(x => model.selectedManagers.Contains(x.UserId?? -1) 
                || x.Participants.Any(v=> model.selectedManagers.Contains(v.UserId))).ToList();

            if (!string.IsNullOrEmpty(model.q))
                result = result.Where(x => x.Title.ToLower().Contains(model.q.ToLower())).ToList();


            model.Projects = result
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
