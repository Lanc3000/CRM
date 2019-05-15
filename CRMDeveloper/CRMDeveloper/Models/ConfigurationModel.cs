using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CRMCore.Objects;

namespace CRMDeveloper.Models
{
    public class ConfigurationModel
    {
        public List<ObjSource> Sources { get; set; }
        public List<ObjPTask> Tasks { get; set; }
        public List<ObjTaskType> TaskTypes {get; set;}
        public List<ObjProjectType> ProjectTypes { get; set; }
        public List<ObjFinanceSubType> objFinanceSubTypes { get; set; }
    }
}
