using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using CRMCore.Repositories;
using CRMCore.DB;
using CRMCore.Objects;
using CRMCore.Enums;
using CRMCore.Extensions;
using Microsoft.Extensions.Logging;

namespace CRMCore.Services.Impl
{
    public class PotentialClientService : IPotentialClientService
    {
        private IPotentialClientRepository _potentialClientRepository { get; }
        readonly IStatusRepository _statusRepository;
        readonly IUserRepository _userRepository;
        ILogger<PotentialClientService> _logger;

        public PotentialClientService(IPotentialClientRepository potentialClientRepository,
            IStatusRepository statusRepository,
            IUserRepository userRepository,
            ILogger<PotentialClientService> logger)
        {
            _potentialClientRepository = potentialClientRepository;
            _statusRepository = statusRepository;
            _userRepository = userRepository;
            _logger = logger;

        }

        public IEnumerable<ObjPotentialClient> All()
        {
            return _potentialClientRepository.AllFull()
                .OrderByDescending(x => x.Created)
                .Select(x => Map(x));

        }

        public ObjPotentialClient GetById(int Id)
        {
            return Map(_potentialClientRepository.GetFull(Id));
        }


        public ServiceResult SavePCLient(ObjPotentialClient obj)
        {

            try
            {
                var result = Map(obj);
                _potentialClientRepository.Insert(result);
                _potentialClientRepository.SaveChanges();
                _logger.LogInformation("Save potential Client");
                return ServiceResult.SuccessResult(result.Id)
;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ServiceResult.ErrorResult("Ошибка при создании нового клиента");
            }

        }

        public ServiceResult EditPClient(ObjPotentialClient obj)
        {
            try
            {
                var pClient = _potentialClientRepository.Get(obj.Id);
                MapWithoutId(pClient, obj);
                _potentialClientRepository.Update(pClient);
                _potentialClientRepository.SaveChanges();
                return ServiceResult.SuccessResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ServiceResult.ErrorResult("Ошибка при обновлении данных о клиенте");
            }


        }


        [Obsolete("Неполный юзер")]
        private ObjPotentialClient Map(PotentialClient pClient)
        {
            return new ObjPotentialClient
            {
                Id = pClient.Id,
                Name = pClient.Name,
                CompanyName = pClient.CompanyName,
                ProjectTypeId = pClient.ProjectTypeId,

                StatusColor = pClient.Status.Color,
                StatusId = pClient.StatusId,
                StatusName = pClient.Status.Name,

                Description = pClient.Description,
                Cost = pClient.Cost,
                Currency = pClient.Currency,
                DateCount = pClient.DateCount,
                DateType = pClient.DateType,
                UserId = pClient.UserId,
                Created = pClient.Created,
                Manager = pClient.UserId == null ? null :
                new ObjUser()
                {
                    Fio = pClient.Manager.Fio,
                    Email = pClient.Manager.Email,
                },
                Source = pClient.SourceId == null ? null :
                new ObjSource()
                {
                    Id = pClient.SourceId.Value,
                    Name = pClient.Source.Name
                },
                Probability = pClient.Probability
            };
        }

        private PotentialClient Map(ObjPotentialClient obj)
        {
            return new PotentialClient
            {
                Id = obj.Id,
                Name = obj.Name,
                CompanyName = obj.CompanyName,
                StatusId = obj.StatusId,
                Description = obj.Description,
                Cost = obj.Cost,
                Currency = obj.Currency,
                DateCount = obj.DateCount,
                DateType = obj.DateType,
                UserId = obj.UserId,
                ProjectTypeId = obj.ProjectTypeId,
                Probability = obj.Probability,
                SourceId = obj.SourceId,
            };
        }

        private void MapWithoutId(PotentialClient pCLient, ObjPotentialClient obj)
        {
            pCLient.Name = obj.Name;
            pCLient.CompanyName = obj.CompanyName;
            pCLient.StatusId = obj.StatusId;
            pCLient.Description = obj.Description;
            pCLient.Cost = obj.Cost;
            pCLient.Currency = obj.Currency;
            pCLient.DateCount = obj.DateCount;
            pCLient.DateType = obj.DateType;
            pCLient.UserId = obj.UserId;
            pCLient.Probability = obj.Probability;
            pCLient.ProjectTypeId = obj.ProjectTypeId;
            pCLient.SourceId = obj.SourceId;
        }

        public List<ObjPotentialClient> Search(string searchString)
        {
            var result = _potentialClientRepository.Search(searchString);

            return result.Select(x => Map(x)).ToList();
        }

        public List<ObjPotentialClient> FilterData(List<int> statuses, string q, List<int> users)
        {
            var result = _potentialClientRepository.AllFull()
                .OrderByDescending(x => x.Created)
                .ToList();
            if(statuses != null && statuses.Any())
                result = result.Where(x => statuses.Contains(x.StatusId)).ToList();
            if(q !=null)
                result = result.Where(x => x.Name.ToLower().Contains(q.ToLower())).ToList();
            if (users != null && users.Any())
                result = result.Where(x => users.Contains(x.UserId??-1)).ToList();

            return result.Select(x => Map(x)).ToList();
        }

        public PotentialClientsModel GetViewModel(PotentialClientsModel model)
        {
            var result = _potentialClientRepository.GetIncludeFull();
            
            
            if(model.selectedStatuses != null)
            {
                if(model.selectedManagers != null && model.selectedManagers.Any())
                    result = result.Where(x => model.selectedManagers.Contains(x.UserId ?? -1));

                if (model.selectedStatuses.Any())
                    result = result.Where(x => model.selectedStatuses.Contains(x.StatusId));

                if (!string.IsNullOrEmpty(model.q))
                    result = result.Where(x => x.Name.ToLower().Contains(model.q.ToLower()));
            }
            else
            {//defaul filters
                model.selectedStatuses = _statusRepository.Get(RootTypes.PotentialCLient)
                    .Where(x=>!x.IsHide)
                    .Select(x=>x.Id)
                    .ToList();

                result = result.Where(x => model.selectedStatuses.Contains(x.StatusId));

               // model.pageViewModel = new PageViewModel(result.Count(), 1, PotentialClientsModel.ItemsPerPage);
            }

            model.Pclients = result
                //.Skip((model.pageViewModel.PageNumber-1)*PotentialClientsModel.ItemsPerPage)
                //.Take(PotentialClientsModel.ItemsPerPage)
                .ToList()
                .Select(x => Map(x))
                .OrderByDescending(x=>x.Created)
                .ToList();


            model.PStatuses = _statusRepository.Get(RootTypes.PotentialCLient);
            model.userList = _userRepository.GetFullUsers().Where(x => x.IsManager)
                .ToList();

            return model;

        }
    }
}
