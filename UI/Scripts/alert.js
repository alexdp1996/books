var Alert = Alert || {};

(function () {

    var self = this;

    self.Type = {
        Success: 0,
        Info: 1,
        Warning: 2,
        Danger: 3
    };

    self.getOptionsVM = function (alertClass, messageType) {
        var inner = this;
        inner.alertClass = alertClass;
        inner.messageType = messageType;
        return inner;
    };

    self.getOptions = function (alertType) {
        switch (alertType) {
            case self.Type.Success: return self.getOptionsVM("alert-success", "Success!");
            case self.Type.Info: return self.getOptionsVM("alert-info", "Info!");
            case self.Type.Warning: return self.getOptionsVM("alert-warning", "Warning!");
            case self.Type.Danger: return self.getOptionsVM("alert-danger", "Danger!");
        }
    };

    self.show = function (selector, alert) {

        let options = self.getOptions(alert.Type);
        let html = `
            <div class="alert ${options.alertClass} alert-dismissible fade in">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong>${options.messageType}</strong> ${alert.Message}
                </div>
        `;
        $(selector).html(html);
    };

}).apply(Alert);