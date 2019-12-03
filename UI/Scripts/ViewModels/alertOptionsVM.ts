class AlertOptionsVM {
    public AlertClass: string;
    public MessageType: string;

    constructor(alertClass: string, messageType: string) {
        this.AlertClass = alertClass;
        this.MessageType = messageType;
    }
}