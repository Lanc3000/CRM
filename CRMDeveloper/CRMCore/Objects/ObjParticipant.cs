using System;
using System.Collections.Generic;
using System.Text;
using CRMCore.DB;
using CRMCore.Enums;
namespace CRMCore.Objects
{
    public class ObjParticipantList
    {
        public List<ObjParticipant> list { get; set; }
    }

    public class ObjParticipant
    {
        public int Id { get; set; }

        public string FIO { get; set; }
        public int RootId { get; set; }
        public RootTypes RootType { get; set; }

        public string Task { get; set; }
        public decimal WorkSum { get; set; }
        public CurrencyType Currency { get; set; }
        public DateTime WorkPeriod { get; set; }

        public decimal Residue { get; set; }

        public int CreatedId { get; set; }

        public int UserId { get; set; }
    }
}
