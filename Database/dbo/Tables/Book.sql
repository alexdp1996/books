CREATE TABLE [dbo].[Book] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (MAX) NULL,
    [Rate]          INT            NOT NULL,
    [Pages]         INT            NOT NULL,
    [Date]          DATETIME       NOT NULL,
    [Discriminator] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.Book] PRIMARY KEY CLUSTERED ([Id] ASC)
);

