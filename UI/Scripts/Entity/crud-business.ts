class CRUDBusiness {
    private service: CRUDService;

    constructor(service: CRUDService) {
        this.service = service;
    }

    public Save(model: any, onSuccess: Action<AlertVM>, onError: Action<AlertVM>) {
        let processResult: Action<AlertVM> = function (alert: AlertVM) {
            if (alert.Type === AlertType.Success) {
                onSuccess(alert);
            } else {
                onError(alert);
            }
        };
        this.service.Save(model, processResult);
    }
}