declare var Alert: AlertController;
declare var DT: IReloadable;

abstract class SaveBaseController {
    private saveBusiness: SaveBusiness;

    constructor(url: string) {
        this.saveBusiness = new SaveBusiness(url);
        this.init();
    }

    private onError(alert: AlertVM) {
        Alert.show("#popup-alert-box", alert);
    }

    private onSuccess(alert: AlertVM) {
        DT.reload();
        Alert.show("#alert-box", alert);
        $("#popup").modal("hide");
    }

    abstract getModel(): BaseVM;

    private init() {
        let self = this;
        $("#popup form").off('submit').on('submit', function (e) {
            e.preventDefault();
            let model: BaseVM = self.getModel();
            self.saveBusiness.Save(model,
                function (alert) { self.onSuccess(alert); },
                function (alert) { self.onError(alert); });
        });
    }
}