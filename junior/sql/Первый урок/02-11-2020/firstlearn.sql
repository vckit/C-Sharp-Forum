-- создание новой базы данных
create database [basalili]
-- переключаемся в новую базу данных
use [basalili]
-- в новой базе данных создаём таблицу - product
create table [product] (
	-- первый столбец - это id, который является идентификатором (первичным ключом)
	[id] int identity(0,1),
	[title]		nvarchar(50)	not null,
	[model]		nvarchar(50)	not null,
	[number]	int				not null,
	[price]		money			not null,
	constraint pk_product_id primary key ([id])
)
go