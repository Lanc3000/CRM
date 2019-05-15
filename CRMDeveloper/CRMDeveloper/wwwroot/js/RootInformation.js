$(document).ready(function () {
    InitInformationForm($('#EntityInformation'));
});

function InitInformationForm(InformationBlock) {

    var editBtn = InformationBlock.find('.ibox-tools .fa-pencil').parent();

    var deleteBtn = InformationBlock.find('.ibox-tools .fa-trash').parent();

    var saveBtn = InformationBlock.find('.ibox-tools .fa-save').parent();

    var cancelBtn = InformationBlock.find('.ibox-tools .fa-chevron-right').parent();

    var EntityForm = $('#EntityForm');

    //Test
    var FormData = EntityForm.find('[disabled="disabled"]');

    var PreviusData = [];
    PreviusData[0] = [];
    PreviusData[1] = [];
    $.each(FormData, function (index, value) {
        PreviusData[0][index] = value.id;
        PreviusData[1][index] = value.value;
    })

    //var PClient = EntityForm.serializeArray();

    editBtn.click(function () {
        FormData.attr('disabled', false);
        editBtn.hide();
        deleteBtn.hide();

        cancelBtn.show();
        saveBtn.show();
    });

    deleteBtn.click(function () {
        $('#DeleteConfirm').modal('show');
    });

    saveBtn.click(function () {
        EntityForm.submit();
    });

    cancelBtn.click(function () {
        editBtn.show();
        deleteBtn.show();
        FormData.attr('disabled', true);
        cancelBtn.hide();
        saveBtn.hide();

        for (let j = 0; j < PreviusData[0].length; j++) {
            EntityForm.find('#' + PreviusData[0][j]).val(PreviusData[1][j]);
        }
      
    });
}
