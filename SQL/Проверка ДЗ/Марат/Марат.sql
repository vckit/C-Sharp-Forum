CREATE DATABASE [Book_shop]

USE [Book_shop]

GO
-- Таблица Книги
CREATE TABLE [Book] (

	[ID]			INT IDENTITY(0, 1),
	[Name_Book]		NVARCHAR(100)		NOT NULL,
	[Nub_Page]		INT					NOT NULL,
	[IDAvtor]		INT CONSTRAINT FK_Book_IDAvtor_Avtor_ID FOREIGN KEY REFERENCES [Avtor]([ID])			NOT NULL,
	[IDPublisher]	INT	CONSTRAINT FK_Book_IDPublisher_Publisher_ID FOREIGN KEY REFERENCES [Publisher]([ID])NOT NULL,
	[IDJender]		INT	CONSTRAINT FK_Book_IDJender_Jender_ID FOREIGN KEY REFERENCES [Jander]([ID]) NOT NULL,
	CONSTRAINT PK_Book_ID	PRIMARY KEY ([ID])
)
GO
-- Таблица Жанр
CREATE TABLE [Jander] (

	[ID]			INT IDENTITY(0,1),
	[Name_Jander]	NVARCHAR(100)	NOT NULL,
	CONSTRAINT	PK_Jander_ID	PRIMARY KEY ([ID])

)
GO
-- Таблица Автор
CREATE TABLE [Avtor] (

	[ID]			INT IDENTITY(0,1),
	[Surname]		NVARCHAR(100)		NOT NULL,
	[Name]			NVARCHAR(100)		NOT NULL,
	[Middel_Name]	NVARCHAR(100)		NOT NULL,
	[Date_Year]		DATE				NOT NULL,
	CONSTRAINT	PK_Avtor_ID		PRIMARY KEY ([ID])
		
)
GO
-- Таблица Издатель
CREATE TABLE [Publisher] (

	[ID]				INT IDENTITY(0,1),
	[Name_Publisher]	NVARCHAR(150)		NOT NULL,
	[Adress]			NVARCHAR(150)		NOT NULL,
	[Country]			NVARCHAR(150)		NOT NULL,
	CONSTRAINT	PK_Publisher_ID		PRIMARY KEY ([ID])
)
GO