class AuthorDataTableController implements IReloadable {
    private urls: DataTableUrlsVM;  
    private markupTemplate: string;
    private grid: any;
    private popupController: PopupController;
    private gridSelector: string;

    constructor(urls: DataTableUrlsVM) {
        this.urls = urls;
        this.gridSelector = "#authors";
        this.popupController = new PopupController();
        this.markupTemplate = `<'row'<'col-md-12'<'pull-left'l><'#add.pull-right'>>>
                               <'row'<'col-md-12'tr>>
                               <'row'<'col-md-12'<'pull-left'i><'pull-right'p>>>`;
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
                    "name": "Name",
                    "data": function (record: DetailedAuthorVM) {
                        return '<a class="author-get" href="#" data-id="' + record.Id + '">' + record.Name + '</a>';
                    }
                },
                {
                    "name": "Surname",
                    "data": function (record: DetailedAuthorVM) {
                        return record.Surname;
                    }
                },
                {
                    "name": "CountOfBooks",
                    "data": function (record: DetailedAuthorVM) {
                        return record.CountOfBooks;
                    }
                },
                {
                    "data": function(record: DetailedAuthorVM) {
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

    protected applyAddButton(): void {
        let addTemplate = $("#add-template");
        let addContainer = $("#add");
        addContainer.html(addTemplate.html());
        addTemplate.remove();
    }

    public reload(): void {
        this.grid.ajax.reload();
    }
}