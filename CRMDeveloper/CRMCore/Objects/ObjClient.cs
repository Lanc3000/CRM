using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using CRMCore.Enums;

namespace CRMCore.Objects
{
    public class ObjClient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название компании")]
        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string BIK { get; set; }

        public string CorrAccount { get; set; }

        public string PaymentAccount { get; set; }

        public string BankName { get; set; }

        public decimal Credit { get; set; }//rub
        

        public int? UserId { get; set; }

        public DateTime DeadLine { get; set; }
    }
}
