CREATE DATABASE [dbLogin]

USE [dbLogin]
GO

CREATE TABLE [Role] (

	[ID]	NCHAR(1)		NOT NULL,
	[Title]	NVARCHAR(MAX)	NOT NULL,
	CONSTRAINT PK_Role_ID PRIMARY KEY ([ID])

)
GO

INSERT [Role]([ID],[Title]) VALUES ('A','Admin')
INSERT [Role]([ID],[Title]) VALUES ('U','User')

CREATE TABLE [User] (

	[ID]	INT IDENTITY(0, 1),
	[Username]	NVARCHAR(MAX)	NOT NULL,
	[Password]	NVARCHAR(MAX)	NOT NULL,
	[IDRole]	NCHAR(1)	CONSTRAINT FK_User_IDRole_Role_ID FOREIGN KEY REFERENCES [Role]([ID]) NOT NULL,
	CONSTRAINT	PK_User_ID PRIMARY KEY ([ID])

)

GO
INSERT INTO [User]([Username],[Password],[IDRole])	VALUES ('maratkaxa', '2001', 'A')
INSERT INTO [User]([Username],[Password],[IDRole])	VALUES ('marat111', '1337', 'U')

SELECT * FROM [User]