using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Objects
{
    public class ObjClientP
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

        public DateTime DeadLine { get; set; }

        public List<ObjProject> ListProjects { get; set; }
    }
}
