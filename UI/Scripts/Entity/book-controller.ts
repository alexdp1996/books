declare var ProcessedResult;

class BookController extends CRUDController {
    private authorsUrl: string;
    private authorsSelector: string;

    constructor(urls: BookUrlsVM) {
        super(urls);
        this.authorsSelector = "#AuthorIds";
        this.authorsUrl = urls.authors;
    }

    getModel(): EditedBookVM {
        let book: EditedBookVM = {
            Id: $("#Id").val() as any,
            Name: $("#Name").val() as string,
            Rate: +$("#Rate").val(),
            Date: ($("#Date").val() as any) as Date,
            Pages: +$("#Pages").val(),
            AuthorIds: ($("#AuthorIds").val() as any) as number[]
        };
        return book;
    }

    public initPlugins() {
        let self = this;
        $(this.authorsSelector).select2({
            multiple: true,
            ajax: {
                url: self.authorsUrl,
                dataType: 'json',
                processResults: self.processSelectData
            }
        });
        $(".datepicker").datepicker();
    }

    processSelectData(data: AuthorVM[]): any {
        return {
            results: $.map(data, (item: AuthorVM) => {
                return {
                    text: item.Name + " " + item.Surname,
                    id: item.Id
                };
            })
        };
    }
}