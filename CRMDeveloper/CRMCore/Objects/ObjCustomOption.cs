using CRMCore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRMCore.Objects
{
    public class ObjCustomOption
    {
        public int Id { get; set; }
        public CustomOptionType Type { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string Description { get; set; }
        public bool IsHide { get; set; }
        public bool IsRoot { get; set; }
    }

    public class EditCustomOption
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Выберите тип")]
        public CustomOptionType Type { get; set; }
        [Required(ErrorMessage = "Выберите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите позицию")]
        public int Position { get; set; }
        public string Description { get; set; }
        public bool IsHide { get; set; }
        public bool IsRoot { get; set; }
    }
}
