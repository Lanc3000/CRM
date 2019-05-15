using System.ComponentModel.DataAnnotations;

namespace CRMCore.Enums
{
    public enum FinanceTypes
    {
        
        /// <summary>
        /// Поступление
        /// </summary>
        [Display(Name = "Поступление")]
        Receipt = 0,

        /// <summary>
        /// Расход
        /// </summary>
        [Display(Name = "Расход")]
        Expence = 1,


    }
}