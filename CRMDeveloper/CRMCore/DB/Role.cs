using System;
using System.Collections.Generic;

using CRMCore.DB.Identity;

namespace CRMCore.DB
{
    public class Role : IEntity
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


        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }
        public List<RoleActivity> RoleActivitys { get; set; }

        public Role()
        {
            this.RoleActivitys = new List<RoleActivity>();
        }
    }
}
