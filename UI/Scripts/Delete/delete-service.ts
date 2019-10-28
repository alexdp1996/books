class DeleteService {
    private url: string;

    constructor(url: string) {
        this.url = url;
    }

    public confirm(Id: number, callback : (alert : AlertVM) => void) : void {
        $.ajax({
            type: "POST",
            url: this.url,
            data: {
                id : Id
            }
        }).done(callback);
    }
}