class AuthorController extends BaseEntityController {
    constructor(urls: EntityUrlsVM) {
        super(urls);
    }

    getModel(): BaseVM {
        let author : AuthorVM = {
            Id: $("#Id").val() as any,
            Name: $("#Name").val() as string,
            Surname: $("#Surname").val() as string
        };
        return author;
    }
}