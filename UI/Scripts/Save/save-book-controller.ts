class SaveBookController extends SaveBaseController {
    constructor(url: string) {
        super(url);
    }

    getModel(): BaseVM {
        let book: BookEditVM = {
            Id: $("#Id").val() as any,
            Name: $("#Name").val() as string,
            Rate: +$("#Rate").val(),
            Date: ($("#Date").val() as any) as Date,
            Pages: +$("#Pages").val(),
            AuthorIds: ($("#AuthorIds").val() as any) as number[]
        };
        return book;
    }
}