abstract class SaveBaseController {
    private DT: IReloadable;
    private alertController: AlertController;
    private saveBusiness: SaveBusiness;

    constructor(url: string, alertController: AlertController, DT: IReloadable) {
        this.DT = DT;
        this.alertController = alertController;
        this.saveBusiness = new SaveBusiness(url);
        this.init();
    }

    private onError(alert: AlertVM) {
        this.alertController.show("#popup-alert-box", alert);
    }

    private onSuccess(alert: AlertVM) {
        this.DT.reload();
        this.alertController.show("#alert-box", alert);
        $("#popup").modal("hide");
    }

    abstract getModel(): BaseVM;

    private init() {
        let self = this;
        $("#popup form").on('submit', function (e) {
            e.preventDefault();
            let model: BaseVM = self.getModel();
            self.saveBusiness.Save(model,
                function (alert) { self.onSuccess(alert); },
                function (alert) { self.onError(alert); });
        });
    }
}