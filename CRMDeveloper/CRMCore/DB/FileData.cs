using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.DB.Identity;
using CRMCore.Enums;

namespace CRMCore.DB
{
    public class FileData : IEntity
    {
        public int Id { get; set; }

        public int RootId { get; set; }

        public RootTypes RootType { get; set; }

        public string OriginalName { get; set; }

        public string FileName { get; set; }

        public int? CreatedId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }
    }
}
