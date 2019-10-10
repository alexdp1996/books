CREATE TABLE [dbo].[Author] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NULL,
    [Surname] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Author] PRIMARY KEY CLUSTERED ([Id] ASC)
);

