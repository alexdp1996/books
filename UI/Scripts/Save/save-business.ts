class SaveBusiness {
    private saveService: SaveService;

    constructor(url: string) {
        this.saveService = new SaveService(url);
    }

    public Save(model: BaseVM, onSuccess: Action<AlertVM>, onError: Action<AlertVM>) {
        let processResult: Action<AlertVM> = function (alert: AlertVM) {
            if (alert.Type === AlertType.Success) {
                onSuccess(alert);
            } else {
                onError(alert);
            }
        };
        this.saveService.Save(model, processResult);
    }
}