using System;

using CRMCore.Enums;
using CRMCore.Objects;
namespace CRMCore.Services
{
    public interface IContactService
    {
        ObjContactList GetListContacts(int RootId, RootTypes rootType);
        ObjContact AddContact(ObjContact obj);
        void DeleteContact(int id);
        void EditContact(ObjContact obj);
    }
}
