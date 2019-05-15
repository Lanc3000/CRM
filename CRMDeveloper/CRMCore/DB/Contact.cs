using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.DB.Identity;
using CRMCore.Enums;

namespace CRMCore.DB
{
    public class Contact : IEntity
    {
        public int Id { get; set; }

        public int RootID { get; set; }

        public RootTypes RootType { get; set; }

        public string FIO { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Position { get; set; }

        public string Other { get; set; }

        public int? CreatedId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }
    }
}
