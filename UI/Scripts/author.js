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
            let alert = new AlertController();
            if (data.Type === AlertType.Success) {
                typeof DT !== 'undefined' && DT.reload();
                alert.show("#alert-box", data);
                $("#popup").modal("hide");
            } else {
                alert.show("#popup-alert-box", data);
            }
        });
    });
}).apply(Author);