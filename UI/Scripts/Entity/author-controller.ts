class AuthorController extends CRUDController {
    constructor(urls: EntityUrlsVM) {
        super(urls);
    }

    getModel(): AuthorVM {
        let author : AuthorVM = {
            Id: $("#Id").val() as any,
            Name: $("#Name").val() as string,
            Surname: $("#Surname").val() as string
        };
        return author;
    }
}