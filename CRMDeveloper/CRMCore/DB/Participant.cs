using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.DB.Identity;
using CRMCore.Enums;

namespace CRMCore.DB
{
    public class Participant : IEntity
    {
        public int Id { get; set; }

        public int RootId { get; set; }
        public RootTypes RootType { get; set; }

       
        public decimal WorkSum { get; set; }
        public CurrencyType Currency { get; set; }

        public DateTime DateStartWork { get; set; }
        public DateTime WorkPeriod { get; set; }

        public decimal Residue { get; set; }//Остаток

        public string Task { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public int CreatedId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }
    }
}
