CREATE TABLE [dbo].[Items]
(
    [Id] INT NOT NULL PRIMARY KEY,
    [Title] VARCHAR(255) NOT NULL,
    [PublicationYear] INT NOT NULL,
    [Author] VARCHAR(255) NOT NULL,
    [ItemType] VARCHAR(30) NOT NULL
);
