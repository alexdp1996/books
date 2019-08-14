var DeleteModal = DeleteModal || {};

(function () {
    var self = this;
    self.url = "";

    self.Init = function () {
        $('#confirm-delete').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget);
            self.id = button.data('id');
        });
    };

    self.ConfirmDelete = function () {
        $.ajax({
            type: "POST",
            url: self.url + '?id=' + self.id
        }).done(function () {
            self.grid && self.grid.ajax.reload();
            showInfo('Successfuly deleted');
        }).fail(function () {
            showInfo('Failed to delete');
        });
    };
}).apply(DeleteModal);