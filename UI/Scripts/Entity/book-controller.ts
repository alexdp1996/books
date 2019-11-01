declare var ProcessedResult;

class BookController extends BaseEntityController {
    private authorsUrl: string;
    private authorsSelector: string;

    constructor(saveUrl: string, authorsUrl: string) {
        super(saveUrl);
        this.authorsSelector = "#AuthorIds";
        this.authorsUrl = authorsUrl;
        this.initPlugins();
    }

    getModel(): BaseVM {
        let book: SavableBookVM = {
            Id: $("#Id").val() as any,
            Name: $("#Name").val() as string,
            Rate: +$("#Rate").val(),
            Date: ($("#Date").val() as any) as Date,
            Pages: +$("#Pages").val(),
            AuthorIds: ($("#AuthorIds").val() as any) as number[]
        };
        return book;
    }

    private initPlugins() {
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

    processSelectData(data: AuthorBaseVM[]): any {
        return {
            results: $.map(data, (item: AuthorBaseVM) => {
                return {
                    text: item.Name + " " + item.Surname,
                    id: item.Id
                };
            })
        };
    }
}