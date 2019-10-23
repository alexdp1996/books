var Book = Book || {};

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

        GeneralConfig.disableAutocomplete();
    };

    $("#popup form").on('submit', function (e) {
        e.preventDefault();
        let book = {
            Id: $("#Id").val(),
            Name: $("#Name").val(),
            Rate: $("#Rate").val(),
            Date: $("#Date").val(),
            Pages: $("#Pages").val(),
            AuthorIds: $("#AuthorIds").val()
        };

        $form = $(this);

        let url = $form.attr('action');

        $.ajax({
            url: url,
            type: "POST",
            data: {
                model: book
            }
        }).done(function (data) {
            if (data.Type === Alert.Type.Success) {
                BookList && BookList.reload();
                Alert.show("#alert-box", 'Book was saved', Alert.Type.Success);
                $("#popup").modal("hide");
            } else {
                Alert.show("#popup-alert-box", data);
            }
        });
    });
}).apply(Book);