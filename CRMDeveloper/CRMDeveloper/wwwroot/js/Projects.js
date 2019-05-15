$(document).ready(function () {
    InitProjects($('[data-sg-project="project-main"]'));
});

function InitProjects(mainItem) {

    //EnumUtils.js
   // var GetNamebyEnum = GetNamebyEnum;

    var RootId = mainItem.find('[data-sg-project="project-rooId"]').val();

    var RootType = mainItem.find('[data-sg-project="project-RootType"]').val();

    var ProjectList = mainItem.find('[data-sg-project="project-list"]');

    var AddProjectBtn = $('#ProjectTitle').find('.fa-plus-square').parent();

    var modalWindow = $('#Add-project');

    var SubmitModalBtn = $('#save-project');

    function CleanModal() {
        modalWindow.find('[data-sg-project="Id"]').val('');
        modalWindow.find('[data-sg-project="Title"]').val('');
        modalWindow.find('[data-sg-project="Cost"]').val('');

        //modalWindow.find('[data-sg-project="Currency"]').val('');
        modalWindow.find('[data-sg-project="Currency"]').prop('selectedIndex', 0);  
        modalWindow.find('[data-sg-project="Currency"] option[selected="selected"]').removeAttr('selected');
        

        modalWindow.find('[data-sg-project="Description"]').val('');
        modalWindow.find('[data-sg-project="DeadLine"]').val('');

        modalWindow.find('[data-sg-project="ProjectType"]').prop('selectedIndex', 0);
        modalWindow.find('[data-sg-project="ProjectType"]').find('option:selected').removeAttr('selected');

        modalWindow.find('[data-sg-project="Manager"]').val('');

        modalWindow.find('[data-sg-participant="Status"]').prop('selectedIndex', 0);
        modalWindow.find('[data-sg-participant="Status"]').find('option:selected').removeAttr('selected');
    }

    function SubmitModal(url) {
        var data = new Object();
        data.RootId = RootId;
        data.RootType = RootType;

        data.Id = modalWindow.find('[data-sg-project="Id"]').val();
        data.Title = modalWindow.find('[data-sg-project="Title"]').val();
        data.Cost = modalWindow.find('[data-sg-project="Cost"]').val();
        data.Currency = modalWindow.find('[data-sg-project="Currency"]').val();
        data.Description = modalWindow.find('[data-sg-project="Description"]').val();
        data.DeadLine = modalWindow.find('[data-sg-project="DeadLine"]').val();
        data.ProjectTypeId = modalWindow.find('[data-sg-project="ProjectType"]').val();
        data.UserId = modalWindow.find('[data-sg-project="Manager"]').val();
        data.StatusId = modalWindow.find('[data-sg-participant="Status"]').val();

        console.log(data.Title);

        modalWindow.modal('hide');

        SubmitModalBtn.unbind('click');

        $.ajax({
            type: "POST",
            url: url,
            data: data,
            error: function () {
                toastr.error("Ошибка сохранения проекта", "Проект");
            },
            success: function (answer) {
                if (answer.success === false)
                    toastr.error(answer.errorMessage, "Проекты");
                else {
                    toastr.success("Проект сохранен");
                    AppendLoadProjects(answer);
                }

            }
        });
    }

    AddProjectBtn.click(function () {
        CleanModal();
        modalWindow.find('.modal-title').html('Добавление проекта');
        let url = "/Projects/AddProject";
        SubmitModalBtn.click(function () {
            SubmitModal(url);
        });
        modalWindow.modal('show');
    });

    

    function Load() {
        $.ajax({
            url: "/projects/GetProjectList?rootId=" + RootId + "&rootType=" + RootType,
            error: function () { toastr.error("Ошибка запроса", "Проекты"); },
            success: function (data) {
                if (data.success === false)
                    toastr.error(data.errorMessage, "Ошибка получения списка проектов");
                else {
                    AppendLoadProjects(data);
                }
            }
        });
    }

    function AppendLoadProjects(data) {
        if ($('div.project-item').length > 0) {
            var ProjectItem = mainItem.find('[data-sg-project="project-item-first"]').clone();
            $('div.project-item').remove();
            ProjectItem.prependTo(ProjectList);
        }

        console.log(data.length);

        $.each(data.list, function (index, value) {
            //заполняем Модуль
            let ProjectItem = mainItem.find('[data-sg-project="project-item-first"]').clone();
            ProjectItem.attr('data-sg-project', 'project-item');
            ProjectItem.find('[data-sg-project="project-Id"]').val(value.id);
            ProjectItem.find('[data-sg-project="Title"]').html('<a href="' + '/Projects/Details/?id=' + value.id+'">' + value.title + '</a>');
            ProjectItem.find('[data-sg-project="Status"]').html(GetNamebyEnum("ProjectStatus",value.status));
            ProjectItem.find('[data-sg-project="Cost"]').html(value.cost);
            ProjectItem.find('[data-sg-project="Currency"]').html(GetNamebyEnum("Currency", value.currency));
            ProjectItem.find('[data-sg-project="Description"]').html(value.description);
            ProjectItem.find('[data-sg-project="DeadLine"]').html("Деадлайн : " +  value.deadLine.split('T')[0]);
            ProjectItem.find('[data-sg-project="Status"]').html(value.statusName);
            ProjectItem.find('[data-sg-project="Residue"]').html(value.residue);
            ProjectItem.find('[data-sg-project="CompleteProcent"]').width(value.competeProcent + '%');
            ProjectItem.find('[data-sg-project="CompleteProcentNumber"]').html("Завершено на : " + value.competeProcent + '%');

            ProjectItem.find('[data-sg-project="Status"]').css("background-color", value.statusColor);
            
            if (value.participants.length > 0) {
                //Проставляем участников проекта
                var participantsUL = ProjectItem.find('.project-list-participants-none');
                participantsUL.id = value.id;
                participantsUL.attr('class', 'project-list-participants');

                $.each(value.participants, function (i, Pvalue) {
                    var participLI = participantsUL.find('[data-sg-project="project-part-li-first"]').clone();
                    participLI.attr('data-sg-project', 'project-part-li');
                    participLI.find('img').attr('src', '/Users/Avatar/' + Pvalue.userId).show();
                    participLI.find('img').attr('data-content', Pvalue.fio + " (" + Pvalue.task + ") - " + Pvalue.workSum + " " + GetNamebyEnum("Currency", Pvalue.currency)
                        + " (выплачено: " + Pvalue.residue + " " + GetNamebyEnum("Currency", Pvalue.currency) + ")");
                    participLI.prependTo(participantsUL);
                    participLI.attr('style', '');
                });
            }
            
            

            

            ProjectItem.show();
            ProjectItem.prependTo(ProjectList);
        });
        ProjectList.animate({ scrollTop: 0 }, "fast");
    }
    Load();
}