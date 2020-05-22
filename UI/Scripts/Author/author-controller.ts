declare var Alert: AlertController;

class AuthorController {
    private grid: any;
    private gridSelector: string;
    private service: AuthorService;
    private getListUrl: string;

    constructor(urls: EntityUrlsVM) {
        this.service = new AuthorService(urls);
        this.getListUrl = urls.getList;
    }

    public init() {
        this.initDataTable(this.getListUrl);
        this.initBtn();
    }

    private initBtn() {
        let authorAddBtn = $("#author-create-btn")[0];
        let authorAddBtnHtml = authorAddBtn.outerHTML;
        authorAddBtn.remove();

        let authorAddDiv = $("#authorAddDiv");
        authorAddDiv.html(authorAddBtnHtml);

        let self = this;

        $("#author-create-btn").off('click').click(function () {
            self.authorCreateBtnOnClick();
        });

        $("#author-publish-btn").off('click').click(function () {
            self.service.getPublishForm()
                .then((response) => {
                    self.getForm(response);
                });
        });
    }

    private authorCreateBtnOnClick() {
        let self = this;
        let promise = self.service.get();
        promise.then(function (html) {
            self.getForm(html);
        });
    }

    private initDataTable(dataUrl: string): void {
        this.gridSelector = "#authors";

        let self = this;
        self.grid = $(self.gridSelector).DataTable({
            "drawCallback": function () {
                self.initDataTableCtrls();
            },
            dom: `<'row'<'col-md-12'<'pull-left'l><'#authorAddDiv.pull-right'>>>
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
                    "data": function (record: AuthorVM) {
                        return '<a class="author-get-lnk" href="#" data-id="' + record.Id + '">' + record.Name + '</a>';
                    }
                },
                {
                    "name": "Surname",
                    "data": function (record: AuthorVM) {
                        return record.Surname;
                    }
                },
                {
                    "name": "CountOfBooks",
                    "data": function (record: AuthorVM) {
                        return record.CountOfBooks;
                    }
                },
                {
                    "data": function (record: AuthorVM) {
                        return '<button class="btn btn-danger author-delete-btn" data-id="' + record.Id + '">Delete</button>'
                            + '<button class="btn btn-info author-send-btn" data-id="' + record.Id + '">RabbitMQ</button>';
                    }
                }
            ],
            columnDefs: [{ orderable: false, targets: [3] }],
            "orderMulti": false
        });
    }

    private initDataTableCtrls(): void {
        let self = this;

        $(".author-get-lnk").off('click').click(function () {
            self.authorGetLnkOnClick(this);
        });

        $(".author-delete-btn").off('click').click(function () {
            let id = $(this).data('id');
            self.deleteForm(id);
        });

        $(".author-send-btn").off('click').click(function () {
            let id = $(this).data('id');
            self.service.sendToRabbitMQ(id).done(() => {
                Alert.showSuccess("Author was sent to RabbitMQ");
            })
        });
        
    };

    private authorGetLnkOnClick(sender: HTMLElement) {
        let self = this;
        let id = $(sender).data('id');
        let promise = self.service.get(id);

        promise.then(function (html) {
            self.getForm(html);
        });
    }

    private onSaveSuccess(message: string) {
        Alert.showSuccess(message);
        $("#popup").modal("hide");
        this.grid.ajax.reload();
    }

    public create() {
        let self = this;
        let author: AuthorVM = {
            Name: $("#Name").val() as string,
            Surname: $("#Surname").val() as string
        };
        self.service.create(author)
            .done(() => { self.onSaveSuccess("Author was created"); })
            .fail(() => { Alert.showError("Failed to create author"); });
    }

    public update() {
        let self = this;
        let author: AuthorVM = {
            Id: $("#Id").val() as number,
            Name: $("#Name").val() as string,
            Surname: $("#Surname").val() as string
        };
        self.service.update(author)
            .done(() => { self.onSaveSuccess("Author was updated"); })
            .fail(() => { Alert.showError("Failed to update author"); });
    }

    private getForm(html: string): void {
        let popup = $("#popup > .modal-dialog > .modal-content");
        popup.html(html);
        $("#popup").modal('show');
        Layout.disableAutocomplete();
    }

    private delete(id: number) {
        let self = this;
        this.service.delete(id)
            .done(() => {
                Alert.showSuccess("Author with Id=" + id + " was deleted");
                self.grid.ajax.reload();
            })
            .fail(() => {
                Alert.showSuccess("Failed to delete author with Id=" + id);
            });
    }

    private deleteForm(id: number): void {
        let self = this;
        $("#delete-popup").modal('show');
        Layout.disableAutocomplete();

        $("#delete").click(function () {
            self.delete(id);
        });
    }

    public publish() {
        let author: AuthorVM = {
            Name: $("#Name").val() as string,
            Surname: $("#Surname").val() as string
        };

        this.service.publish(author)
            .catch(() => {
                Alert.showError("Failed to publish author model to SNS");
            })
            .then((model) => {
                Alert.showSuccess("Author was published to SNS, message id: " + model);
                $("#popup").modal("hide");
            });
    }
}