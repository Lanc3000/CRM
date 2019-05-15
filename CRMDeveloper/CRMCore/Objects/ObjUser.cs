using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRMCore.Objects
{
    public class ObjUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите Фио")]
        public string Fio { get; set; }

        [Required(ErrorMessage = "Введите должность")]
        public string Position { get; set; }

        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите грейд")]
        public int Grade { get; set; }

        public string Skype { get; set; }
        public string Telegram { get; set; }
        public string Other { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        public string PassHash { get; set; }

        public int RoleId { get; set; }
        public ObjRole Role { get; set; }

        public bool IsManager { get; set; }

        public IFormFile file { get; set; }

        public string AvatarFileName { get;  set; }
    }
}
