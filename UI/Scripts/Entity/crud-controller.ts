declare var Alert: AlertController;
declare var DT: IReloadable;

abstract class CRUDController {
    private service: CRUDService;
    private business: CRUDBusiness;

    constructor(urls: EntityUrlsVM) {
        this.service = new CRUDService(urls);
        this.business = new CRUDBusiness(this.service);
    }

    private onSaveError(alert: AlertVM) {
        Alert.show("#popup-alert-box", alert);
    }

    private onSaveSuccess(alert: AlertVM) {
        DT.reload();
        Alert.show("#alert-box", alert);
        $("#popup").modal("hide");
    }

    abstract getModel(): any;

    public bindSave() {
        let self = this;
        $("#popup form").off('submit').on('submit', function (e) {
            e.preventDefault();
            let model: any = self.getModel();
            self.business.Save(model,
                function (alert) { self.onSaveSuccess(alert); },
                function (alert) { self.onSaveError(alert); });
        });
    }

    public bindDelete() {
        let self = this;
        $("#delete").click(function () {
            let id: number = +$("#Id").val();
            self.service.Delete(id, function (alert: AlertVM) {
                DT.reload();
                Alert.show("#alert-box", alert);
            });
        });
    }
}