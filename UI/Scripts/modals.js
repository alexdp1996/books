
var idToDelete;

function showInfo(info) {
    $('#text-info').text(info);
    $('#operation-info').modal('show');
}

$(document).ready(function () {
    $('#confirm-delete').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        idToDelete = button.data('id');
    });
});

function confirmDelete() {
    $.ajax({
        type: "POST",
        url: deleteUrl + '?id=' + idToDelete
    }).done(function () {
        grid && grid.ajax.reload();
        showInfo('Successfuly deleted');
    }).fail(function () {
        showInfo('Failed to delete');
    });
}