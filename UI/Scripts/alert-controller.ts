class AlertController {
    public showSuccess(message : string) {
        this.show(message, "#alert-success-div");
    }

    public showError(message : string) {
        this.show(message, "#alert-danger-div");
    }

    private show(message: string, popupSelector: string): void {
        let messageSelector = popupSelector + '>.alert-message';
        $(messageSelector).html(message);
        $(popupSelector).show();
    }
}

var Alert = new AlertController();