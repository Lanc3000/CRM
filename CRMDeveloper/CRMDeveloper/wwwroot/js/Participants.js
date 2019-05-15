$(document).ready(function () {
    InitParticipants($('[data-sg-participant="partic-main"]'));
});

function InitParticipants(mainItem) {
    var RootId = mainItem.find('[data-sg-participant="partic-rooId"]').val();
    var RootType = mainItem.find('[data-sg-participant="partic-RootType"]').val();

    var AddParticipantBtn = $('#ParticipantsTitle').find('.fa-plus-square').parent();

    var ParticList = mainItem.find('[data-sg-participant="partic-list"]');

    var modalWindow = $('#Add-participant');

    var SubmitModalBtn = $('#save-participant');

    function GetTasks() {
        $.ajax({
            type: "GET",
            url: "/Configurations/GetTasks",
            success: function (data) {
                let result = '';
                for (var i = 0; i < data.length; i++) {
                    if (i === 0)
                        result += '[';
                    if (i === data.length - 1) {
                        result += '"' + data[i] + '"]';
                        break;
                    }
                    result +='"' + data[i] + '",';
                    
                }
                modalWindow.find('[data-sg-participant="Ptask"]').attr('data-source', result);
            },
            error: function () {
                console.log("Ошибка загрузки Задач");
            }
            
            
        })
    }

    function CleanModal() {
        modalWindow.find('[data-sg-participant="Id"]').val('');
        modalWindow.find('[data-sg-participant="User"] :selected').removeAttr('selected');
        modalWindow.find('[data-sg-participant="WorkSum"]').val('');
        modalWindow.find('[data-sg-participant="Currency"] :selected').removeAttr('selected');
        modalWindow.find('[data-sg-participant="WorkPeriod"]').val('');
    }

    function SubmitModal( url) {
        
        var data = new Object();
        data.RootId = RootId;
        data.RootType = RootType;

        data.Id = modalWindow.find('[data-sg-participant="Id"]').val();
        data.UserId = modalWindow.find('[data-sg-participant="User"]').val();
        data.Task = modalWindow.find('[data-sg-participant="Ptask"]').val();
        data.WorkSum = modalWindow.find('[data-sg-participant="WorkSum"]').val();
        data.Currency = modalWindow.find('[data-sg-participant="Currency"]').val();
        data.WorkPeriod = modalWindow.find('[data-sg-participant="WorkPeriod"]').val();

        modalWindow.modal('hide');

        SubmitModalBtn.unbind('click');

        $.post(url, data, function (result) {
            AppendLoadPartic(result);
        });
    }

    AddParticipantBtn.click(function () {
        CleanModal();
        modalWindow.find('.modal-title').html('Добавление участника');
        let url = "/Participants/AddParticipant";
        SubmitModalBtn.click(function () {
            SubmitModal(url);
        });
        modalWindow.modal('show');
    });

    function Load() {
        var url = "/participants/GetParticipantList?rootId=" + RootId + "&rootType=" + RootType;
        $.get(url, function (data) {
            AppendLoadPartic(data);
        });
    }

    function AppendLoadPartic(data) {
        var ParticipItemFirst = mainItem.find('[data-sg-participant="partic-item-first"]').clone();
        $('div.partic-item').remove();
        ParticipItemFirst.prependTo(ParticList);

        $.each(data.list, function (index, value) {
            let ParticipItem = mainItem.find('[data-sg-participant="partic-item-first"]').clone();
            ParticipItem.attr('data-sg-participant', 'partic-item');
            ParticipItem.find('[data-sg-participant="partic-Id"]').val(value.id);
            ParticipItem.find('[data-sg-participant="FIO"]').html('<img src="/Users/Avatar/' + value.userId + '" alt="Фото" class="participants-img" />' + value.fio);
            ParticipItem.find('[data-sg-participant="FIO"]').attr('href', "/Users/Details/" + value.userId);
            ParticipItem.find('[data-sg-participant="WorkSum"]').html(value.workSum);
            ParticipItem.find('[data-sg-participant="Residue"]').html(value.residue + " " + GetNamebyEnum("Currency", value.currency));
            ParticipItem.find('[data-sg-participant="Currency"]').html(GetNamebyEnum("Currency", value.currency));

            ParticipItem.find('[data-sg-participant="Task"]').html(value.task);
            ParticipItem.find('[data-sg-participant="WorkPeriod"]').html(value.workPeriod.split('T')[0]);

            //кнопки удалить и редактировать
            let editBtn = ParticipItem.find('a i.fa-pencil').parent();
            var delteBtn = ParticipItem.find('a i.fa-trash').parent();

            editBtn.click(function () {
                CleanModal();
                let url = "/Participants/EditParticipant";

                modalWindow.find('[data-sg-participant="Id"]').val(value.id);
                let selectUser = modalWindow.find('[data-sg-participant="User"]');
                selectUser.find('option[value = ' + value.userId + '] ').attr("selected", "selected");
                //modalWindow.find('[data-sg-participant="FIO"] option[value="' + value.userId + '"] ').attr("selected","selected");
                modalWindow.find('[data-sg-participant="WorkSum"]').val(value.workSum);
                modalWindow.find('[data-sg-participant="Currency"] :nth-child( ' + (value.currency + 1) + ')').attr("selected", "selected");

                modalWindow.find('[data-sg-participant="Ptask"]').val(value.task);

                
                modalWindow.find('[data-sg-participant="WorkPeriod"]').val(value.workPeriod.split('T')[0]);

                modalWindow.find('.modal-title').html('Редактирование участника');

                SubmitModalBtn.click(function () {
                    SubmitModal(url);
                });

                modalWindow.modal('show');
            });

            delteBtn.click(function () {
                let id = ParticipItem.find('[data-sg-participant="partic-Id"]').val();
                let url = "/Participants/DeleteParticipant?id=" + id + "&rootId=" + RootId + "&rootType=" + RootType;
                $.post(url, function (data) {
                    AppendLoadPartic(data);
                });
            });


            ParticipItem.show();
            ParticipItem.prependTo(ParticList);
        });
        ParticList.animate({ scrollTop: 0 }, "fast");
    }
    Load();
    GetTasks();
}