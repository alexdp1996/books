class BaseEntityService {
    private urls: EntityUrlsVM;

    constructor(urls: EntityUrlsVM) {
        this.urls = urls;
    }

    public Save(model: BaseVM, success: Action<AlertVM>) {
        let self = this;
        $.ajax({
            url: self.urls.save,
            type: "POST",
            data: {
                model: model
            }
        }).done(success);
    }

    public Delete(Id: number, callback: (alert: AlertVM) => void): void {
        let self = this;
        $.ajax({
            type: "POST",
            url: self.urls.delete,
            data: {
                id: Id
            }
        }).done(callback);
    }
}