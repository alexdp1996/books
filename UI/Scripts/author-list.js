var AuthorList = AuthorList || {};

(function () {

    var self = this;
    self.gridSelector = "#authors";
    self.getDataUrl = "";
    self.getUrl = "";
    self.deleteUrl = "";

    self.Init = function () {
        self.grid = $(self.gridSelector).DataTable({
            dom: `<'row'<'col-md-12'<'pull-left'l><'#add.pull-right'>>>
                  <'row'<'col-md-12'tr>>
                  <'row'<'col-md-12'<'pull-left'i><'pull-right'p>>>`,
            serverSide: true,
            "searching": false,
            "scrollX": true,
            responsive: true,
            ajax: {
                url: self.getDataUrl,
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
                        return data.Surname;
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

        let addTemplate = $("#add-template");
        let addContainer = $("#add");
        addContainer.html(addTemplate.html());
        addTemplate.remove();

        DeleteModal.url = self.deleteUrl;
        DeleteModal.grid = self.grid;
        DeleteModal.Init();
    };

}).apply(AuthorList);