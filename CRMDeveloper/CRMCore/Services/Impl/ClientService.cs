using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;

using CRMCore.Services;
using CRMCore.Repositories;
using CRMCore.DB;
using CRMCore.Objects;
using CRMCore.Enums;
using CRMCore.Helpers;
using CRMCore.Extensions;

namespace CRMCore.Services.Impl
{
    public class ClientService : IClientService
    {
        IClientRepository _clientRepository { get; }
        IPotentialClientRepository _potentialClientRepository { get; }
        ICommentRepository _commentRepository { get; }
        IContactRepository _contactRepository { get; }
        IFileDataRepository _fileDataRepository { get; }
        IProjectRepository _projectRepository { get; }
        IStatusRepository _statusRepository { get; }

        ILogger<ClientService> _logger;

        public ClientService(IClientRepository clientRepository, IPotentialClientRepository potentialClientRepository,
            ICommentRepository commentRepository, IContactRepository contactRepository,
            IFileDataRepository fileDataRepository, IProjectRepository projectRepository,
            IStatusRepository statusRepository,
            ILogger<ClientService> logger)
        {
            _clientRepository = clientRepository;
            _potentialClientRepository = potentialClientRepository;
            _commentRepository = commentRepository;
            _contactRepository = contactRepository;
            _fileDataRepository = fileDataRepository;
            _projectRepository = projectRepository;
            _statusRepository = statusRepository;

            _logger = logger;
        }

        public List<ObjClient> GetListClientP()
        {
            var clients =  _clientRepository.All();
            var result = new List<ObjClient>();
            foreach(var client in clients)
            {
                result.Add(Map(client));
            }
            return result;

        }

        public List<ObjClientP> GetListClientWithProjects()
        {
            var clients = _clientRepository.All();

            return clients.Select(x => new ObjClientP()
            {
                Id = x.Id,
                CompanyName = x.CompanyName,
                Description = x.Description,
                BIK = x.BIK,
                CorrAccount = x.CorrAccount,
                PaymentAccount = x.PaymentAccount,
                BankName = x.BankName,
                Credit = x.Credit,
                ListProjects = _projectRepository.All()
                .Where(p => p.RootType == RootTypes.Client && p.RootId == x.Id)
                .Select(p => new ObjProject()
                {
                    Id = p.Id,
                    Title = p.Title,
                    DeadLine = p.DeadLine,
                    Cost = p.Cost,
                    Currency = p.Currency,
                    Residue = p.Residue,
                })
                .ToList()
            })
            .OrderBy(x=>x.CompanyName)
            .ToList();
        }

        public ServiceResult ConvertToClient(int pClientId)
        {
            var pClient = _potentialClientRepository.Get(pClientId);

            var newStatus = _statusRepository.Get("Заключен договор");

            if (pClient == null)
                return ServiceResult.ErrorResult("Потенциальный клиент не найден");
            try
            {
                var Client = Convert(pClient);
                _clientRepository.Insert(Client);
                _clientRepository.SaveChanges();

                ConvertToProject(Client.Id, pClient);
                TransferRelatedObjects(Client.Id, pClientId, RootTypes.PotentialCLient);
                pClient.StatusId = newStatus.Id;
                return ServiceResult.SuccessResult(Client.Id);
            }catch(Exception ex)
            {
                //_logger.LogError("Convert Client error {0}", ex);
                return ServiceResult.ErrorResult("Ошибка при переносе данных потенциального клиента");
            }
        }

       public ObjClient Get(int id)
        {
            var Client = _clientRepository.Get(id);
            return Map(Client);
        }


        public ServiceResult EditClient(ObjClient objClient)
        {
            try
            {
                var client = _clientRepository.Get(objClient.Id);
                UpdateMapping(objClient, client);
                _clientRepository.Update(client);
                _clientRepository.SaveChanges();
                return ServiceResult.SuccessResult();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Edit client error {0}", ex);
                return ServiceResult.ErrorResult("Ошибка при сохранении");
            }
           
        }

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="objClient"></param>
        /// <returns>Client ID</returns>
        public ServiceResult AddClient(ObjClient objClient)
        {
            try
            {
                objClient.Credit = 0;
                var Client = Map(objClient);
                _clientRepository.Insert(Client);
                _clientRepository.SaveChanges();
                return ServiceResult.SuccessResult(Client.Id);
            } catch(Exception ex)
            {
                //_logger.LogError("Add client error {0}", ex);
                return ServiceResult.ErrorResult("Ошибка при создании клиента");
            }
            
        }


