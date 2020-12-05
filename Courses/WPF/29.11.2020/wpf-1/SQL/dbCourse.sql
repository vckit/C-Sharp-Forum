CREATE DATABASE [dbCourse]

USE [dbCourse]


CREATE TABLE [Role](
	
	[RoleID]	NCHAR(1)		NOT NULL,
	[Title]		NVARCHAR(20)	NOT NULL,
	CONSTRAINT PK_Role_RoleID PRIMARY KEY ([RoleID])
)
GO

INSERT [Role]([RoleID], [Title]) VALUES ('A', 'Admin')
INSERT [Role]([RoleID], [Title]) VALUES ('U', 'User')

CREATE TABLE [SignIn](

	[ID]		INT IDENTITY (1000,1),
	[Username]	NVARCHAR(30)	NOT NULL,
	[Password]	NVARCHAR(30)	NOT NULL,
	-- ÂÒÎÐÈ×ÍÛÉ ÊËÞ× | FOREIGN KEY
	[IDRole]	NCHAR(1) CONSTRAINT FK_SignIn_IDRole_Role_RoleID FOREIGN KEY REFERENCES [Role]([RoleID])
	CONSTRAINT PK_SignIn_ID	PRIMARY KEY ([ID])
)
GO

INSERT [SignIn] ([Username], [Password], [IDRole]) VALUES ('thevckit', '1234', 'A')
INSERT [SignIn] ([Username], [Password], [IDRole]) VALUES ('Milana', '4321', 'U')
INSERT [SignIn] ([Username], [Password], [IDRole]) VALUES ('Alina', '8888', 'U')
INSERT [SignIn] ([Username], [Password], [IDRole]) VALUES ('Eldar', '4512', 'U')
INSERT [SignIn] ([Username], [Password], [IDRole]) VALUES ('Marat', '5857', 'U')
INSERT [SignIn] ([Username], [Password], [IDRole]) VALUES ('Helena', '9855', 'U')


SELECT [Username], [Password], [IDRole] FROM [SignIn]
