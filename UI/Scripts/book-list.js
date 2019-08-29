﻿var BookList = BookList || {};

(function () {

    var self = this;
    self.gridSelector = "#books";
    self.getUrl = "";
    self.getDataUrl = "";
    self.authorGetUrl = "";
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
                        return '<a href="' + self.getUrl+'?id=' + data.Id + '">' + data.Name + '</a>';
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
                                result.push('<a href="' + self.authorGetUrl + '?id=' + authors[i].Id + '" >' +authors[i].Name + ' ' + authors[i].Surname + '</a>');
                            }
                            return result.join(', ');
                        }
                    }
                },
                {
                    "data": function (data) {
                        return '<button class="btn btn-danger" data-toggle="modal" data-target="#confirm-delete" data-id="' + data.Id + '">Delete</button>';
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

        DeleteModal.url = self.deleteUrl;
        DeleteModal.grid = self.grid;
        DeleteModal.Init();
    };

}).apply(BookList);