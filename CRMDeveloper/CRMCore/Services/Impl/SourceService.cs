using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using CRMCore.DB;
using CRMCore.Extensions;
using CRMCore.Objects;
using CRMCore.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMCore.Services.Impl
{
    public class SourceService : ISourceService
    {
        ISourceRepository _sourceRepository;
        ILogger<SourceService> _logger;
        public SourceService(ISourceRepository sourceRepository,
            ILogger<SourceService> logger)
        {
            _sourceRepository = sourceRepository;
            _logger = logger;
        }
        public ServiceResult Add(ObjSource objSource)
        {
            var source = Map(objSource);
            _sourceRepository.Insert(source);
            _sourceRepository.SaveChanges();
            return ServiceResult.SuccessResult();
        }

        public List<ObjSource> All()
        {
            var result = _sourceRepository.All()
                .OrderBy(x => x.Position);

            return result.Select(x => Map(x))
                .ToList();
        }

        public ServiceResult Delete(int id)
        {
            var source = _sourceRepository.Get(id);
            if (source == null)
                return ServiceResult.ErrorResult("Источник не найден");

            _sourceRepository.Delete(id);
            _sourceRepository.SaveChanges();
            return ServiceResult.SuccessResult();
        }

        public ServiceResult Edit(ObjSource objSource)
        {
            var source = _sourceRepository.Get(objSource.Id);
            if (source.Name == objSource.Name)
                return ServiceResult.ErrorResult("Источник с таким имеенм уже существует");
            source.Name = objSource.Name;
            source.Description = objSource.Description;
            source.Position = objSource.Position;
            _sourceRepository.Update(source);
            _sourceRepository.SaveChanges();
            
            return ServiceResult.SuccessResult();
        }

        public ObjSource Get(int? id)
        {
            var source = _sourceRepository.Get(id.Value);

            return Map(source);
        }

        #region Map

        public ObjSource Map(Source model)
        {
            return new ObjSource()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Position = model.Position,
            };
        }

        public Source Map(ObjSource model)
        {
            return new Source()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Position = model.Position,
            };
        }

        #endregion
    }
}
