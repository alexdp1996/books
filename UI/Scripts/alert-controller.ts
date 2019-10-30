class AlertController {

    private getOptionsVM(alertClass: string, messageType: string) {
        let vm: any = {};
        vm.alertClass = alertClass;
        vm.messageType = messageType;
        return vm;
    };

    public show(selector: string, model: AlertVM): void {
        let options = this.getOptions(model.Type);
        let html = `
            <div class="alert ${options.alertClass} alert-dismissible fade in">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong>${options.messageType}</strong> ${model.Message}
                </div>
        `;
        $(selector).html(html);
    }

    private getOptions(type: AlertType) {
        switch (type) {
            case AlertType.Success: return this.getOptionsVM("alert-success", "Success!");
            case AlertType.Info: return this.getOptionsVM("alert-info", "Info!");
            case AlertType.Warning: return this.getOptionsVM("alert-warning", "Warning!");
            case AlertType.Danger: return this.getOptionsVM("alert-danger", "Danger!");
        }
    }
}

var Alert = new AlertController();