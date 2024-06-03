CREATE TABLE [dbo].[Events]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[StateId] INT NOT NULL,
	[UserId] INT NOT NULL,
	[DateStamp] DATE NOT NULL,
	[EventType] VARCHAR(30) NOT NULL

	CONSTRAINT [FK_Events_States] FOREIGN KEY ([StateId]) REFERENCES [dbo].[States] ([Id]),
	CONSTRAINT [FK_Events_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
)