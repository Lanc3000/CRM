$(document).ready(function () {
    InitFinances($('[data-sg-finance="finance-main"]'));
});

function InitFinances(mainItem) {
    var RootId = mainItem.find('[data-sg-finance="finance-rootId"]').val();

    var RootType = mainItem.find('[data-sg-finance="finance-RootType"]').val();

    var FinanceList = mainItem.find('[data-sg-finance="finance-list"]');

    var AddFinanceBtn = $('#FinanceTitle').find('.fa-plus-square').parent();

    var modalWindow = $('#Add-Finance');

    var SubmitModalBtn = $('#save-finance');

    function CleanModal() {

    }

    function SubmitModal(url) {
        var data = new Object();
        data.RootId = RootId;
        data.RootType = RootType;

        data.Id = modalWindow.find('[data-sg-finance="Id"]').val();
        data.FinanceType = modalWindow.find('[data-sg-finance="type"]').val();
        data.SubTypeId = modalWindow.find('[data-sg-finance="subtype"]').val();
        data.ProjectId = modalWindow.find('[data-sg-finance="project"]').val();
        data.Date = modalWindow.find('[data-sg-finance="date"]').val();
        data.PlaceId = modalWindow.find('[data-sg-finance="place"]').val();
        data.Cost = modalWindow.find('[data-sg-finance="cost"]').val();
        data.Currency = modalWindow.find('[data-sg-finance="currency"]').val();
        data.UserId = modalWindow.find('[data-sg-finance="userFio"]').val();
        data.DocumentName = "";

        modalWindow.modal('hide');

        SubmitModalBtn.unbind('click');

        $.ajax({
            type: "POST",
            url: url,
            data: data,
            error: function () { toastr.error("Ошибка запроса", "Финансы"); },
            success: function (data) {
                if (data.success === false)
                    toastr.error(data.errorMessage, "Финансы");
                else {
                    toastr.success("Добавлен", "Финансы");
                    console.log(data.result.length);
                    AppendLoadFinances(data.result);
                }
            }
        });
    }

    AddFinanceBtn.click(function () {
        CleanModal();
        modalWindow.find('.modal-title').html('Добавить расход/доход');
        let url = "/Finance/AddFinance";

        SubmitModalBtn.click(function () {
            SubmitModal(url);
        });
        modalWindow.modal('show');
    });

    function Load() {
        $.ajax({
            url: "/Finance/GetFinancesList?rootId=" + RootId + "&rootType=" + RootType,
            error: function () { toastr.error("Ошибка запроса", "Финансы"); },
            success: function (data) {
                if (data.success === false)
                    toastr.error(data.errorMessage, "Финансы");
                else {
                    console.log(data.result.length);
                    AppendLoadFinances(data.result);
                }
            }
        });
    }

    function AppendLoadFinances(data) {
        var FinanceItemFirst = mainItem.find('[data-sg-finance="finance-item-first"]').clone();
        $('div.finance-item').remove();
        FinanceItemFirst.prependTo(FinanceList);

        $.each(data, function (index, value) {
            let FinanceItem = mainItem.find('[data-sg-finance="finance-item-first"]').clone();
            FinanceItem.attr('data-sg-finance', 'finance-item');
            if (RootType === "User") {
                FinanceItem.find('[data-sg-finance="type"]').html(value.subTypeName);
            }
            else {
                let labelColor = value.financeType === 0 ? "label-primary" : "label-danger";
                FinanceItem.find('[data-sg-finance="type"]').attr('class', 'label ' + labelColor);
                FinanceItem.find('[data-sg-finance="type"]').show();
                FinanceItem.find('[data-sg-finance="type"]').html(GetNamebyEnum('FinanceTypes', value.financeType));
                if (value.userId !== null) {
                    FinanceItem.find('[data-sg-finance="userName"]').html(value.userName);
                    FinanceItem.find('[data-sg-finance="userName"]').attr('href', '/Users/Details?id=' + value.userId);
                    FinanceItem.find('[data-sg-finance="user"]').show();
                }
            }
            FinanceItem.find('[data-sg-finance="Id"]').val(value.id);

            FinanceItem.find('[data-sg-finance="projectName"]').html(value.projectName);
            FinanceItem.find('[data-sg-finance="projectName"]').attr('href', '/Projects/Details?id=' + value.projectId);

            FinanceItem.find('[data-sg-finance="place"]').html(GetNamebyEnum('FinancePlaceTypes', value.place));

            FinanceItem.find('[data-sg-finance="cost"]').html(value.cost);
            FinanceItem.find('[data-sg-finance="currency"]').html(GetNamebyEnum('Currency', value.currency));

            FinanceItem.find('[data-sg-finance="date"]').html(value.date.split('T')[0]);

            FinanceItem.find('a i.fa-trash').parent().click(function () {
                let id = FinanceItem.find('[data-sg-finance="Id"]').val();
                $.ajax({
                    method: 'POST',
                    url: '/finance/deletefinance?id=' + id + '&rootId=' + RootId + "&rootType=" + RootType,
                    error: function () { toastr.error("Ошибка запроса", "Финансы"); },
                    success: function (data) {
                        if (data.success === false)
                            toastr.error(data.errorMessage, "Финансы");
                        else {
                            console.log(data.result.length);
                            toastr.success('Успешно удалено', 'Финансы');
                            AppendLoadFinances(data.result);
                        }
                    }
                });
            });
            FinanceItem.prependTo(FinanceList);
            FinanceItem.show();
            
        });
    }

    Load();

}