var AuthorList = AuthorList || {};

(function () {

    var self = this;
    self.gridSelector = "#authors";
    self.getAuthorsUrl = "";
    self.detailsUrl = "";
    self.editUrl = "";

    self.Init = function () {
        self.grid = $(gridSelector).DataTable({
            serverSide: true,
            "searching": false,
            "scrollX": true,
            responsive: true,
            ajax: {
                url: self.getAuthorsUrl,
                type: "POST",
                datatype: "json",
                "dataSrc": function (json) {
                    return json.data;
                }
            },
            columns: [
                {
                    "data": function (data) {
                        return '<a href="' + self.detailsUrl + '?id=' + data.Id + '">' + data.Name + '</a>';
                    }
                },
                {
                    "data": function (data) {
                        return '<a href="' + self.detailsUrl + '?id=' + data.Id + '">' + data.Surname + '</a>';
                    }
                },
                {
                    "data": function (data) {
                        return data.CountOfBooks;
                    }
                },
                {
                    "data": function (data) {
                        return '<a class="btn btn-default" href="' + self.editUrl + '?id=' + data.Id + '&surname=' + data.Surname + '">Edit</a>' +
                            '<button class="btn btn-danger" data-toggle="modal" data-target="#confirm-delete" data-id="' + data.Id + '">Delete</button>';
                    }
                }
            ],
            columnDefs: [{ orderable: false, targets: [3] }],
            "orderMulti": false
        });
    };

}).apply(AuthorList);