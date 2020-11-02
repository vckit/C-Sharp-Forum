CREATE database [XZZZZZZZZZZZZ]
use [XZZZZZZZZZZZZ]
go
CREATE TABLE [Телефон]
create database [Magaz]
use [magaz]
go
create table [Knigi]
(
	[ID] INT PRIMARY KEY ,
	[Наименование]				VARCHAR(50)		NOT NULL,
	[Количество страниц]		int			    NOT NULL,
	[ID Жанр]					NVARCHAR(1) CONSTRAINT PK_Knigi_ID Жанр_Жанр_ID
	FOREIGN KEY REFERENCES [Knigi]([Жанр]) NOT NULL,,
	[ID Автора]				 	NVARCHAR(1),	
	[ID Издатель]				NVARCHAR(1)
	
)
create table [Жанр]
(
	[ID] INT PRIMARY KEY ,	
	[Наименование]			   VARCHAR(50)		 NOT NULL,
)
go
create table [Автор]
(
	[ID]						INT PRIMARY KEY,	
	[Фамилия]					VARCHAR(50)		NOT NULL,
	[Имя]						VARCHAR(50)		NOT NULL,
	[Отчество]					VARCHAR(50)		NOT NULL,
	[Дата]						DateTime		NOT NULL,
)
create table [Издатель]
(
	[ID]						INT PRIMARY KEY,
	[Наименование]				VARCHAR(50)		NOT NULL,
	[Адрес]						VARCHAR(50)		NOT NULL,
	[Страна]					VARCHAR(50)		NOT NULL,
)
