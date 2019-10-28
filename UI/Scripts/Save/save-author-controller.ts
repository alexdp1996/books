class SaveAuthorController extends SaveBaseController {
    constructor(url: string, alertController: AlertController, DT: IReloadable) {
        super(url, alertController, DT);
    }

    getModel(): BaseVM {
        let author : AuthorBaseVM = {
            Id: $("#Id").val() as any,
            Name: $("#Name").val() as string,
            Surname: $("#Surname").val() as string
        };
        return author;
    }
}