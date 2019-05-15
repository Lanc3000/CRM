using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Objects
{
    public class ObjUserList
    {
        public int count { get; set; }
        public List<ObjUserListItem> item { get; set; }
    }
    public class ObjUserListItem
    {
        public int id { get; set; }
        public string fio { get; set; }
        public string position { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int grade { get; set; }
        public string skype { get; set; }
        public string telegram { get; set; }
        public string other { get; set; }
    }
}
