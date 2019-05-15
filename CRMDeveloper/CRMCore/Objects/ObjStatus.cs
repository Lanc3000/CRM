using CRMCore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRMCore.Objects
{
    public class ObjStatus
    {
        public int Id { get; set; }
        public RootTypes rootType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public bool IsRoot { get; set; }
        public int Position { get; set; }
        public bool IsHide { get; set; }
    }
}
