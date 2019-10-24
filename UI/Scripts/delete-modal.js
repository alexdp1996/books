var DeleteModal = DeleteModal || {};

(function () {
    var self = this;
    self.url = "";

    self.init = function (callback) {
        self.callback = callback;
    };

    self.ConfirmDelete = function () {
        let id = $("#Id").val();
        $.ajax({
            type: "POST",
            url: self.url + '?id=' + id
        }).done(function (data) {
            self.callback && self.callback();
            Alert.show("#alert-box", data);
        });
    };

    $("#delete").click(function () {
        DeleteModal.ConfirmDelete();
    });
}).apply(DeleteModal);