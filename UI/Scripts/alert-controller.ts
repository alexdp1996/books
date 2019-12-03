class AlertController {
    public show(selector: string, model: AlertVM): void {
        let options = this.getOptions(model.Type);
        let html = `
            <div class="alert ${options.AlertClass} alert-dismissible fade in">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong>${options.MessageType}</strong> ${model.Message}
                </div>
        `;
        $(selector).html(html);
    }

    private getOptions(type: AlertType) {
        switch (type) {
            case AlertType.Success: return new AlertOptionsVM("alert-success", "Success!");
            case AlertType.Info: return new AlertOptionsVM("alert-info", "Info!");
            case AlertType.Warning: return new AlertOptionsVM("alert-warning", "Warning!");
            case AlertType.Danger: return new AlertOptionsVM("alert-danger", "Danger!");
        }
    }
}

var Alert = new AlertController();