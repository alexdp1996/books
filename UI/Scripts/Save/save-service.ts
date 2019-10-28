class SaveService {
    private url: string;

    constructor(url: string) {
        this.url = url;
    }

    public Save(model: BaseVM, success: Action<AlertVM>) {
        let self = this;
        $.ajax({
            url: self.url,
            type: "POST",
            data: {
                model: model
            }
        }).done(success);
    }
}