using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;

using CRMCore.DB;
using CRMCore.Enums;
using CRMCore.Objects;
using CRMCore.Repositories;
using CRMCore.Helpers;
using CRMCore.Extensions;

namespace CRMCore.Services.Impl
{
    public class FinanceService : IFinanceService
    {
        IFinanceRepository _financeRepository { get; set; }
        IFinanceSubTypeRepository _financeSubTypeRepository { get; set; }
        IUserRepository _userRepository { get; set; }
        IProjectRepository _projectRepository { get; set; }
        IClientRepository _clientRepository { get; set; }
        IParticipantRepository _participantRepository { get; set; }

        ILogger<FinanceService> _logger;

        public FinanceService(IFinanceRepository financeRepository,
            IFinanceSubTypeRepository financeSubTypeRepository,
            IUserRepository userRepository,
            IProjectRepository projectRepository,
            IClientRepository clientRepository,
            IParticipantRepository participantRepository,
            ILogger<FinanceService> logger)
        {
            _financeRepository = financeRepository;
            _financeSubTypeRepository = financeSubTypeRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _clientRepository = clientRepository;
            _participantRepository = participantRepository;
            _logger = logger;
        }

        public ServiceResult<List<ObjFinance>> GetList(int rootId, RootTypes rootType)
        {
            try
            {
                var finances = new List<Finance>();

                switch (rootType)
                {
                    case RootTypes.User:
                        finances = _financeRepository.GetListByUserId(rootId);
                        break;
                    case RootTypes.Client:
                        finances = _financeRepository.GetListByClientId(rootId);
                        break;
                    case RootTypes.Project:
                        finances = _financeRepository.GetListByProjectId(rootId);
                        break;
                    case RootTypes.Finance:
                        finances = _financeRepository.GetList();
                        break;
                }
                var result = finances.Select(x => Map(x)).ToList();

                return ServiceResult<List<ObjFinance>>.SuccessResult(result);
            } catch(Exception ex)
            {
                return ServiceResult<List<ObjFinance>>.ErrorResult("Ошибка при загрузке данных");
            }
            
            
        }

        public ServiceResult Add(ObjFinance objFinance)
        {
            try
            {
                var finance = Map(objFinance);
                _financeRepository.Insert(finance);
                _financeRepository.SaveChanges();
                _logger.LogDebug("Save Finance");

                if (finance.FinanceType == FinanceTypes.Expence)
                {
                    //проверяем на принадлежность к пользователю
                    if (finance.UserId != null)
                    {
                        var participant = _participantRepository.Get(finance.UserId.Value, finance.ProjectId, RootTypes.Project);
                        if (participant == null)
                            return ServiceResult.ErrorResult("Добавьте участника");

                        participant.Residue -= finance.Cost * CurrencyConverter.ConvertValute(finance.Currency, participant.Currency);
                       
                        _participantRepository.Update(participant);
                        _participantRepository.SaveChanges();
                        _logger.LogDebug("Save participant");
                    }
                }
                //для поступления только один вариант
                if (finance.FinanceType == FinanceTypes.Receipt)
                {
                    var project = _projectRepository.Get(finance.ProjectId);
                    if (project == null)
                        return ServiceResult.ErrorResult("Проект не найден");
                    int ClientId = project.RootId;
                    var client = _clientRepository.Get(ClientId);
                    if (client == null)
                        return ServiceResult.ErrorResult("Клиент не найден");

                    project.Residue -= finance.Cost * CurrencyConverter.ConvertValute(finance.Currency, project.Currency);
                    _projectRepository.Update(project);
                    _projectRepository.SaveChanges();
                    _logger.LogDebug("Save project");

                    client.Credit -= finance.Cost * CurrencyConverter.ConvertValute(finance.Currency, CurrencyType.Rub);
                    _clientRepository.Update(client);
                    _clientRepository.SaveChanges();
                    _logger.LogDebug("Save Client");
                }
                return ServiceResult.SuccessResult();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ServiceResult.ErrorResult("Ошибка сохранения");
            }
            
            
        }

