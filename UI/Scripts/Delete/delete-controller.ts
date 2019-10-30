declare var Alert: AlertController;
declare var DT: IReloadable;

class DeleteController {
    private service: DeleteService;

    constructor(url: string) {
        this.service = new DeleteService(url);
        this.bind();
    }

    private bind() {
        let self: DeleteController = this;
        $("#delete").click(function () {
            let id: number = +$("#Id").val();
            self.service.confirm(id, function (alert: AlertVM) {
                DT.reload();
                Alert.show("#alert-box", alert);
            });
        });
    }
}