using System;
using System.Linq;
using System.Collections.Generic;

using CRMCore.Repositories;
using CRMCore.DB;
using CRMCore.Objects;
using CRMCore.Enums;

namespace CRMCore.Services.Impl
{
    public class ContactService : IContactService
    {
        private IContactRepository _contactRepository { get; }

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public ObjContactList GetListContacts(int RootId, RootTypes rootType)
        {
            ObjContactList result = new ObjContactList();

            var listContact = _contactRepository.GetByRootIdAndType(RootId, rootType);
            listContact = listContact.OrderBy(c => c.Id);

            result.Contacts = new List<ObjContact>();

            foreach (var contact in listContact)
            {
                result.Contacts.Add(Map(contact));
            }
            return result;
        }

        public ObjContact AddContact(ObjContact obj)
        {
            var contact = Map(obj);
            _contactRepository.Insert(contact);
            _contactRepository.SaveChanges();

            return new ObjContact();

        }

        public void DeleteContact(int id)
        {
            _contactRepository.Delete(id);
            _contactRepository.SaveChanges();
        }

        public void EditContact(ObjContact obj)
        {
            var Contact = _contactRepository.Get(obj.Id);
            Contact = UpdateMap(Contact, obj);
            _contactRepository.Update(Contact);
            _contactRepository.SaveChanges();
            
        }

        private ObjContact Map(Contact contact)
        {
            return new ObjContact
            {
                Id = contact.Id,
                RootId = contact.RootID,
                RootType = contact.RootType,
                Fio = contact.FIO,
                Phone = contact.Phone,
                Email = contact.Email,
                Other = contact.Other,
                CreatedId = contact.CreatedId,
                Position = contact.Position,
            };
        }

        private Contact Map(ObjContact obj)
        {
            return new Contact
            {
                Id = obj.Id,
                RootID = obj.RootId,
                RootType = obj.RootType,
                FIO = obj.Fio,
                Phone = obj.Phone,
                Email = obj.Email,
                Other = obj.Other,
                CreatedId = obj.CreatedId,
                Position = obj.Position,
            };
        }

        private Contact UpdateMap(Contact updateContact, ObjContact newContact)
        {
            updateContact.FIO = newContact.Fio;
            updateContact.Phone = newContact.Phone;
            updateContact.Email = newContact.Email;
            updateContact.Other = newContact.Other;
            updateContact.CreatedId = newContact.CreatedId;
            updateContact.Position = newContact.Position;
            return updateContact;
        }
    }
}
