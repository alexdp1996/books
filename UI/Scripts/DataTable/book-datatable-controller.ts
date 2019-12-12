declare var moment;

class BookDataTableController extends BaseDataTableController implements IReloadable {
    private urls: BookDataTableUrlsVM;

    constructor(urls: BookDataTableUrlsVM) {
        super();
        this.urls = urls;
        this.gridSelector = "#books";
        this.initDT();
    }

    private rebindTriggers(): void {
        this.popupController.bind(".book-get", this.urls.get);
        this.popupController.bind(".author-get", this.urls.authorGet);
        this.popupController.bind(".book-delete", this.urls.delete);
    };

    private initDT(): void {
        let self = this;
        this.grid = $(self.gridSelector).DataTable({
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
                    "data": function(record: DetailedBookVM) {
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
                    "name": "Date",
                    "data": function (record: DetailedBookVM) {
                        let date = moment(record.Date).format('LL');
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
                                result.push('<a class="author-get" data-id="' + authors[i].Id + '" >' + authors[i].Name + ' ' + authors[i].Surname + '</a>');
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

        this.applyAddButton();
        this.rebindTriggers();
    }
}