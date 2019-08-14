var BookList = BookList || {};

(function () {

    var self = this;
    self.gridSelector = "#books";
    self.getBooksUrl = "";
    self.detailsUrl = "";
    self.authorDetailsUrl = "";
    self.editUrl = "";

    self.Init = function () {
        self.grid = $(self.gridSelector).DataTable({
            serverSide: true,
            "searching": false,
            "scrollX": true,
            responsive: true,
            ajax: {
                url: self.getBooksUrl,
                type: "POST",
                datatype: "json",
                "dataSrc": function (json) {
                    return json.data;
                }
            },
            columns: [
                {
                    "data": function (data) {
                        return '<a href="' + self.bookDetailsUrl+'?id=' + data.Id + '">' + data.Name + '</a>';
                    }
                },
                {
                    "data": function (data) {
                        return data.Pages;
                    }
                },
                {
                    "data": function (data) {
                        return data.Rate;
                    }
                },
                {
                    "data": function (data) {
                        let date = moment(data.Date).format('LL');
                        return date;
                    }
                },
                {
                    "data": function (data) {
                        let authors = data.Authors;
                        if (!authors.length) {
                            return '';
                        } else {
                            let result = [];
                            for (let i = 0; i < authors.length; ++i) {
                                result.push('<a href="' + self.authorDetailsUrl + '?id=' + authors[i].Id + '" >' +authors[i].Name + ' ' + authors[i].Surname + '</a>');
                            }
                            return result.join(', ');
                        }
                    }
                },
                {
                    "data": function (data) {
                        return '<a class="btn btn-default" href="' + self.editUrl + '?id=' + data.Id + '">Edit</a>' +
                            '<button class="btn btn-danger" data-toggle="modal" data-target="#confirm-delete" data-id="' + data.Id + '">Delete</button>';
                    }
                }
            ],
            columnDefs: [{ orderable: false, targets: [4, 5] }],
            "orderMulti": false
        });
    };

}).apply(BookList);