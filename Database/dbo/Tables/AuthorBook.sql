CREATE TABLE [dbo].[AuthorBook] (
    [AuthorId] BIGINT NOT NULL,
    [BookId]   BIGINT NOT NULL,
    CONSTRAINT [PK_dbo.AuthorBook] PRIMARY KEY CLUSTERED ([AuthorId] ASC, [BookId] ASC),
    CONSTRAINT [FK_dbo.AuthorBook_dbo.Author_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Author] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AuthorBook_dbo.Book_BookId] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Book] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_AuthorId]
    ON [dbo].[AuthorBook]([AuthorId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BookId]
    ON [dbo].[AuthorBook]([BookId] ASC);

