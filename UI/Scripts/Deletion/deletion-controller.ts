declare var Alert: AlertController;
declare var DT: IReloadable;

class DeletionController {
    private service: DeletionService;

    constructor(url: string) {
        this.service = new DeletionService(url);
        this.bind();
    }

    private bind() {
        let self: DeletionController = this;
        $("#delete").click(function () {
            let id: number = +$("#Id").val();
            self.service.confirm(id, function (alert: AlertVM) {
                DT.reload();
                Alert.show("#alert-box", alert);
            });
        });
    }
}