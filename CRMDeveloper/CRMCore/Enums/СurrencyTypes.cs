using System.ComponentModel.DataAnnotations;

namespace CRMCore.Enums
{
    public enum CurrencyType
    {
        [Display(Name = "Руб.")]
        Rub = 0,
        [Display(Name = "Долл.")]
        Dollat = 1,
        [Display (Name = "Евро.")]
        Euro = 2,
    }
   
}