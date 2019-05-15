using CRMCore.DB.Identity;
using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.Enums;

namespace CRMCore.DB
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        
        public int RootId { get; set; }

        public RootTypes RootType { get; set; }

        public string Text { get; set; }

        public int? CreateId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }


    }
}
