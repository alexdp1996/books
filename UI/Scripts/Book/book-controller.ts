declare var ProcessedResult;
declare var moment;

class BookController {
    private authorsUrl: string;
    private grid: any;
    private service: BookService;
    private getListUrl: string;
    private selectSourceAuthors: AuthorVM[];

    constructor(urls: BookUrlsVM) {
        this.service = new BookService(urls);
        this.authorsUrl = urls.getAuthors;
        this.getListUrl = urls.getList;
    }

    public init() {
        this.initDataTable(this.getListUrl);
        this.initBtn();
    }

    private initDataTable(dataUrl: string): void {
        let self = this;
        this.grid = $("#books").DataTable({
            "drawCallback": function () {
                self.initDataTableCtrls();
            },
            dom: `<'row'<'col-md-12'<'pull-left'l><'#bookAddDiv.pull-right'>>>
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
                    "data": function (record: BookVM) {
                        return '<a class="book-get" href="#" data-id="' + record.Id + '">' + record.Name + '</a>';
                    }
                },
                {
                    "name": "Pages",
                    "data": function (record: BookVM) {
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
                    "data": function (record: BookVM) {
                        let date = moment(record.CreatedDate).format('LL');
                        return date;
                    }
                },
                {
                    "name": "Authors",
                    "data": function (record: BookVM) {
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
                    "data": function (record: BookVM) {
                        return '<button class="btn btn-danger book-delete-btn" data-id="' + record.Id + '">Delete</button>';
                    }
                }
            ],
            columnDefs: [{ orderable: false, targets: [4, 5] }],
            "orderMulti": false
        });
    }

    private initBtn() {
        let bookAddBtn = $("#book-create-btn")[0];
        let bookAddBtnHtml = bookAddBtn.outerHTML;
        bookAddBtn.remove();

        let bookAddDiv = $("#bookAddDiv");
        bookAddDiv.html(bookAddBtnHtml);

        let self = this;
        $("#book-create-btn").off('click').click(function () {
            self.bookCreateBtnOnClick();
        });

        $("#book-publish-btn").off('click').click(function () {
            self.service.getPublishForm()
                .then((response) => {
                    self.getForm(response);
                });
        });
    }

    private bookCreateBtnOnClick() {
        let self = this;
        let promise = self.service.get();
        promise.then(function (html) {
            self.getForm(html);
        });
    }

    private initDataTableCtrls(): void {
        let self = this;
        $(".book-get").off('click').click(function () {
            self.bookGetLnkOnClick(this);
        });

        $(".book-delete-btn").off('click').click(function () {
            let id = $(this).data('id');
            self.deleteForm(id);
        });
    };

    private bookGetLnkOnClick(sender: HTMLElement) {
        let self = this;
        let id = $(sender).data('id');
        let promise = self.service.get(id);

        promise.then(function (html) {
            self.getForm(html);
        });
    }

    createSelectOption(item: AuthorVM) {
        let optionIsPresent = this.selectSourceAuthors.some(function (e) {
            e.Id == item.Id;
        });

        if (!optionIsPresent) {
            this.selectSourceAuthors.push(item);
        }
        
        return {
            text: item.Name + " " + item.Surname,
            id: item.Id
        };
    }

    public initPlugins(selectedAuthors: AuthorVM[]) {
        let self = this;
        this.selectSourceAuthors = selectedAuthors;

        let authorsCtrl = $("#Authors").select2({
            multiple: true,
            minimumInputLength: 3,
            ajax: {
                url: self.authorsUrl,
                dataType: 'json',
                processResults: (data) => {
                    return {
                        results: $.map(data, (item) => { return self.createSelectOption(item); })
                    };
                }
            }
        });

        selectedAuthors.every((e) => {
            authorsCtrl.append(new Option(e.Name + " " + e.Surname, e.Id.toString(), false, true));
        });

        $(".datepicker").datepicker();
    }

    private onSaveSuccess(alert: string) {
        Alert.showSuccess(alert);
        $("#popup").modal("hide");
        this.grid.ajax.reload();
    }

    public create() {
        let self = this;
        let selectedAuthorsIds = ($("#Authors").val() as any) as number[]; 
        let book: BookVM = {
            Name: $("#Name").val() as string,
            Rate: +$("#Rate").val(),
            CreatedDate: ($("#CreatedDate").val() as any) as Date,
            Pages: +$("#Pages").val(),
            Authors: this.selectSourceAuthors.filter(function (sourceItem) {
                return selectedAuthorsIds.some(function (selectedId) { return sourceItem.Id == selectedId });
            })
            
        };
        self.service.create(book)
            .done(() => { self.onSaveSuccess("Book was created"); })
            .fail(() => { Alert.showError("Failed to create book"); });
    }

    public update() {
        let self = this;
        let selectedAuthorsIds = ($("#Authors").val() as any) as number[]
        let book: BookVM = {
            Id: $("#Id").val() as number,
            Name: $("#Name").val() as string,
            Rate: +$("#Rate").val(),
            CreatedDate: ($("#CreatedDate").val() as any) as Date,
            Pages: +$("#Pages").val(),
            Authors: this.selectSourceAuthors.filter(function (sourceItem) {
                return selectedAuthorsIds.some(function (selectedId) { return sourceItem.Id == selectedId });
            })
        };
        self.service.update(book)
            .done(() => { self.onSaveSuccess("Book was updated"); })
            .fail(() => { Alert.showError("Failed to update book"); });
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
                Alert.showSuccess("Book with Id=" + id + " was deleted");
                self.grid.ajax.reload();
            })
            .fail(() => {
                Alert.showSuccess("Failed to delete book with Id=" + id);
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
        let book: BookVM = {
            Name: $("#Name").val() as string,
            Rate: +$("#Rate").val(),
            CreatedDate: ($("#CreatedDate").val() as any) as Date,
            Pages: +$("#Pages").val(),
            Authors: []
        };

        this.service.publish(book)
            .catch(() => {
                Alert.showError("Failed to publish book model to SNS");
            })
            .then((model) => {
                Alert.showSuccess("Book was published to SNS, message id: " + model);
                $("#popup").modal("hide");
            });
    }
}