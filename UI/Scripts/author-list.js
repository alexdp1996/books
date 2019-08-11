var deleteUrl = '/Author/Delete';
var grid;

$(document).ready(function () {
    grid = $("#books").DataTable({
        serverSide: true,
        "searching": false,
        "scrollX": true,
        responsive: true,
        ajax: {
            url: "/Author/GetAuthors",
            type: "POST",
            datatype: "json",
            "dataSrc": function (json) {
                return json.data;
            }
        },
        columns: [
            {
                "data": function (data) {
                    return '<a href="/Author/Details?id=' + data.Id + '">' + data.Name + '</a>';
                }
            },
            {
                "data": function (data) {
                    return '<a href="/Author/Details?id=' + data.Id + '">' + data.Surname + '</a>';
                }
            },
            {
                "data": function (data) {
                    return data.CountOfBooks;
                }
            },
            {
                "data": function (data) {
                    return '<a class="btn btn-default" href="/Author/Edit?id=' + data.Id + '&surname=' + data.Surname + '">Edit</a>' +
                        '<button class="btn btn-danger" data-toggle="modal" data-target="#confirm-delete" data-id="' + data.Id + '">Delete</button>';
                }
            }
        ],
        columnDefs: [{ orderable: false, targets: [3] }],
        "orderMulti": false
    });
});