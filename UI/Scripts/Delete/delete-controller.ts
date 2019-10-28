class DeleteController {
    private service: DeleteService;
    private alertController: AlertController;

    constructor(url: string, datatable: IReloadable , alertController: AlertController) {
        this.service = new DeleteService(url);
        this.alertController = alertController;
        this.bind(datatable);
    }

    private bind(datatable: IReloadable) {
        let self: DeleteController = this;
        $("#delete").click(function () {
            let id: number = +$("#Id").val();
            self.service.confirm(id, function (alert: AlertVM) {
                datatable.reload();
                self.alertController.show("#alert-box", alert);
            });
        });
    }
}