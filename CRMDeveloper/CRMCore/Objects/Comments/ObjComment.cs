using CRMCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

using CRMCore.Objects;

namespace CRMCore.Objects.Comments
{
    public class ObjComment
    {
        public int Id { get; set; }

        public int RootId { get; set; }

        public RootTypes RootType { get; set; }

        public string Text { get; set; }

        public DateTime Create { get; set; }

        public string CreateStr { get; set; }

        public string CreatedName { get; set; }

        public string CreatedPhotoUrl { get; set; }

        public int? CreatedId { get; set; }

    }
}