        private Client Convert(PotentialClient pClient)
        {
            return new Client
            {
                CompanyName = pClient.CompanyName,
                UserId = pClient.UserId,
                Credit = pClient.Cost * CurrencyConverter.ConvertValute(pClient.Currency, CurrencyType.Rub), 
            };
        }
        private void ConvertToProject(int clientId, PotentialClient potentialClient)
        {
            var Project = new Project
            {
                RootId = clientId,
                RootType = RootTypes.Client,
                Title = potentialClient.Name,
                Description = potentialClient.Description,
                StatusId = potentialClient.StatusId,
                CompeteProcent = 0,
                Cost = potentialClient.Cost,
                Currency = potentialClient.Currency,
                DeadLine = ConvertDateCount(potentialClient.DateType, potentialClient.DateCount),
                ProjectTypeId = potentialClient.ProjectTypeId,
                UserId = potentialClient.UserId,
                Residue = potentialClient.Cost,
            };
            _projectRepository.Insert(Project);
            _potentialClientRepository.SaveChanges();
        }

        private void UpdateMapping(ObjClient obj, Client client)
        {
            client.CompanyName = obj.CompanyName;
            client.Description = obj.Description;
            client.BIK = obj.BIK;
            client.CorrAccount = obj.CorrAccount;
            client.PaymentAccount = obj.PaymentAccount;
            client.BankName = obj.BankName;
            client.Credit = obj.Credit;
            client.UserId = obj.UserId;
        }

        private ObjClient Map(Client client)
        {
            return new ObjClient
            {
                Id = client.Id,
                CompanyName = client.CompanyName,
                Description = client.Description,
                BIK = client.BIK,
                CorrAccount = client.CorrAccount,
                PaymentAccount = client.PaymentAccount,
                BankName = client.BankName,
                Credit = client.Credit,
                UserId = client.UserId,
            };
        }

        private Client Map(ObjClient obj)
        {
            return new Client
            {
                Id = obj.Id,
                CompanyName = obj.CompanyName,
                Description = obj.Description,
                BIK = obj.BIK,
                CorrAccount = obj.CorrAccount,
                PaymentAccount = obj.PaymentAccount,
                BankName = obj.BankName,
                Credit = obj.Credit,
                UserId = obj.UserId,
            };
        }

        //Перенос связанных объектов новому клиенту
        private void TransferRelatedObjects(int ClientId, int PClientId, RootTypes rootType)
        {
           //Коментарии
            var comments = _commentRepository.GetByRootIDAndType(PClientId, rootType);
            foreach (var comment in comments)
            {
                _commentRepository.Insert(new Comment
                {
                    RootId = ClientId,
                    RootType = RootTypes.Client,
                    Text = comment.Text,
                    CreateId = comment.CreateId
                });
            }
            _commentRepository.SaveChanges();

            //Контакты
            var contacts = _contactRepository.GetByRootIdAndType(PClientId, rootType);

            foreach (var contact in contacts)
            {
                _contactRepository.Insert(new Contact
                {
                    RootID = ClientId,
                    RootType = RootTypes.Client,
                    FIO = contact.FIO,
                    Phone = contact.Phone,
                    Email = contact.Email,
                    Other = contact.Other,
                    Created = contact.Created
                });
            }
            _contactRepository.SaveChanges();

            //Файлы
            //var files = _fileDataRepository.GetFiles(PClientId, FileRootTypes.UploadFile, rootType);

            //foreach (var file in files)
            //{
            //    _fileDataRepository.Insert(new FileData
            //    {
            //        RootId = ClientId,
            //        RootType = RootTypes.Client,
            //        FileRootType = FileRootTypes.UploadFile,
            //        OriginalName = file.OriginalName,
            //        Url = file.Url,
            //        Path = file.Path,
            //        CreatedId = file.CreatedId,
            //    });
            //    _fileDataRepository.SaveChanges();
            //}
        }

        private DateTime ConvertDateCount(DateTypes dateType, int DateCount)
        {
            
            switch (dateType)
            {
                case DateTypes.Day:
                    return DateTime.Now.AddDays(DateCount);
                    
                case DateTypes.Week:
                    return DateTime.Now.AddDays(DateCount * 7);
                  
                case DateTypes.Month:
                    return DateTime.Now.AddMonths(DateCount);
                case DateTypes.Year:
                    return DateTime.Now.AddYears(DateCount);
                default:
                    return DateTime.Now;

            }
        }

    }
}
