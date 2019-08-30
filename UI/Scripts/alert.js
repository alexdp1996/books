var Alert = Alert || {};

(function () {

    var self = this;
    self.container = "#alert-box";

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

    self.show = function (message, alertType) {
        let options = self.getOptions(alertType);
        let html = `
            <div class="alert ${options.alertClass} alert-dismissible fade in">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong>${options.messageType}</strong> ${message}
                </div>
        `;
        $(self.container).html(html);
    };

}).apply(Alert);