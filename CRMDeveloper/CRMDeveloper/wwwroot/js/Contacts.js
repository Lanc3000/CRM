$(document).ready(function () {
    $('[data-sg-contact="contact-main"]').each(function () {
        iniSGContactItem($(this));
    });
});
function iniSGContactItem(thisItem) {

    var RootId = thisItem.find('[data-sg-contact="contact-rooId"]').val();

    var RootType = thisItem.find('[data-sg-contact="contact-RootType"]').val();

    var ContactList = thisItem.find('[data-sg-contact="contact-list"]');

    var AddContactBtn = $('#ContactTitle').find('.fa-plus-square').parent(); //забираем <a> в котором лежит нужный элементы

    var modalObj = $('#Add-contact');

    var submitBtn = modalObj.find('#save-contact');
    

    function CleanModal() {
        //находим и очищаем модальное окно
        modalObj.find('[data-sg-contact="Id"]').val('');
        modalObj.find('[data-sg-contact="Fio"]').val('');
        modalObj.find('[data-sg-contact="Phone"]').val('');
        modalObj.find('[data-sg-contact="Email"]').val('');
        modalObj.find('[data-sg-contact="Other"]').val('');
    }

    function SubmitModal(url) {
        //получаем объект из модального окна
        var data = new Object();
        data.RootId = thisItem.find('[data-sg-contact="contact-rooId"]').val();
        data.RootType = thisItem.find('[data-sg-contact="contact-RootType"]').val();

        data.Id = modalObj.find('[data-sg-contact="Id"]').val();
        data.Fio = modalObj.find('[data-sg-contact="Fio"]').val();
        data.Phone = modalObj.find('[data-sg-contact="Phone"]').val();
        data.Email = modalObj.find('[data-sg-contact="Email"]').val();
        data.Position = modalObj.find('[data-sg-contact="Position"]').val();
        data.Other = modalObj.find('[data-sg-contact="Other"]').val();

        modalObj.modal('hide');

        submitBtn.unbind('click');

        $.ajax({
            method: "POST",
            url: url,
            data: data,
            error: function () { toastr.error("Ошибка запроса", "Контакты"); },
            success: function (data) {
                if (data.success === false)
                    toastr.error(data.errorMessage, "Контакты");
                else {
                    toastr.success('Контакт сохранен', 'Контакты');
                    AppendLoadContacts(data);
                }
            }
        });
    }

    AddContactBtn.click(function () {
        CleanModal();
        let url = '/Contacts/AddContact';
        modalObj.find('.modal-title').html('Добавление контакта');
        submitBtn.click(function () {
            SubmitModal(url);
        })
        modalObj.modal('show');
    });

    function Load() {
        $.ajax({
            url: "/contacts/GetContactList?rootId=" + RootId + "&rootType=" + RootType,
            error: function () { toastr.error("Ошибка запроса", "Контакты"); },
            success: function (data) {
                if (data.success === false)
                    toastr.error(data.errorMessage, "Контакты");
                else {
                    AppendLoadContacts(data);
                }
            }
        });
    }

    function AppendLoadContacts(data) {

        if ($('div.contact-item').length > 0) {
            let ContactItem = thisItem.find('[data-sg-contact="contact-item-first"]').clone();
            $('div.contact-item').remove();
            ContactItem.prependTo(ContactList);
        }


        $.each(data.contacts, function (index, value) {

            var ContactItem = thisItem.find('[data-sg-contact="contact-item-first"]').clone();
            ContactItem.show();
            ContactItem.attr('data-sg-contact', 'contact-item');
            ContactItem.find('[data-sg-contact="contact-Id"]').val(value.id);

            ContactItem.find('[data-sg-contact="Fio"]').html(value.fio);

            if (value.position !== null) {
                ContactItem.find('[data-sg-contact="Position"]').html('(' + value.position + ')');
                ContactItem.find('[data-sg-contact="Position"]').show();
            }

            if (value.phone !== null) {
                ContactItem.find('[data-sg-contact="Phone-input"]').html(value.phone);
                ContactItem.find('[data-sg-contact="Phone"]').show();
            }

            if (value.email !== null) {
                ContactItem.find('[data-sg-contact="Email-input"]').html(value.email);
                ContactItem.find('[data-sg-contact="Email"]').show();
                
            }

            if (value.other !== null) {
                ContactItem.find('[data-sg-contact="Other-input"]').html(value.other);
                ContactItem.find('[data-sg-contact="Other"]').show();
            }


           
            
            

            let editContactBtn = ContactItem.find('a i.fa-pencil').parent();
            let deleteContactBtn = ContactItem.find('a i.fa-trash').parent();

            editContactBtn.click(function () {
                let url = "/Contacts/EditContact";
                CleanModal();
                modalObj.find('[data-sg-contact="Id"]').val(value.id);
                modalObj.find('[data-sg-contact="Fio"]').val(value.fio);
                modalObj.find('[data-sg-contact="Phone"]').val(value.phone);
                modalObj.find('[data-sg-contact="Email"]').val(value.email);
                modalObj.find('[data-sg-contact="Other"]').val(value.other);
                modalObj.find('[data-sg-contact="Position"]').val(value.position);

                modalObj.find('.modal-title').html('Редактирование контакта');

                submitBtn.click(function () {
                    SubmitModal(url);
                });
                modalObj.modal('show');
            });

            deleteContactBtn.click(function () {
                let id = ContactItem.find('[data-sg-contact="contact-Id"]').val();

                $.ajax({
                    method: "POST",
                    url: "/contacts/DeleteContact?id=" + id + "&rootId=" + RootId + "&rootType=" + RootType,
                    error: function () { toastr.error("Ошибка запроса", "Контакты"); },
                    success: function (data) {
                        if (data.success === false)
                            toastr.error(data.errorMessage, "Контакты");
                        else {
                            toastr.success('Контакт удален', 'Контакты');
                            AppendLoadContacts(data);
                        }
                    }
                });
            });

            
            ContactItem.prependTo(ContactList);

        });
        ContactList.animate({ scrollTop: 0 }, "fast");
    }
    Load();
}








