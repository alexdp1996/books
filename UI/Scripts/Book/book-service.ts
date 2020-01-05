class BookService {
    private urls: EntityUrlsVM;

    constructor(urls: EntityUrlsVM) {
        this.urls = urls;
    }

    public create(model: EditedBookVM, success: Action<AlertVM>) {
        let self = this;
        $.ajax({
            url: self.urls.create,
            type: "POST",
            data: {
                model: model
            }
        }).done(success);
    }

    public update(model: EditedBookVM, success: Action<AlertVM>) {
        let self = this;
        $.ajax({
            url: self.urls.update,
            type: "POST",
            data: {
                model: model
            }
        }).done(success);
    }

    public delete(id: number, callback: (alert: AlertVM) => void): void {
        let self = this;
        $.ajax({
            type: "POST",
            url: self.urls.delete,
            data: {
                id: id
            }
        }).done(callback);
    }

    public get(id : number|null, callback: (html: string) => void): void {
        let self = this;
        $.ajax({
            url: self.urls.get,
            type: "GET",
            data: {
                id: id
            },
            success: callback
        });
    }
}