        public ServiceResult Delete(int id)
        {
            try
            {
                var finance = _financeRepository.Get(id);
                if(finance.FinanceType == FinanceTypes.Expence)
                {
                    if(finance.UserId != null)
                    {
                        var paricipant = _participantRepository.Get(finance.UserId.Value, finance.ProjectId, RootTypes.Project);
                        if (paricipant == null)
                            return ServiceResult.ErrorResult("Не найден сотрудник по расходу");
                        paricipant.Residue += finance.Cost * CurrencyConverter.ConvertValute(finance.Currency, paricipant.Currency);

                        _participantRepository.Update(paricipant);
                        _participantRepository.SaveChanges();
                        _logger.LogInformation("participant Save");
                    }
                }
                if(finance.FinanceType == FinanceTypes.Receipt)
                {
                    var project = _projectRepository.Get(finance.ProjectId);
                    if (project == null)
                        return ServiceResult.ErrorResult("Проект не найден");
                    var client = _clientRepository.Get(project.RootId);
                    if (client == null)
                        return ServiceResult.ErrorResult("Клиент не найден");

                    project.Residue += finance.Cost * CurrencyConverter.ConvertValute(finance.Currency, project.Currency);
                    _projectRepository.Update(project);
                    _projectRepository.SaveChanges();
                    _logger.LogInformation("project Save");

                    client.Credit += finance.Cost * CurrencyConverter.ConvertValute(finance.Currency, CurrencyType.Rub);
                    _clientRepository.Update(client);
                    _clientRepository.SaveChanges();
                }
                _financeRepository.Delete(id);
                _financeRepository.SaveChanges();

                return ServiceResult.SuccessResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ServiceResult.ErrorResult("Ошибка удаления");
            }
        }

        public List<ObjFinance> All()
        {
            var finances = _financeRepository.AllFull();

            return finances.Select(x => Map(x))
                .ToList();

        }

        #region SubTypes

        public List<ObjFinanceSubType> GetFSubTypes()
        {
            var subtypes = _financeSubTypeRepository.All();

            return subtypes
                .Select(x => Map(x))
                .OrderBy(x=>x.Position)
                .ToList();

        }

        public void Add(ObjFinanceSubType obj)
        {
            var subType = Map(obj);
            _financeSubTypeRepository.Insert(subType);
            _financeSubTypeRepository.SaveChanges();
        }

        public ObjFinanceSubType GetSubType(int id)
        {
            var subType = _financeSubTypeRepository.Get(id);
            return Map(subType);
        }

        public void Edit(ObjFinanceSubType obj)
        {
            var subtype = _financeSubTypeRepository.Get(obj.Id);

            subtype.Name = obj.Name;
            subtype.Description = obj.Description;
            subtype.Position = obj.Position;

            _financeSubTypeRepository.Update(subtype);
            _financeSubTypeRepository.SaveChanges();
        }

        public void DeleteSubType(int id)
        {
            _financeSubTypeRepository.Delete(id);
            _financeSubTypeRepository.SaveChanges();
        }

        #endregion

        

        #region Map

        public ObjFinance Map(Finance model)
        {
            return new ObjFinance()
            {
                Id = model.Id,
                FinanceType = model.FinanceType,

                SubTypeId = model.SubTypeId,
                SubTypeName = model.SubTypeId !=null ? model.SubType.Name : "",

                ProjectId = model.ProjectId,
                ProjectName = model.Project.Title,

                Date = model.Date,

                Place = model.Place,

                Cost = model.Cost,
                Currency = model.Currency,

                UserId = model.UserId,
                UserName = model.UserId != null ? model.User.Fio : "",

                DocumentName = model.DocumentName,

                CreatedId = model.CreatedId,
            };
        }

        public Finance Map(ObjFinance obj)
        {
            return new Finance()
            {
                Id = obj.Id,
                FinanceType = obj.FinanceType,

                SubTypeId = obj.SubTypeId,
                ProjectId = obj.ProjectId,
                Date = obj.Date,
                Place = obj.Place,

                Cost = obj.Cost,
                Currency = obj.Currency,

                UserId = obj.UserId,

                DocumentName = obj.DocumentName
            };
        }

        public ObjFinanceSubType Map(FinanceSubType model)
        {
            return new ObjFinanceSubType()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Position = model.Position,
            };
        }

        public FinanceSubType Map( ObjFinanceSubType obj)
        {
            return new FinanceSubType()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                Position = obj.Position,
            };
        }

       


        #endregion
    }
}
