class AuthorController {
    private grid: any;
    private gridSelector: string;
    private service: AuthorService;
    private business: AuthorBusiness;

    constructor(urls: EntityUrlsVM) {
        this.service = new AuthorService(urls);
        this.business = new AuthorBusiness(this.service);
    }

    public initDataTable(dataUrl: string): void {
        this.gridSelector = "#authors";

        let self = this;
        self.grid = $(self.gridSelector).DataTable({
            "drawCallback": function () {
                self.rebindTriggers();
            },
            dom: `<'row'<'col-md-12'<'pull-left'l><'#add.pull-right'>>>
                  <'row'<'col-md-12'tr>>
                  <'row'<'col-md-12'<'pull-left'i><'pull-right'p>>>`,
            serverSide: true,
            "searching": false,
            "scrollX": true,
            responsive: true,
            ajax: {
                url: dataUrl,
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
                    "data": function (record: DetailedAuthorVM) {
                        return '<button class="btn btn-danger author-delete" data-id="' + record.Id + '">Delete</button>';
                    }
                }
            ],
            columnDefs: [{ orderable: false, targets: [3] }],
            "orderMulti": false
        });

        let addTemplate = $("#add-template");
        let addContainer = $("#add");
        addContainer.html(addTemplate.html());
        addTemplate.remove();

        $(".author-create").off('click').click(function () {
            self.get(null, function () {
                self.initCreate();
            });
        });
    }

    private rebindTriggers(): void {
        let self = this;
        $(".author-get").off('click').click(function () {
            let id = $(this).data('id');
            self.get(id, function () {
                self.initUpdate(function () {
                    self.reloadDataTable();
                });
            });
        });

        $(".author-delete").off('click').click(function () {
            let id = $(this).data('id');
            let html = `
                <div class="modal-header">
                    <h5 class="modal-title">Confirm delete</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <input type="hidden" id="Id" value="${id}" />
                    <p>Are you sure you want to delete this?</p>
                </div>
                <div class="modal-footer">
                    <button id="delete" class="btn btn-danger" data-dismiss="modal">Yes</button>
                    <button class="btn btn-default" data-dismiss="modal">No</button>
                </div>
            `;

            self.renderPopup(html);
            self.initDelete();
        });
    };

    private reloadDataTable(): void {
        this.grid.ajax.reload();
    }

    private showAlert(selector: string, model: AlertVM): void {
        let options = AlertBusiness.getOptions(model.Type);
        let html = `
            <div class="alert ${options.AlertClass} alert-dismissible fade in">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong>${options.MessageType}</strong> ${model.Message}
                </div>
        `;
        $(selector).html(html);
    }

    private onSaveError(alert: AlertVM) {
        this.showAlert("#popup-alert-box", alert);
    }

    private onSaveSuccess(alert: AlertVM) {
        this.showAlert("#alert-box", alert);
        $("#popup").modal("hide");
    }

    public create() {
        let self = this;
        let author: AuthorVM = {
            Name: $("#Name").val() as string,
            Surname: $("#Surname").val() as string
        };
        self.business.create(author,
            function (alert) { self.onSaveSuccess(alert); self.reloadDataTable(); },
            function (alert) { self.onSaveError(alert); });
    }

    public update(callback: Action<void>) {
        let self = this;
        let author: AuthorVM = {
            Id: $("#Id").val() as number,
            Name: $("#Name").val() as string,
            Surname: $("#Surname").val() as string
        };
        self.business.update(author,
            function (alert) { self.onSaveSuccess(alert); callback(); },
            function (alert) { self.onSaveError(alert); });
    }

    public get(id: number | null, callback: Action<void>): void {
        let self = this;
        this.service.get(id, function (html) { self.renderPopup(html); callback(); });
    }

    public delete() {
        let self = this;
        let id: number = +$("#Id").val();
        this.service.delete(id, function (alert: AlertVM) {
            this.showAlert("#alert-box", alert);
            self.reloadDataTable();
        });
    }

    private renderPopup(html: string): void {
        let popup = $("#popup > .modal-dialog > .modal-content");
        popup.html(html);
        $("#popup").modal('show');
        Layout.disableAutocomplete();
    }

    public initCreate() {
        let self = this;
        $("#popup form").off('submit').on('submit', function (e) {
            e.preventDefault();
            self.create();
        });
    }

    public initUpdate(callback: Action<void>) {
        let self = this;
        $("#popup form").off('submit').on('submit', function (e) {
            e.preventDefault();
            self.update(callback);
        });
    }

    public initDelete() {
        let self = this;
        $("#delete").click(function () {
            self.delete();
        });
    }
}