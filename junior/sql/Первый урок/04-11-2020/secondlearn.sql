USE [basalili]

CREATE TABLE [Car] (

-- id первичный ключ
	[ID]		INT	IDENTITY(0,1),
-- наименование
	[Title]		NVARCHAR(MAX)	NOT NULL,
-- модель
	[Model]		NVARCHAR(MAX)	NOT NULL,
-- год выпуска
	[Year]		NVARCHAR(MAX)	NOT NULL,
-- гос. номер
	[Number]	NVARCHAR(9)		NOT NULL,
-- устанавливаем ограничение первичного ключа
	CONSTRAINT PK_Car_ID PRIMARY KEY ([ID])
)
GO

-- добавление данных в таблицу через sql script
INSERT [Car] ([Title], [Model], [Year], [Number]) VALUES ('mercedes benz', 'amg63', '01.01.2020', 'a999aa01')

-- выборка всех данных / вывод всех данных
SELECT * FROM [Car]
-- выбор желаемых данных / или указанных нами данных
SELECT [Car].[Model], [Car].[Title] FROM [Car]