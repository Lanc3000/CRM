using CRMCore.DB.Identity;
using CRMCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.DB
{
    public class CustomOption : IEntity
    {
        public int Id { get; set; }
        public CustomOptionType Type { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string Description { get; set; }
        public bool IsHide { get; set; }
        public bool IsRoot { get; set; }
        public bool Deleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
