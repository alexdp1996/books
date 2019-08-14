var ChooseAuthor = ChooseAuthor || {};

(function () {
    var self = this;
    self.authorSelector = "#AuthorIds";
    self.getAuthorsByTermUrl = "";

    self.Init = function () {
        $(self.authorSelector).select2({
            multiple: true,
            ajax: {
                url: self.getAuthorsByTermUrl,
                dataType: 'json',
                dataSrc: function (data) {
                    return data.Data;
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
    };
}).apply(ChooseAuthor);