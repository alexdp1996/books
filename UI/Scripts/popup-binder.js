var PopupBinder = PopupBinder || {};

(function () {
    var self = this;

    self.rebindTrigger = function (triggerSelector, popupContentSelector, url) {
        $(triggerSelector).click(function () {
            let id = $(this).data('id');

            $.ajax({
                url: url,
                type: "GET",
                data: {
                    id: id
                },
                success: function (data) {
                    let popup = $(popupContentSelector);
                    popup.html(data);
                }
            });
        });
    };
}).apply(PopupBinder);