class BaseEntityBusiness {
    private service: BaseEntityService;

    constructor(service: BaseEntityService) {
        this.service = service;
    }

    public Save(model: BaseVM, onSuccess: Action<AlertVM>, onError: Action<AlertVM>) {
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