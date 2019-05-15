using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRMCore.Objects
{
    public class ObjRole
    {
        public int Id { get; set; }
        /// <summary>
        /// Для разработчиков
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Для отображения
        /// </summary>
        public string Name { get; set; }

        public List<string> RoleActivitys { get; set; }

    }

    public class ObjRoleEdit
    {
        [Required(ErrorMessage = "Введите наименование роли")]
        /// <summary>
        /// Для разработчиков
        /// </summary>
        public string Title { get; set; }
        [Required(ErrorMessage = "Введите имя роли")]
        /// <summary>
        /// Для отображения
        /// </summary>
        public string Name { get; set; }

        public int Id { get; set; }

        public List<ObjActivitySelect> SelectedActivities { get; set; }
    }

    public class ObjActivitySelect
    {
        public string Name { get; set; }

        public bool Checked { get; set; }
    }
}
