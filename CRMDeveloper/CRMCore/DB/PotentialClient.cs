using System;

using CRMCore.DB.Identity;
using CRMCore.Enums;
using CRMCore.DB;

namespace CRMCore.DB
{
    public class PotentialClient : IEntity
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }
        public string Name { get; set; }

        public Status Status { get; set; }
        public int StatusId { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }
        public CurrencyType Currency { get; set; }

        public byte DateCount { get; set; }
        public DateTypes DateType { get; set; }

        public int ProjectTypeId { get; set; }
        public ProjectType ProjectType { get; set; }

        public int? UserId { get; set; }
        public User Manager { get; set; }

        public int? SourceId { get; set; }
        public Source Source { get; set; }

        public byte Probability { get; set; }

       

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }
    }
}
