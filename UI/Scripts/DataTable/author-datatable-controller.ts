class AuthorDataTableController extends BaseDataTableController implements IReloadable {
    private urls: EntityUrlsVM;
    private popupController: PopupController;
    private grid: any;
    private gridSelector: string;


    constructor(urls: EntityUrlsVM) {
        super();
        this.urls = urls;
        this.popupController = new PopupController();
        this.gridSelector = "#authors";
        this.initDT();
    }

    private rebindTriggers(): void {
        this.popupController.bind(".author-get", this.urls.get);
        this.popupController.bind(".author-delete", this.urls.delete);
    };

    private initDT(): void {
        let self = this;
        self.grid = $(self.gridSelector).DataTable({
            "drawCallback": function () {
                self.rebindTriggers();
            },
            dom: self.markupTemplate,
            serverSide: true,
            "searching": false,
            "scrollX": true,
            responsive: true,
            ajax: {
                url: self.urls.dtData,
                type: "POST",
                dataType: "json",
                dataSrc: function (json) {
                    return json.data;
                }
            },
            columns: [
                {
                    "data": function (record: AuthorVM) {
                        return '<a class="author-get" href="#" data-id="' + record.Id + '">' + record.Name + '</a>';
                    }
                },
                {
                    "data": function (record: AuthorVM) {
                        return record.Surname;
                    }
                },
                {
                    "data": function (record: AuthorVM) {
                        return record.CountOfBooks;
                    }
                },
                {
                    "data": function (record: AuthorVM) {
                        return '<button class="btn btn-danger author-delete" data-id="' + record.Id + '">Delete</button>';
                    }
                }
            ],
            columnDefs: [{ orderable: false, targets: [3] }],
            "orderMulti": false
        });

        this.applyAddButton();
        this.rebindTriggers();
    }

    public reload(): void {
        this.grid.ajax.reload();
    }
}