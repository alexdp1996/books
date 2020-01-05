class BookBusiness {
    private service: BookService;

    constructor(service: BookService) {
        this.service = service;
    }

    public create(model: EditedBookVM, onSuccess: Action<AlertVM>, onError: Action<AlertVM>) {
        let processResult: Action<AlertVM> = function (alert: AlertVM) {
            if (alert.Type === AlertType.Success) {
                onSuccess(alert);
            } else {
                onError(alert);
            }
        };
        this.service.create(model, processResult);
    }

    public update(model: EditedBookVM, onSuccess: Action<AlertVM>, onError: Action<AlertVM>) {
        let processResult: Action<AlertVM> = function (alert: AlertVM) {
            if (alert.Type === AlertType.Success) {
                onSuccess(alert);
            } else {
                onError(alert);
            }
        };
        this.service.update(model, processResult);
    }

    public delete(id: number, callback: (alert: AlertVM) => void): void {
        this.service.delete(id, callback);
    }

    public get(id: number | null, callback: (html: string) => void): void {
        this.service.get(id, callback);
    }
}