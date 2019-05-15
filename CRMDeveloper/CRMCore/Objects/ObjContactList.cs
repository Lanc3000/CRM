using System.Collections.Generic;

using CRMCore.Enums;

namespace CRMCore.Objects
{
    public class ObjContactList
    {
        public int LastId { get; set; }

        public List<ObjContact> Contacts { get; set; }

    }

    public class ObjContact
    {
        public int Id { get; set; }

        public int RootId { get; set; }

        public RootTypes RootType { get; set; }

        public string Fio { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Position { get; set; }

        public string Other { get; set; }

        public int? CreatedId { get; set; }
    }
}
