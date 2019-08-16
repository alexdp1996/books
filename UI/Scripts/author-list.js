var AuthorList = AuthorList || {};

(function () {

    var self = this;
    self.gridSelector = "#authors";
    self.getAuthorsUrl = "";
    self.getUrl = "";

    self.Init = function () {
        self.grid = $(self.gridSelector).DataTable({
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
                        return '<a href="' + self.getUrl + '?id=' + data.Id + '">' + data.Name + '</a>';
                    }
                },
                {
                    "data": function (data) {
                        return '<a href="' + self.getUrl + '?id=' + data.Id + '">' + data.Surname + '</a>';
                    }
                },
                {
                    "data": function (data) {
                        return data.CountOfBooks;
                    }
                },
                {
                    "data": function (data) {
                        return '<button class="btn btn-danger" data-toggle="modal" data-target="#confirm-delete" data-id="' + data.Id + '">Delete</button>';
                    }
                }
            ],
            columnDefs: [{ orderable: false, targets: [3] }],
            "orderMulti": false
        });
    };

}).apply(AuthorList);