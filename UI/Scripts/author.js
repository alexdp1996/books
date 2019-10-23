var Author = Author || {};

(function () {
    var self = this;

    self.Init = function () {
        GeneralConfig.disableAutocomplete();
    };

    $("#popup form").on('submit', function (e) {
        e.preventDefault();

        let author = {
            Id: $("#Id").val(),
            Name: $("#Name").val(),
            Surname: $("#Surname").val()
        };

        $form = $(this);

        let url = $form.attr('action');

        $.ajax({
            url: url,
            type: "POST",
            data: {
                model: author
            }
        }).done(function (data) {
            if (data.Type === Alert.Type.Success) {
                typeof BookList !== 'undefined' && BookList.reload();
                typeof AuthorList !== 'undefined' && AuthorList.reload();
                Alert.show("#alert-box", data);
                $("#popup").modal("hide");
            } else {
                Alert.show("#popup-alert-box", data);
            }
        });
    });
}).apply(Author);