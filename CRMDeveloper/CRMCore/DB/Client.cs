using System;

using CRMCore.DB.Identity;
using CRMCore.Enums;


namespace CRMCore.DB
{
    public class Client : IEntity
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string BIK { get; set; }

        public string CorrAccount { get; set; }

        public string PaymentAccount { get; set; }

        public string BankName { get; set; }

        public decimal Credit { get; set; }//rub
       

        public int? UserId { get; set; }
        public User Manager { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }
    }
}
