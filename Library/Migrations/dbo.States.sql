CREATE TABLE [dbo].[States]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[ItemId] INT NOT NULL,
	[ItemAmount] INT NOT NULL

	CONSTRAINT [FK_States_Items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([id])
)