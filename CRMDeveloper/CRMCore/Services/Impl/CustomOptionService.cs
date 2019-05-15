using CRMCore.DB;
using CRMCore.Enums;
using CRMCore.Objects;
using CRMCore.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CRMCore.Extensions;
using NLog;

namespace CRMCore.Services.Impl
{
    public class CustomOptionService : ICustomOptionsService
    {
        readonly ICustomOptionRepository _customOptionsRepository;
        readonly Logger _logger;

        public CustomOptionService(ICustomOptionRepository customOptionRepository)
        {
            _customOptionsRepository = customOptionRepository;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public List<Grouping<CustomOptionType, ObjCustomOption>> GetVM()
        {
            var groupByCustomItems = _customOptionsRepository.All().Select(x=>Map(x))
                .GroupBy(x=>x.Type);

            var result = groupByCustomItems.Select(x => new Grouping<CustomOptionType, ObjCustomOption>(x))
                .ToList();

            return result;
        }

        public EditCustomOption Get(int id)
        {
            if (id == 0)
                return new EditCustomOption();
            var customOptiondb = _customOptionsRepository.Get(id);

            var result = MapEdit(customOptiondb);

            return result;
        }

        public ServiceResult Save(EditCustomOption editModel)
        {
            try
            {
                var dbCustomOption = MapEdit(editModel);
                _customOptionsRepository.Insert(dbCustomOption);
                _customOptionsRepository.SaveChanges();
                return ServiceResult.SuccessResult();
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error with save CustommOption = " + editModel.Id);
                return ServiceResult.ErrorResult("Ошибка при сохранении данных");
            }
        }

        public ServiceResult Update(EditCustomOption editModel)
        {
            try
            {
                var customOption = _customOptionsRepository.Get(editModel.Id);
                customOption.Name = editModel.Name;
                customOption.Description = editModel.Description;
                customOption.IsHide = editModel.IsHide;
                customOption.Position = editModel.Position;
                customOption.IsRoot = editModel.IsRoot;
                customOption.Type = editModel.Type;

                _customOptionsRepository.Update(customOption);
                _customOptionsRepository.SaveChanges();

                return ServiceResult.SuccessResult();
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error with update CustomOption id = " + editModel.Id);
                return ServiceResult.ErrorResult("Ошибка при обновлении данных");
            }
        }

        public ServiceResult Delete(int id)
        {
            try
            {
                _customOptionsRepository.Delete(id);
                _customOptionsRepository.SaveChanges();
                return ServiceResult.SuccessResult();
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error with delete CustomOption id = " + id);
                return ServiceResult.ErrorResult("Ошибка при удалении");
            }
        }



        public ObjCustomOption Map(CustomOption dbModel)
        {
            var result = new ObjCustomOption()
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Description = dbModel.Description,
                IsHide = dbModel.IsHide,
                Position = dbModel.Position,
                IsRoot = dbModel.IsRoot,
                Type = dbModel.Type
            };
            return result;
        }

        public EditCustomOption MapEdit(CustomOption dbModel)
        {
            var result = new EditCustomOption()
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Description = dbModel.Description,
                IsHide = dbModel.IsHide,
                Position = dbModel.Position,
                IsRoot = dbModel.IsRoot,
                Type = dbModel.Type
            };

            return result;
        }

        public CustomOption MapEdit(EditCustomOption editModel)
        {
            var result = new CustomOption()
            {
                Id = editModel.Id,
                Name = editModel.Name,
                Description = editModel.Description,
                IsHide = editModel.IsHide,
                Position = editModel.Position,
                IsRoot = editModel.IsRoot,
                Type = editModel.Type
            };

            return result;
        }

       
    }
}
