$(document).ready(function () {
    $('[data-seven-comment="comment-main"]').each(function () {
        initSevenCommentItem($(this));
    });
});


function initSevenCommentItem(thisItem) {

    var RootIdVal = thisItem.find('[data-seven-comment="comment-rootId"]');
    var RootType = thisItem.find('[data-seven-comment="comment-RootType"]');

    var CommentList = thisItem.find('[data-seven-comment="comment-list"]');
    var CommentNewForm = thisItem.find('[data-seven-comment="comment-new-form"]');
    var LastId = thisItem.find('[data-seven-comment="comment-lastId"]');

    var CommentNewFormText = thisItem.find('[data-seven-comment="comment-new-form-text"]');
    var CommentNewFormBtn = thisItem.find('[data-seven-comment="comment-new-form-btn"]');

    CommentNewFormBtn.on('click', function (e) {

        var url = "/comment/addcomment";
        var data = new Object();
        data.Text = CommentNewFormText.val();
        data.RootId = RootIdVal.val();
        data.RootType = RootType.val();
        data.lastId = LastId.val();

        $.post(url, data, function (result) {
            AppendLoadItems(result);
        });

        CommentNewFormText.val("");
    });

    function Load() {
        var url = "/comment/getcomments?lastId=" + LastId.val() + "&rootId=" + RootIdVal.val() + "&rootType=" + RootType.val();

        $.get(url, function (data) {
            AppendLoadItems(data);
        });

    }

    function AppendLoadItems(data) {
        if ($('div.comment-item').length > 1) {
            let CommentItem = thisItem.find('[data-seven-comment="comment-item-first"]').clone();
            $('div.comment-item').remove();
            CommentItem.prependTo(CommentList);
        }

        $.each(data.items, function (index, value) {

            var CommentItem = thisItem.find('[data-seven-comment="comment-item-first"]').clone();
            CommentItem.attr('data-seven-comment', "comment-item");
            CommentItem.find('[data-seven-comment="comment-item-date"]').html(value.createStr);
            CommentItem.find('[data-seven-comment="comment-item-user"]').html(value.createdName);
            CommentItem.find('[data-seven-comment="comment-item-text"]').html(value.text);
            CommentItem.find('[data-seven-comment="comment-item-img"]').attr("src", '/Users/Avatar/' + value.createdId);
            CommentItem.show();
            CommentItem.prependTo(CommentList);

        });
        CommentList.animate({ scrollTop: 0 }, "fast");
        LastId.val(data.lastId);
    }

    Load();
}


