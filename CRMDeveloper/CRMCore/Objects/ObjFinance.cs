using CRMCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Objects
{
    public class ObjFinance
    {
        public int Id { get; set; }

        public RootTypes RootType { get; set; }
        public int RootId { get; set; }

        public FinanceTypes FinanceType { get; set; }

        public string SubTypeName { get; set; }
        public int? SubTypeId { get; set; }

        public string ProjectName { get; set; }
        public int ProjectId { get; set; }

        public DateTime Date { get; set; }

        public FinancePlaceTypes Place { get; set; }

        public decimal Cost { get; set; }
        public CurrencyType Currency { get; set; }

        public string UserName { get; set; }
        public int? UserId { get; set; }

        public string DocumentName { get; set; }

        public int CreatedId { get; set; }
    }
}
