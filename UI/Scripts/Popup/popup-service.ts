class PopupService {
    public getPopup(model: PopupVM, callback: (html: string) => void): void {
        $.ajax({
            url: model.Url,
            type: "GET",
            data: {
                id: model.Id
            },
            success: callback
        });
    }
}