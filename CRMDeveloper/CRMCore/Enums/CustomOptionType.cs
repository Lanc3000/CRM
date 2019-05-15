using System.ComponentModel.DataAnnotations;
namespace CRMCore.Enums
{
    public enum CustomOptionType
    {
        [Display(Name = "Источник")]
        Source = 0,

        [Display(Name = "Задачи")]
        Task = 1,

        [Display(Name = "Типы проектов")]
        ProjectTypes = 2,

        [Display(Name = "Подтипы финансов")]
        FinanceTypes = 3,
    }
}