enum AlertType {
    Success = 0,
    Info = 1,
    Warning = 2,
    Danger = 3
}

class AlertVM {
    public Type: AlertType;
    public Message: string;
}
