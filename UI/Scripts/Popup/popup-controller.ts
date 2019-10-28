class PopupController {
    private service: PopupService;

    constructor() {
        this.service = new PopupService();
    }

    public bind(selector: string, url: string): void {
        let self = this;
        $(selector).click(function () {
            let model = new PopupVM();
            model.Url = url;
            model.Id = $(this).data('id');
            self.getPopup(model);
        });
    }

    public getPopup(model: PopupVM): void {
        this.service.getPopup(model, this.render);
    }

    private render(html: string): void {
        let popup = $("#popup > .modal-dialog > .modal-content");
        popup.html(html);
        $("#popup").modal('show');
    }
}