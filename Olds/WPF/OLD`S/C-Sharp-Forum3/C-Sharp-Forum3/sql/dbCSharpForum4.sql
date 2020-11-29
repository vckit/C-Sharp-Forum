
-- —Œ«ƒ¿Õ»≈ ¡¿«€ ƒ¿ÕÕ€’
CREATE DATABASE [dbCSharpForum4]
USE [dbCSharpForum4]
GO

-- —Œ«ƒ¿Õ»≈ “¿¡À»÷€ –ŒÀ»
CREATE TABLE [Role] (
	
	[RoleID]	NCHAR(1)		NOT NULL,
	[Title]		NVARCHAR(200)	NOT NULL,
	CONSTRAINT PK_Role_RoleID	PRIMARY KEY ([RoleID])
)
GO

-- —Œ«ƒ¿Õ»≈ “¿¡À»÷” SIGNIN
CREATE TABLE [SignIn] (
	
	ID INT IDENTITY(0,1),
	[Username]	VARCHAR(40)	CONSTRAINT Unique_SignIn_Username UNIQUE NOT NULL,
	[Password]	NVARCHAR(40)	NOT NULL,
	[IDRole]	NCHAR(1) CONSTRAINT FK_SignIn_IDRole_Role_RoleID FOREIGN KEY REFERENCES 
	[Role]([RoleID]) ON DELETE CASCADE NOT NULL
)
GO

-- —Œ«ƒ¿Õ»≈ “¿¡À»÷€  ŒÃœ”“≈–
CREATE TABLE [Computer] (
	
	[ID]			INT IDENTITY (0,1),
	[CPU]			NVARCHAR(40)	NOT NULL,
	[GPU]			NVARCHAR(40)	NOT NULL,
	[PowerSupply]	NVARCHAR(40)	NOT NULL,
	[RAM]			NVARCHAR(40)	NOT NULL,
	[MotherBoard]	NVARCHAR(40)	NOT NULL,
	[Housing]		NVARCHAR(40)	NOT NULL,
	CONSTRAINT PK_Computer_ID PRIMARY KEY ([ID])
)
GO


-- —Œ«ƒ¿Õ»≈ “¿¡À»÷€ —“–¿Õ¿ œ–Œ»«¬Œƒ»“≈À‹
CREATE TABLE [CountryManufacture] (
	
	[CountryID]	NCHAR(3)		NOT NULL,
	[Title]		NVARCHAR(40)	NOT NULL,
	CONSTRAINT PK_CountryManufacture_CountryID PRIMARY KEY ([CountryID])
)
GO

INSERT [CountryManufacture] ([CountryID], [Title]) VALUES ('RUS', 'RUSSIA')
INSERT [CountryManufacture] ([CountryID], [Title]) VALUES ('USA', 'UNITED STATES AMERICA')
INSERT [CountryManufacture] ([CountryID], [Title]) VALUES ('CHR', 'CHINE')

-- CŒ«ƒ¿Õ»≈ “¿¡À»÷€ œ–Œƒ” “€
CREATE TABLE [Product] (
	
	[ID]	INT IDENTITY(0,1),
	[IDComputer] INT CONSTRAINT FK_Product_IDComputer_Computer_ID FOREIGN KEY REFERENCES [Computer]([ID]) ON DELETE CASCADE NOT NULL,
	[IDCoutryManufacture] NCHAR(3) CONSTRAINT FK_Product_IDCoutryManufacture_CountryManufacture_CountryID FOREIGN KEY REFERENCES [CountryManufacture]([CountryID]) ON DELETE CASCADE NOT NULL,
	CONSTRAINT PK_Product_ID PRIMARY KEY ([ID])
)
GO


INSERT [Role] ([RoleID], [Title]) VALUES ('A','ADMIN')
INSERT [Role] ([RoleID], [Title]) VALUES ('U','USER')

INSERT [SignIn] ([Username], [Password], [IDRole]) VALUES ('vckit', '1234', 'A')
INSERT [SignIn] ([Username], [Password], [IDRole]) VALUES ('thevckit', '1234', 'A')