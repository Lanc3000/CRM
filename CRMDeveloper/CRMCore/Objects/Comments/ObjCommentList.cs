using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Objects.Comments
{
    public class ObjCommentList
    {
        public int lastId { get; set; }

        public List<ObjComment> items { get; set; }

        public string q { get; set; }
    }
}
