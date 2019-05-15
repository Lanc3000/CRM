using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.DB.Identity;

namespace CRMCore.DB
{
    public class RoleActivity : IEntity
    {
        public int Id { get; set; }
        public string Activity { get; set; }
        public int RoleId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }

        public Role Role { get; set; }
        
    }
}
