$(document).ready(function () {

    InitFiles($('[data-sg-file="file-main"]'));
});

function InitFiles(mainItem) {
    var RootId = mainItem.find('[data-sg-file="file-rooId"]').val();
    var RootType = mainItem.find('[data-sg-file="file-RootType"]').val();
    var FileRootType = mainItem.find('[data-sg-file="file-FileRootType"]').val();

    var AddFileBtn = mainItem.find('.fa-plus-square').parent();

    var fileList = mainItem.find('[data-sg-file="file-list"]');

    var modalWindow = $('#Add-file');

    AddFileBtn.click(function () {
        modalWindow.modal('show');
    });

    


    function Load() {
        var url = "/File/GetFileList?rootId=" + RootId + '&rootType=' + RootType;

        $.get(url, function (data) {
            AppendLoadFiles(data);
        });
    }

    function AppendLoadFiles(data) {
        if ($('tr.file-item').length > 0) {
            let fileItem = mainItem.find('[data-sg-file="file-item-first"]').clone();
            $('tr.file-item').remove();
            fileItem.prependTo(fileList);
        }
        $.each(data.files, function (index, value) {
            var fileItem = mainItem.find('[data-sg-file="file-item-first"]').clone();
            fileItem.attr('data-sg-file', 'file-item');
            fileItem.find('[data-sg-file="file-Id"]').val(value.id);
            fileItem.find('[data-sg-file="file-item-name"]').html('<a href="/File/DownLoadFile/' + value.id +'">'+ value.originalName+'</a>');

            var exten = value.originalName.split('.').pop().toLowerCase();
            var iconElement = fileItem.find('[data-sg-file="file-item-type-icon"]');
            if (exten === 'pdf') {
                iconElement.html('<i class="fa fa-file-pdf-o"></i>');
            }
            else if (exten === 'doc' || exten === 'docx') {
                iconElement.html('<i class="fa fa-file-word-o"></i>');
            }
            else if (exten === 'xls' || exten === 'xlsx') {
                iconElement.html('<i class="fa fa-file-excel-o"></i>');
            }
            else {
                iconElement.html('<i class="fa fa-file-o"></i>');
            }

            fileItem.find('.fa-times').click(function () {
                let id = fileItem.find('[data-sg-file="file-Id"]').val();
                var url = "/File/DeleteFile?id=" + id + '&rootId=' + RootId +
                    '&rootType=' + RootType;

                $.ajax({
                    method: "POST",
                    url: url,
                    error: function () { toastr.error("Ошибка запроса", "Файлы"); },
                    success: function (data) {
                        if (data.success === false)
                            toastr.error(data.errorMessage, "Файлы");
                        else {
                            toastr.success('Файл удален', 'Файлы');
                            AppendLoadFiles(data);
                        }
                    }
                });
            });

            fileItem.show();
            fileItem.prependTo(fileList);
        });
        fileList.animate({ scrollTop: 0 }, "fast");
    }

    Load();

    $(document).ready(function () {
        Dropzone.autoDiscover = false;
        //Simple Dropzonejs 
        $(".dropzone-me-files").dropzone({
            url: "/File/New?rootId=" + RootId + '&rootType=' + RootType,
            addRemoveLinks: true,
            dictDefaultMessage:'',
            success: function (file, response) {
                Load();
                this.removeFile(file);
            },
            error: function (file, response) {
                file.previewElement.classList.add("dz-error");
            }
        });
    });
}