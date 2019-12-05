class AuthorDataTableController extends BaseDataTableController {
    private urls: DataTableUrlsVM;  

    constructor(urls: DataTableUrlsVM) {
        super();
        this.urls = urls;
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
                    "data": function (record: DetailedAuthorVM) {
                        return '<a class="author-get" href="#" data-id="' + record.Id + '">' + record.Name + '</a>';
                    }
                },
                {
                    "data": function (record: DetailedAuthorVM) {
                        return record.Surname;
                    }
                },
                {
                    "data": function (record: DetailedAuthorVM) {
                        return record.CountOfBooks;
                    }
                },
                {
                    "data": function (record: DetailedAuthorVM) {
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
}