using System.ComponentModel.DataAnnotations;

namespace CRMCore.Enums
{
    public enum RootTypes
    {
        [Display(Name = "Пользовать")]
        User = 0,
        [Display(Name = "Потенциальный клиент")]
        PotentialCLient = 1,
        [Display(Name = "Клиент")]
        Client = 2,
        [Display(Name = "Проект")]
        Project = 3,
        [Display(Name = "Финансы")]
        Finance = 4,
        [Display(Name = "Задача")]
        Task = 5,

    }
}