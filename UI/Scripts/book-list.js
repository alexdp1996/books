var deleteUrl = '/Book/Delete';
var grid;

$(document).ready(function () {
    grid = $("#books").DataTable({
        serverSide: true,
        "searching": false,
        "scrollX": true,
        responsive: true,
        ajax: {
            url: "/Book/GetBooks",
            type: "POST",
            datatype: "json",
            "dataSrc": function (json) {
                return json.data;
            }
        },
        columns: [
            {
                "data": function (data) {
                    return '<a href="/Book/Details?id=' + data.Id + '">' + data.Name + '</a>';
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
                    let authors = data.Authors;
                    if (!authors.lenght) {
                        return '';
                    } else {
                        let result = [];
                        for (let i = 0; i < authors.lenght; ++i) {
                            result.push('<li>authors[i].Name + ' + authors[i].Surname + '</li>');
                        }
                        return '<ul>' + result.join() + '</ul>';
                    }
                }
            },
            {
                "data": function (data) {
                    return '<a class="btn btn-default" href="/Book/Edit?id=' + data.Id + '">Edit</a>' +
                        '<button class="btn btn-danger" data-toggle="modal" data-target="#confirm-delete" data-id="' + data.Id + '">Delete</button>';
                }
            }
        ],
        columnDefs: [{ orderable: false, targets: [3, 4] }]
    });
});