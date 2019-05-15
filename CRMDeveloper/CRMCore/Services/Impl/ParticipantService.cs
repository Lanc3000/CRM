using CRMCore.DB;
using CRMCore.Enums;
using CRMCore.Objects;
using CRMCore.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CRMCore.Services.Impl
{
    public class ParticipantService : IParticipantService
    {
        IParticipantRepository _participantRepository { get; }
        IUserService _userService { get; }
        IPTaskRepository _pTaskRepository { get; }

        public ParticipantService(IParticipantRepository participantRepository,
            IUserService userService,
            IPTaskRepository pTaskRepository)
        {
            _participantRepository = participantRepository;
            _userService = userService;
            _pTaskRepository = pTaskRepository;
        }

        public ObjParticipantList GetList(int rootId, RootTypes rootType)
        {
            ObjParticipantList result = new ObjParticipantList();

            var listParticipants = _participantRepository.Get(rootId, rootType);

            result.list = new List<ObjParticipant>();

            foreach (var participant in listParticipants)
            {
                result.list.Add(Map(participant));
            }
            return result;
        }

        

        public void Add(ObjParticipant obj)
        {
            var participant = Map(obj);
            _participantRepository.Insert(participant);
            _participantRepository.SaveChanges();
        }

        public void Delete(int id)
        {
            _participantRepository.Delete(id);
            _participantRepository.SaveChanges();
        }

        public void Edit(ObjParticipant obj)
        {
            var participant = _participantRepository.Get(obj.Id);
            UpdateMap(participant, obj);
            _participantRepository.Update(participant);
            _participantRepository.SaveChanges();

        }

        #region Ptasks
        public List<ObjPTask> GetTasks()
        {
            var tasks = _pTaskRepository.All();
            return tasks
                .Select(x => Map(x))
                .OrderBy(x => x.Position)
                .ToList();
        }

        public ObjPTask GetPTask(int id)
        {
            var task = _pTaskRepository.Get(id);
            return Map(task);
        }

        public void UpdatePTask(ObjPTask model)
        {
            var ptask = _pTaskRepository.Get(model.Id);

            ptask.Name = model.Name;
            ptask.Description = model.Description;
            ptask.Position = model.Position;
            _pTaskRepository.Update(ptask);
            _pTaskRepository.SaveChanges();
        }

        public void AddPTask(ObjPTask objPTask)
        {
            var taks = Map(objPTask);
            _pTaskRepository.Insert(taks);
            _pTaskRepository.SaveChanges();
        }

        public void DeletePTask(int id)
        {
            _pTaskRepository.Delete(id);
            _pTaskRepository.SaveChanges();
        }

        public string[] GetNamePtasks()
        {
            var result = GetTasks()
                .Select(x => x.Name)
                .ToArray();

            return result;
        }
        #endregion


        #region Map
        private ObjParticipant Map(Participant participant)
        {
            return new ObjParticipant
            {
                Id = participant.Id,
                RootId = participant.RootId,
                RootType = participant.RootType,
                FIO = participant.User.Fio,
                Task = participant.Task,
                WorkSum = participant.WorkSum,
                WorkPeriod = participant.WorkPeriod,
                Residue = participant.Residue,
                Currency = participant.Currency,
                CreatedId = participant.CreatedId,
                UserId = participant.UserId,
            };
        }
        private Participant Map(ObjParticipant obj)
        {
            return new Participant
            {
                Id = obj.Id,
                RootId = obj.RootId,
                RootType = obj.RootType,
                WorkSum = obj.WorkSum,
                WorkPeriod = obj.WorkPeriod,
                Residue = obj.WorkSum,
                CreatedId = obj.CreatedId,
                Currency = obj.Currency,
                UserId = obj.UserId,
                Task = obj.Task,
            };
        }

        private void UpdateMap(Participant participant, ObjParticipant obj)
        {
            participant.Task = obj.Task;
            participant.UserId = obj.UserId;
            participant.WorkSum = obj.WorkSum;
            participant.WorkPeriod = obj.WorkPeriod;
            participant.CreatedId = obj.CreatedId;
            participant.Currency = obj.Currency;
        }

        private ObjPTask Map(PTask pTask)
        {
            return new ObjPTask()
            {
                Id = pTask.Id,
                Name = pTask.Name,
                Description = pTask.Description,
                Position = pTask.Position
            };
        }

        private PTask Map(ObjPTask objPTask)
        {
            return new PTask()
            {
                Id = objPTask.Id,
                Name = objPTask.Name,
                Description = objPTask.Description,
                Position = objPTask.Position,
            };
        }
        #endregion




    }
}
