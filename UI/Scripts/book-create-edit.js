$(document).ready(function () {
    $("#AuthorIds").select2({
        multiple: true,
        ajax: {
            url: "/Book/GetAuthorsByTerm",
            dataType: 'json',
            dataSrc: function (data) {
                return data.Data
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.Name + " " + item.Surname,
                            id: item.Id
                        };
                    })
                };
            }
        }
    });
    $(".datepicker").datepicker();
});