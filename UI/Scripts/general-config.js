var GeneralConfig = GeneralConfig || {};

(function () {
    var self = this;
    self.navSelector = ".navbar.navbar-info.bg-info.navbar-fixed-top";
    self.bodySelector = ".container.body-content";

    self.setHeight = function () {
        let nav = $(self.navSelector);
        let navHeight = nav.height();
        let neededBodyHeight = navHeight + 15;
        let body = $(self.bodySelector);
        body.css('padding-top', neededBodyHeight);
    };

    self.disableAutocomplete = function () {
        $("input:text,form").attr("autocomplete", "off");
    };

    self.Init = function () {
        $(window).resize(function () {
            self.setHeight();
        });

        self.setHeight();
        self.disableAutocomplete();
    };

}).apply(GeneralConfig);

