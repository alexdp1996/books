class GeneralConfigController {
    private navSelector: string = ".navbar.navbar-info.bg-info.navbar-fixed-top";
    private bodySelector: string = ".container.body-content";

    constructor() {
        this.setOnResize();
        this.setHeight();
        this.disableAutocomplete();
    }

    private setHeight() {
        let nav = $(this.navSelector);
        let navHeight = nav.height();
        let neededBodyHeight = navHeight + 15;
        let body = $(this.bodySelector);
        body.css('padding-top', neededBodyHeight);
    };

    public disableAutocomplete() : void {
        $("input:text,form").attr("autocomplete", "off");
    };

    private setOnResize() {
        let self = this;
        $(window).resize(function () {
            self.setHeight();
        });
    };
}

var GeneralConfig: GeneralConfigController;
$(document).ready(function () {
    GeneralConfig = new GeneralConfigController();
});