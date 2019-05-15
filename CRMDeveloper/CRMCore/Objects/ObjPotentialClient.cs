using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using CRMCore.Enums;

namespace CRMCore.Objects
{
    public class ObjPotentialClient
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Введите название проекта")]
        public string Name { get; set; }

        public string StatusName { get; set; }
        public string StatusColor { get; set; }
        public int StatusId { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Введите стоимость проекта")]
        public decimal Cost { get; set; }

        public CurrencyType Currency { get; set; }

        [Required(ErrorMessage = "Введите продолжительность работ")]
        public byte DateCount { get; set; }

        public DateTypes DateType { get; set; }

        public int ProjectTypeId { get; set; }

        public int? UserId { get; set; }
        public ObjUser Manager { get; set; }

        public int? SourceId { get; set; }
        public ObjSource Source { get; set; }

        public DateTime Created { get; set; }

        [Required(ErrorMessage = "Введите вероятность заказа")]
        public byte Probability { get; set; }
    }
}
