class SaveAuthorController extends SaveBaseController {
    constructor(url: string) {
        super(url);
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