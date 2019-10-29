class BookController {
    private authorsUrl: string;
    private authorSelector: string;

    constructor(authorsUrl: string) {
        this.authorsUrl = authorsUrl;
        this.authorSelector = "#AuthorIds";
        this.init();
    }

    private init() {
        let self = this;
        $(this.authorSelector).select2({
            multiple: true,
            ajax: {
                url: self.authorsUrl,
                dataType: 'json',
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
    }
}