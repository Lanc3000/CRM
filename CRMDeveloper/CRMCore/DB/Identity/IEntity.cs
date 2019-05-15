using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.DB.Identity
{
    public interface IEntity
    {
        int Id { get; set; }

        DateTime Created { get; set; }
        DateTime Modified { get; set; }
        bool Deleted { get; set; }
        
    }
}
