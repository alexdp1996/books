declare var ProcessedResult;
declare var moment;

class BookController {
    private authorsUrl: string;
    private grid: any;
    private service: BookService;
    private business: BookBusiness;

    constructor(urls: BookUrlsVM) {
        this.service = new BookService(urls);
        this.business = new BookBusiness(this.service);
        this.authorsUrl = urls.authors;
    }

    public initDataTable(dataUrl: string): void {
        let self = this;
        this.grid = $("#books").DataTable({
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
                    "data": function (record: DetailedBookVM) {
                        return '<a class="book-get" href="#" data-id="' + record.Id + '">' + record.Name + '</a>';
                    }
                },
                {
                    "name": "Pages",
                    "data": function (record: DetailedBookVM) {
                        return record.Pages;
                    }
                },
                {
                    "name": "Rate",
                    "data": function (data) {
                        return data.Rate;
                    }
                },
                {
                    "name": "CreatedDate",
                    "data": function (record: DetailedBookVM) {
                        let date = moment(record.CreatedDate).format('LL');
                        return date;
                    }
                },
                {
                    "name": "Authors",
                    "data": function (record: DetailedBookVM) {
                        let authors = record.Authors;
                        if (!authors.length) {
                            return '';
                        } else {
                            let result = [];
                            for (let i = 0; i < authors.length; ++i) {
                                result.push('<span data-id="' + authors[i].Id + '" >' + authors[i].Name + ' ' + authors[i].Surname + '</span>');
                            }
                            return result.join(', ');
                        }
                    }
                },
                {
                    "data": function (record: DetailedBookVM) {
                        return '<button class="btn btn-danger book-delete" data-id="' + record.Id + '">Delete</button>';
                    }
                }
            ],
            columnDefs: [{ orderable: false, targets: [4, 5] }],
            "orderMulti": false
        });

        let addTemplate = $("#add-template");
        let addContainer = $("#add");
        addContainer.html(addTemplate.html());
        addTemplate.remove();

        $(".book-create").off('click').click(function () {
            self.get(null, function () {
                self.initPlugins();
                $("#popup form").off('submit').on('submit', function (e) {
                    e.preventDefault();
                    self.create();
                });
            });
        });
    }

    private rebindTriggers(): void {
        let self = this;
        $(".book-get").off('click').click(function () {
            let id = $(this).data('id');
            self.get(id, function () {
                self.initPlugins();
                $("#popup form").off('submit').on('submit', function (e) {
                    e.preventDefault();
                    self.update();
                });
            });
        });

        $(".book-delete").off('click').click(function () {
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
            $("#delete").click(function () {
                self.delete();
            });
        });
    };

    private initPlugins() {
        let self = this;
        $("#AuthorIds").select2({
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

    private renderPopup(html: string): void {
        let popup = $("#popup > .modal-dialog > .modal-content");
        popup.html(html);
        $("#popup").modal('show');
        Layout.disableAutocomplete();
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
        this.grid.ajax.reload();
    }

    private create() {
        let self = this;
        let book: EditedBookVM = {
            Name: $("#Name").val() as string,
            Rate: +$("#Rate").val(),
            CreatedDate: ($("#CreatedDate").val() as any) as Date,
            Pages: +$("#Pages").val(),
            AuthorIds: ($("#AuthorIds").val() as any) as number[]
        };
        self.business.create(book,
            function (alert) { self.onSaveSuccess(alert); },
            function (alert) { self.onSaveError(alert); });
    }

    private update() {
        let self = this;
        let book: EditedBookVM = {
            Id: $("#Id").val() as number,
            Name: $("#Name").val() as string,
            Rate: +$("#Rate").val(),
            CreatedDate: ($("#CreatedDate").val() as any) as Date,
            Pages: +$("#Pages").val(),
            AuthorIds: ($("#AuthorIds").val() as any) as number[]
        };
        self.business.update(book,
            function (alert) { self.onSaveSuccess(alert); },
            function (alert) { self.onSaveError(alert); });
    }

    private get(id: number | null, callback: Action<void>): void {
        let self = this;
        this.service.get(id, function (html) { self.renderPopup(html); callback(); });
    }

    private delete() {
        let self = this;
        let id: number = +$("#Id").val();
        this.service.delete(id, function (alert: AlertVM) {
            this.showAlert("#alert-box", alert);
            self.grid.ajax.reload();
        });
    }
}