using CRMCore.DB;
using CRMCore.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Objects
{
    public class PotentialClientsModel
    {
        public List<ObjPotentialClient> Pclients { get; set; }
        public List<Status> PStatuses { get; set; }
        public List<User> userList { get; set; }

        public List<int> selectedStatuses { get; set; }
        public List<int> selectedManagers { get; set; }
        public string q { get; set; }

        public PageViewModel pageViewModel { get; set; }
        public const int ItemsPerPage = 10;
    }
}
