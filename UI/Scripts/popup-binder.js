var PopupBinder = PopupBinder || {};

(function () {
    var self = this;

    self.rebindTrigger = function (triggerSelector, url) {
        $(triggerSelector).click(function () {
            let id = $(this).data('id');

            $.ajax({
                url: url,
                type: "GET",
                data: {
                    id: id
                },
                success: function (data) {
                    let popup = $("#popup > .modal-dialog > .modal-content");
                    popup.html(data);
                    $("#popup").modal('show');
                }
            });
        });
    };
}).apply(PopupBinder);