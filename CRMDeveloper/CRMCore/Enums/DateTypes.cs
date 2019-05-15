using System.ComponentModel.DataAnnotations;
namespace CRMCore.Enums
{
    public enum DateTypes
    {
        [Display(Name ="День")]
        Day = 0,
        [Display(Name = "Неделя")]
        Week = 1,
        [Display(Name = "Месяц")]
        Month = 2,
        [Display(Name = "Год")]
        Year = 3,
    }
}