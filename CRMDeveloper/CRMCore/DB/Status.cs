using CRMCore.DB.Identity;
using CRMCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.DB
{
    public class Status : IEntity
    {
        public int Id { get; set; }
        public RootTypes rootType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int Position { get; set; }
        public bool IsRoot { get; set; }
        public bool IsHide { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }
    }
}
