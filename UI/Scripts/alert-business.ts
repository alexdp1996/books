class AlertBusiness {
    public static getOptions(type: AlertType) {
        switch (type) {
            case AlertType.Success: return new AlertOptionsVM("alert-success", "Success!");
            case AlertType.Info: return new AlertOptionsVM("alert-info", "Info!");
            case AlertType.Warning: return new AlertOptionsVM("alert-warning", "Warning!");
            case AlertType.Danger: return new AlertOptionsVM("alert-danger", "Danger!");
        }
    }
}