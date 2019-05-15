using CRMCore.DB.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRMCore.DB
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Grade { get; set; }
        public string Skype { get; set; }
        public string Telegram { get; set; }
        public string Other { get; set; }

        public string PassHash { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public string Avatar { get; set; }

        public bool IsManager { get; set; }

        public bool Deleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string AvatarFileName { get;  set; }
    }
}
