using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRMCore.Objects
{
    public class ObjChangePassword
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите новый пароль")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Введите подтверждение пароля")]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и подтверждение не совпадают")]
        public string NewPasswordConfirm { get; set; }
    }

    public class ObjChangeEmail
    {
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите Email")]
        public string NewEmail { get; set; }
    }
}
