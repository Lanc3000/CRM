using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using CRMCore.Enums;
using CRMCore.Repositories;
using CRMCore.Services;
using CRMCore.Helpers;
using CRMCore.Extensions;

namespace CRMCore.Services.Impl
{
    public class DeleteEntitryService : IDeleteEntityService
    {
        private IUserRepository _userRepository { get;}
        private IPotentialClientRepository _potentialClientRepository { get; }
        IProjectRepository _projectRepository { get; }
        IClientRepository _clientRepository { get; }
        IParticipantRepository _participantRepository { get; set; }
        ICommentRepository _commentRepository { get; set; }
        IFileDataRepository _fileDataRepository { get; set; }
        IFinanceRepository _financeRepository { get; set; }

        public DeleteEntitryService(IUserRepository userRepository,
            IPotentialClientRepository potentialClientRepository,
            IProjectRepository projectRepository,
            IClientRepository clientRepository,
            IParticipantRepository participantRepository,
            ICommentRepository commentRepository,
            IFileDataRepository fileDataRepository)
        {
            _userRepository = userRepository;
            _potentialClientRepository = potentialClientRepository;
            _projectRepository = projectRepository;
            _clientRepository = clientRepository;
            _participantRepository = participantRepository;
            _commentRepository = commentRepository;
            _fileDataRepository = fileDataRepository;
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Id"></param>
       /// <param name="rootType"></param>
       /// <returns></returns>
        public ServiceResult<string> Delete(int Id, RootTypes rootType)
        {
            string ControllerName = "Home";

            switch (rootType)
            {
                case RootTypes.User:
                    try
                    {
                        //Удаляем пользователя из учасников проекта
                        var participants = _participantRepository.All().ToList();
                        for(int i =0; i<participants.Count; i++)
                        {
                            if (participants[i].UserId == Id)
                                _participantRepository.Delete(participants[i]);
                        }
                        _participantRepository.SaveChanges();
                        
                        _userRepository.Delete(Id);
                        _userRepository.SaveChanges();
                        ControllerName = "Users";
                    }
                    catch(Exception ex)
                    {
                        return ServiceResult<string>.ErrorResult("Ошибка при удалении сотрудника");
                    }
                    break;
                    
                case RootTypes.PotentialCLient:
                    try
                    {
                        _potentialClientRepository.Delete(Id);
                        _potentialClientRepository.SaveChanges();
                        ControllerName = "PotentialClients";
                    }
                    catch
                    {
                        return ServiceResult<string>.ErrorResult("Ошибка при удалении ");
                    }
                    break;
                case RootTypes.Client:
                    try
                    {
                        var projects = _projectRepository.All().ToList();
                        for (int i = 0; i < projects.Count; i++)
                        {
                            if (projects[i].RootType == RootTypes.Client && projects[i].RootId == Id)
                            {
                                _projectRepository.Delete(projects[i]);
                            }
                        }
                        _projectRepository.SaveChanges();
                        _clientRepository.Delete(Id);
                        _clientRepository.SaveChanges();
                        ControllerName = "Clients";
                    }
                    catch (Exception ex)
                    {
                        return ServiceResult<string>.ErrorResult("Ошибка при удалении клиента");
                    }
                    break;
                case RootTypes.Project:
                    try
                    {
                        var project = _projectRepository.Get(Id);

                        if (project.RootType == RootTypes.Client)
                        {
                            var client = _clientRepository.Get(project.RootId);
                            client.Credit -= project.Residue * CurrencyConverter.ConvertValute(project.Currency, CurrencyType.Rub);
                        }
                        _projectRepository.Delete(Id);
                        _projectRepository.SaveChanges();
                        ControllerName = "Projects";
                    }
                    catch (Exception ex)
                    {
                        return ServiceResult<string>.ErrorResult("Ошбика при удалении проекта");
                    }
                    break; 
            }

            //Зачищаем модули


            return ServiceResult<string>.SuccessResult(ControllerName);
        }
    }
}
