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
                    let date = data.Date;
                    return date.toString();
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
                            result.push('<li><a href="/Author/Details?id=' + authors[i].Id + '" >' +authors[i].Name + ' ' + authors[i].Surname + '</a></li>');
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
        columnDefs: [{ orderable: false, targets: [4, 5] }]
    });
});