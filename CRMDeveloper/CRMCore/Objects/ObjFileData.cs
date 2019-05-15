using System;
using System.Collections.Generic;

using CRMCore.Enums;

namespace CRMCore.Objects
{
    public class ObjFileDataList
    { 
        public List<ObjFileData> Files { get; set; }
    }

    public class ObjFileData
    {
        public int Id { get; set; }

        public int RootId { get; set; }

        public RootTypes RootType { get; set; }

        public DateTime? Create { get; set; }

        public string Url { get; set; }

        public string OriginalName { get; set; }

        public string CreatedName { get; set; }

        public string CreatedPhotoUrl { get; set; }

        public int? CreatedId { get; set; }

        public string FileName { get;  set; }
    }

    public class ObjFileStream
    {
        public string OriginalName { get; set; }
        public byte[] Data { get;  set; }
    }
}