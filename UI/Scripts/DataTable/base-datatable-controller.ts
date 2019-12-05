class BaseDataTableController implements IReloadable {
    protected markupTemplate: string;
    protected grid: any;
    protected popupController: PopupController;
    protected gridSelector: string;

    constructor() {
        this.popupController = new PopupController();
        this.markupTemplate = `<'row'<'col-md-12'<'pull-left'l><'#add.pull-right'>>>
                               <'row'<'col-md-12'tr>>
                               <'row'<'col-md-12'<'pull-left'i><'pull-right'p>>>`;

    }

    protected applyAddButton() : void {
        let addTemplate = $("#add-template");
        let addContainer = $("#add");
        addContainer.html(addTemplate.html());
        addTemplate.remove();
    }

    public reload(): void {
        this.grid.ajax.reload();
    }
}