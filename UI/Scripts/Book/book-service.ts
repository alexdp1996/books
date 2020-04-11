﻿class BookService {
    private urls: EntityUrlsVM;

    constructor(urls: EntityUrlsVM) {
        this.urls = urls;
    }

    public create(model: BookVM) {
        let self = this;
        return $.ajax({
            url: self.urls.create,
            type: "POST",
            data: {
                model: model
            }
        });
    }

    public update(model: BookVM) {
        let self = this;
        return $.ajax({
            url: self.urls.update,
            type: "POST",
            data: {
                model: model
            }
        });
    }

    public delete(id: number) {
        let self = this;
        return $.ajax({
            type: "POST",
            url: self.urls.delete,
            data: {
                id: id
            }
        });
    }

    public get(id?: number): Promise<string> {
        let self = this;
        return $.ajax({
            url: self.urls.get,
            type: "GET",
            data: {
                id: id
            }
        });
    }
}