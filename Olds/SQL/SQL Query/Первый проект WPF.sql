CREATE DATABASE [WPF]
USE [WPF]
GO

CREATE TABLE [Role] (
	
	[ID]	NCHAR(1)		NOT NULL,
	[Title]	NVARCHAR(MAX)	NOT NULL,
	CONSTRAINT PK_Role_ID PRIMARY KEY ([ID])
)
GO

-- ббндхл дюммше б рюакхжс [Role]
INSERT [Role]([ID],[Title]) VALUES ('A','Admin')
INSERT [Role]([ID],[Title]) VALUES ('U','User')

CREATE TABLE [User]	(
	
	[ID]	INT IDENTITY(0,1),
	[Username]	NVARCHAR(MAX)	NOT NULL,
	[Password]	NVARCHAR(MAX)	NOT NULL,
	[IDRole]	NCHAR(1)	CONSTRAINT FK_User_IDRole_Role_ID FOREIGN KEY REFERENCES [Role]([ID]) NOT NULL,
	CONSTRAINT PK_User_ID	PRIMARY KEY ([ID])
)
GO
-- ббндхл дюммше б рюакхжс [User]
INSERT INTO [User]([Username],[Password],[IDRole]) VALUES ('vckit', '#hello', 'A')
INSERT INTO [User]([Username],[Password],[IDRole]) VALUES ('thevckit', '#hello', 'U')

SELECT * FROM [User]