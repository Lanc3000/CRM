using CRMCore.DB.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.DB
{
    public class FinanceSubType : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }


        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }
    }
}
