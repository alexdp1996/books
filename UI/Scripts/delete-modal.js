var DeleteModal = DeleteModal || {};

(function () {
    var self = this;
    self.url = "";

    self.Init = function (callback) {
        self.callback = callback;
        $('#confirm-delete').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget);
            self.id = button.data('id');
        });
    };

    self.ConfirmDelete = function () {
        $.ajax({
            type: "POST",
            url: self.url + '?id=' + self.id
        }).done(function (data) {
            self.callback && self.callback();
            Alert.show("#alert-box", data);
        });
    };

    $("#delete").click(function () {
        DeleteModal.ConfirmDelete();
    });
}).apply(DeleteModal);