using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRMCore.DB;
using CRMCore.Enums;
using CRMCore.Objects;
using CRMCore.Repositories;
using CRMCore.Extensions;

namespace CRMCore.Services.Impl
{
    public class StatusService : IStatusService
    {
        IStatusRepository _statusRepository { get; set; }

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public List<Grouping<RootTypes,ObjStatus>> GetAllStatuses()
        {
            var groupByStatuses = _statusRepository.All().OrderBy(x=>x.Position).Select(x => Map(x))
                .GroupBy(x => x.rootType);

            var result = groupByStatuses.Select(group => new Grouping<RootTypes, ObjStatus>(group))
                .ToList();

            

            return result;
        }

        private ObjStatus Map(Status status)
        {
            return new ObjStatus()
            {
                Id = status.Id,
                rootType = status.rootType,
                Name = status.Name,
                Description = status.Description,
                Color = status.Color,
                Position = status.Position,
                IsRoot = status.IsRoot,
                IsHide = status.IsHide,
            };
        }

        private Status Map(ObjStatus objStatus)
        {
            return new Status()
            {
                Id = objStatus.Id,
                rootType = objStatus.rootType,
                Name = objStatus.Name,
                Description = objStatus.Description,
                Color = objStatus.Color,
                Position = objStatus.Position,
                IsRoot = objStatus.IsRoot,
                IsHide = objStatus.IsHide,
            };
        }

        public void AddStatus(ObjStatus objStatus)
        {
            var status = Map(objStatus);
            _statusRepository.Insert(status);
            _statusRepository.SaveChanges();
        }

        public ObjStatus Get(int id)
        {
            var status = _statusRepository.Get(id);
            return Map(status);
        }

        public void Update(ObjStatus model)
        {
            var status = _statusRepository.Get(model.Id);

            
            status.Description = model.Description;
            status.Color = model.Color;
            status.IsHide = model.IsHide;
            //IsRoot не должен меняться
            if(!status.IsRoot)
            {
                status.Name = model.Name;
                status.rootType = model.rootType;
                status.Position = model.Position;
            }
               

            _statusRepository.Update(status);
            _statusRepository.SaveChanges();
        }

        public void Delete(int id)
        {
            _statusRepository.Delete(id);
            _statusRepository.SaveChanges();
        }

        public List<ObjStatus> GetStatusesByRootType(RootTypes rootType)
        {
            var statuses = _statusRepository.Get(rootType);

            return statuses
                .Select(x => Map(x))
                .OrderBy(x => x.Position)
                .ToList();
        }

    }
}
