using System.ComponentModel.DataAnnotations;
namespace CRMCore.Enums
{
    public enum FinancePlaceTypes
    {
        [Display(Name = "Наличные")]
        Cash = 0,
        
        [Display (Name = "Карта")]
        Card = 1,

        [Display (Name = "Расчетный счет")]
        CurrentAccount
    }
    //Для отображения на стороне клиента EnumUtils,js
   
}