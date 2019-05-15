using CRMCore.DB.Identity;
using CRMCore.Enums;

using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.DB
{
    public class Finance : IEntity
    {
        public int Id { get; set; }

        public FinanceTypes FinanceType { get; set; }

        public int? SubTypeId { get; set; }
        public FinanceSubType SubType { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public DateTime Date { get; set; }

        public FinancePlaceTypes Place { get; set; }

        public decimal Cost { get; set; }
        public CurrencyType Currency { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public string DocumentName { get; set; }

        public int CreatedId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }
    }
}
