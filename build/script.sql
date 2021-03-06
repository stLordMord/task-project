USE [TrainingTaskDB]
GO
/****** Object:  StoredProcedure [dbo].[UpdateTask]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[UpdateTask]
GO
/****** Object:  StoredProcedure [dbo].[UpdateProject]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[UpdateProject]
GO
/****** Object:  StoredProcedure [dbo].[UpdateEmployee]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[UpdateEmployee]
GO
/****** Object:  StoredProcedure [dbo].[InsertTask]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[InsertTask]
GO
/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[InsertProject]
GO
/****** Object:  StoredProcedure [dbo].[InsertEmployee]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[InsertEmployee]
GO
/****** Object:  StoredProcedure [dbo].[GetTasksByProjectId]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetTasksByProjectId]
GO
/****** Object:  StoredProcedure [dbo].[GetTaskById]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetTaskById]
GO
/****** Object:  StoredProcedure [dbo].[GetStatusById]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetStatusById]
GO
/****** Object:  StoredProcedure [dbo].[GetProjectById]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetProjectById]
GO
/****** Object:  StoredProcedure [dbo].[GetPositionById]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetPositionById]
GO
/****** Object:  StoredProcedure [dbo].[GetEmployeeById]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetEmployeeById]
GO
/****** Object:  StoredProcedure [dbo].[GetCountOfTasks]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetCountOfTasks]
GO
/****** Object:  StoredProcedure [dbo].[GetCountOfStatuses]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetCountOfStatuses]
GO
/****** Object:  StoredProcedure [dbo].[GetCountOfProjects]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetCountOfProjects]
GO
/****** Object:  StoredProcedure [dbo].[GetCountOfPositions]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetCountOfPositions]
GO
/****** Object:  StoredProcedure [dbo].[GetCountOfEmployees]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetCountOfEmployees]
GO
/****** Object:  StoredProcedure [dbo].[GetAllTasks]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetAllTasks]
GO
/****** Object:  StoredProcedure [dbo].[GetAllStatuses]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetAllStatuses]
GO
/****** Object:  StoredProcedure [dbo].[GetAllProjects]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetAllProjects]
GO
/****** Object:  StoredProcedure [dbo].[GetAllPositions]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetAllPositions]
GO
/****** Object:  StoredProcedure [dbo].[GetAllEmployees]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[GetAllEmployees]
GO
/****** Object:  StoredProcedure [dbo].[DeleteTask]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[DeleteTask]
GO
/****** Object:  StoredProcedure [dbo].[DeleteProject]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[DeleteProject]
GO
/****** Object:  StoredProcedure [dbo].[DeleteEmployee]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP PROCEDURE [dbo].[DeleteEmployee]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Tasks_Statuses]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Tasks_Projects]
GO
ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Tasks_Employees]
GO
ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_Positions]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP TABLE [dbo].[Tasks]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP TABLE [dbo].[Statuses]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP TABLE [dbo].[Projects]
GO
/****** Object:  Table [dbo].[Positions]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP TABLE [dbo].[Positions]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 5/6/2020 8:11:57 PM ******/
DROP TABLE [dbo].[Employees]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NOT NULL,
	[PositionId] [int] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Positions]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Positions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ShortName] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Timing] [int] NOT NULL,
	[DateStart] [datetime] NOT NULL,
	[DateEnd] [datetime] NOT NULL,
	[StatusId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (1, N'Александр', N'Пушкин', N'Сергеевич', 2)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (2, N'Николай', N'Игафонов', N'Викторович', 1)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (3, N'Григорий', N'Агейкин', N'Петрович', 3)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (4, N'Лев', N'Вайсенберг', N'Маркович', 1)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (5, N'Илья', N'Федорович', N'Варавва', 4)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (6, N'Михаил', N'Игнатьев', N'Витальевич', 2)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (7, N'Павел', N'Тарантушко', N'Григорьевич', 3)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (8, N'Виктор', N'Григорьев', N'Павлович', 4)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (13, N'Ирина', N'Межлеско', N'Петровна', 1)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (14, N'Игнат', N'Самойлов', N'Максимович', 2)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (15, N'Александр', N'Троицкий', N'Геннадьевич', 3)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (16, N'Игорь', N'Абрамов', N'Георгиевич', 2)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (18, N'Петр', N'Петров', N'Петрович', 1)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (27, N'a', N'a', N'a', 2)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (28, N'Георгий', N'Геориев', N'Георгиевич', 2)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (29, N'q', N'q', N'q', 1)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (30, N'a', N'a', N'a', 1)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (31, N'Сергей', N'Бобуев', N'Михайлович', 4)
INSERT [dbo].[Employees] ([Id], [Name], [Surname], [Patronymic], [PositionId]) VALUES (55, N'asd', N'asd', N'asd', 3)
SET IDENTITY_INSERT [dbo].[Employees] OFF
SET IDENTITY_INSERT [dbo].[Positions] ON 

INSERT [dbo].[Positions] ([Id], [Name]) VALUES (1, N'Инженер')
INSERT [dbo].[Positions] ([Id], [Name]) VALUES (2, N'Менеджер')
INSERT [dbo].[Positions] ([Id], [Name]) VALUES (3, N'Программист')
INSERT [dbo].[Positions] ([Id], [Name]) VALUES (4, N'Техник')
INSERT [dbo].[Positions] ([Id], [Name]) VALUES (5, N'Охранник')
SET IDENTITY_INSERT [dbo].[Positions] OFF
SET IDENTITY_INSERT [dbo].[Projects] ON 

INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (1, N'Сервис текстов', N'MagicText', N'Облегчить работу с текстовым файлом.')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (2, N'Доставка еды на дом', N'Дон Батон', N'Супербыстрая и вкусная еда.')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (3, N'Сервис доставок', N'Пешкарики', N'Бодрый сервис недорогих доставок')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (4, N'Могилевская автолиния', N'Авто', N'Автомобили загрязняют окружающую среду')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (5, N'Игристое вино', N'Вино', N'Не лучше времяпрепровождения в совсременном мире.')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (6, N'Желто-синий пуфик', N'Пуфик', N'Находится в углу под доской.')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (8, N'Игристое вино', N'Kodiak', N'Это система упрощает жизнь и облегчает анализ информации.')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (10, N'Психологическое равновесие', N'Псих', N'Ходят легенды, что этим обладают люди.')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (12, N'Опилки в голове', N'Опилки', N'В голове моей опилки - не беда.')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (14, N'Золотая молодежь', N'Молодежь', N'Классовое разделение сквозь время.')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (15, N'Молодая русь', N'Русь', N'Русь-матушка')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (16, N'Прекрасное начало', N'Начало', N'Старт новой деятельности')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (17, N'q', N'q', N'q')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (18, N'qw', N'qw', N'qw')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (19, N'q', N'q', N'q')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (20, N'z', N'z', N'z')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (22, N'qaz', N'zaq', N'qaz')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (23, N'a', N'a', N'a')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (25, N'asasas', N'asaas', N'asas')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (26, N'Магнитушка', N'Магнит', N'Магнитное притяжение никто не отменял.')
INSERT [dbo].[Projects] ([Id], [Name], [ShortName], [Description]) VALUES (28, N'Работа круглые сутки', N'Раб', N'труляля')
SET IDENTITY_INSERT [dbo].[Projects] OFF
SET IDENTITY_INSERT [dbo].[Statuses] ON 

INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (1, N'Не начата')
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (2, N'В процессе')
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (3, N'Закончена')
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (4, N'Отложена')
SET IDENTITY_INSERT [dbo].[Statuses] OFF
SET IDENTITY_INSERT [dbo].[Tasks] ON 

INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (1, 3, N'Проектирование БД', 200, CAST(N'2020-03-03T00:00:00.000' AS DateTime), CAST(N'2020-03-06T00:00:00.000' AS DateTime), 2, 1)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (2, 3, N'Постановка задачи', 10, CAST(N'2020-03-01T00:00:00.000' AS DateTime), CAST(N'2020-03-02T00:00:00.000' AS DateTime), 3, 3)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (3, 2, N'Создание математической модели', 12, CAST(N'2020-03-18T00:00:00.000' AS DateTime), CAST(N'2020-03-22T00:00:00.000' AS DateTime), 4, 4)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (4, 3, N'Тестирование', 25, CAST(N'2020-05-01T00:00:00.000' AS DateTime), CAST(N'2020-04-05T00:00:00.000' AS DateTime), 1, 2)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (5, 3, N'Получение информации', 55, CAST(N'2020-03-05T00:00:00.000' AS DateTime), CAST(N'2020-03-07T00:00:00.000' AS DateTime), 3, 2)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (6, 1, N'z', 5, CAST(N'2020-04-01T00:00:00.000' AS DateTime), CAST(N'2020-04-01T00:00:00.000' AS DateTime), 3, 5)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (7, 2, N'Доставочка', 22, CAST(N'2020-02-22T00:00:00.000' AS DateTime), CAST(N'2020-03-02T00:00:00.000' AS DateTime), 3, 6)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (9, 5, N'Формирование задания', 8, CAST(N'2020-04-01T00:00:00.000' AS DateTime), CAST(N'2020-04-01T00:00:00.000' AS DateTime), 3, 6)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (12, 4, N'e', 5, CAST(N'2020-02-02T00:00:00.000' AS DateTime), CAST(N'2020-02-02T00:00:00.000' AS DateTime), 2, 14)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (17, 2, N'q', 1, CAST(N'2020-02-02T00:00:00.000' AS DateTime), CAST(N'2020-02-02T00:00:00.000' AS DateTime), 1, 1)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (19, 5, N'a', 5, CAST(N'2020-02-02T00:00:00.000' AS DateTime), CAST(N'2020-02-02T00:00:00.000' AS DateTime), 3, 1)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (20, 2, N'z', 4, CAST(N'2020-02-02T00:00:00.000' AS DateTime), CAST(N'2020-02-02T00:00:00.000' AS DateTime), 1, 2)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (22, 3, N'g', 5, CAST(N'2020-02-02T00:00:00.000' AS DateTime), CAST(N'2020-02-02T00:00:00.000' AS DateTime), 4, 3)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (23, 1, N'r', 4, CAST(N'2020-02-02T00:00:00.000' AS DateTime), CAST(N'2020-02-02T00:00:00.000' AS DateTime), 1, 3)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (24, 2, N'y', 2, CAST(N'2020-02-01T00:00:00.000' AS DateTime), CAST(N'2020-02-02T00:00:00.000' AS DateTime), 2, 6)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (25, 1, N'j', 1, CAST(N'2020-02-02T00:00:00.000' AS DateTime), CAST(N'2020-02-02T00:00:00.000' AS DateTime), 2, 6)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (26, 1, N'q', 12, CAST(N'2020-02-02T00:00:00.000' AS DateTime), CAST(N'2020-02-02T00:00:00.000' AS DateTime), 1, 14)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (27, 1, N'q', 12, CAST(N'2020-02-02T00:00:00.000' AS DateTime), CAST(N'2020-02-02T00:00:00.000' AS DateTime), 2, 14)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [Name], [Timing], [DateStart], [DateEnd], [StatusId], [EmployeeId]) VALUES (28, 1, N'q', 2, CAST(N'2220-02-02T00:00:00.000' AS DateTime), CAST(N'2020-02-02T00:00:00.000' AS DateTime), 3, 27)
SET IDENTITY_INSERT [dbo].[Tasks] OFF
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Positions] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Positions] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Positions]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Employees]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Projects]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Statuses]
GO
/****** Object:  StoredProcedure [dbo].[DeleteEmployee]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[DeleteEmployee]
	   @Id INT
AS
BEGIN
       DELETE FROM Employees WHERE Id=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteProject]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteProject]
	   @Id INT
AS
BEGIN
       DELETE FROM Projects WHERE Id=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteTask]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteTask]
	   @Id INT
AS
BEGIN
       DELETE FROM Tasks WHERE Id=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllEmployees]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllEmployees]
  @page INT,
  @size INT,
  @searchText NVARCHAR(150)  
AS  
BEGIN;  
WITH EmployeesSet  
AS(SELECT *, ROW_NUMBER() OVER (ORDER BY Id) AS [RowNo]
      FROM Employees WHERE Surname LIKE '%' + @searchText + '%')  
SELECT EmployeesSet.RowNo,  
    EmployeesSet.Id, 
	EmployeesSet.Name,  
    EmployeesSet.Surname,  
    EmployeesSet.Patronymic,  
    EmployeesSet.PositionId
	FROM EmployeesSet  
ORDER BY EmployeesSet.RowNo  
OFFSET(@page - 1) * @size ROWS  
FETCH NEXT @size ROWS ONLY; 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllPositions]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllPositions]
AS
BEGIN
    SELECT * FROM Positions
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllProjects]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAllProjects]
  @page INT,
  @size INT,
  @searchText NVARCHAR(150)  
AS  
BEGIN;  
WITH ProjectsSet  
AS(SELECT *, ROW_NUMBER() OVER (ORDER BY Id) AS [RowNo]
      FROM Projects WHERE ShortName LIKE '%' + @searchText + '%')  
SELECT ProjectsSet.RowNo,  
    ProjectsSet.Id, 
	ProjectsSet.Name,  
    ProjectsSet.ShortName,  
    ProjectsSet.Description
	FROM ProjectsSet  
ORDER BY ProjectsSet.RowNo  
OFFSET(@page - 1) * @size ROWS  
FETCH NEXT @size ROWS ONLY; 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllStatuses]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllStatuses]
AS
BEGIN
    SELECT * FROM Statuses
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllTasks]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAllTasks]
  @page INT,
  @size INT,  
  @searchText NVARCHAR(150)  
AS  
BEGIN;  
WITH TasksSet  
AS(SELECT *, ROW_NUMBER() OVER (ORDER BY Id) AS [RowNo]
      FROM Tasks WHERE Name LIKE '%' + @searchText + '%')  
SELECT TasksSet.RowNo,  
    TasksSet.ID, 
	TasksSet.ProjectId,  
    TasksSet.Name,  
    TasksSet.Timing,  
    TasksSet.DateStart,  
    TasksSet.DateEnd,  
	TasksSet.StatusId, 
    TasksSet.EmployeeId
	FROM TasksSet  
ORDER BY TasksSet.RowNo  
OFFSET(@page - 1) * @size ROWS  
FETCH NEXT @size ROWS ONLY; 
END
GO
/****** Object:  StoredProcedure [dbo].[GetCountOfEmployees]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCountOfEmployees]
	 @searchText NVARCHAR(150)
AS
BEGIN
	SELECT COUNT(Id) FROM Employees WHERE Surname LIKE '%' + @searchText + '%'
END
GO
/****** Object:  StoredProcedure [dbo].[GetCountOfPositions]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCountOfPositions]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT COUNT(Id) FROM Positions 
END
GO
/****** Object:  StoredProcedure [dbo].[GetCountOfProjects]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCountOfProjects]
@searchText NVARCHAR(150) 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT COUNT(Id) FROM Projects WHERE ShortName LIKE '%' + @searchText + '%'
END
GO
/****** Object:  StoredProcedure [dbo].[GetCountOfStatuses]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCountOfStatuses]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT COUNT(Id) FROM Statuses
END
GO
/****** Object:  StoredProcedure [dbo].[GetCountOfTasks]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCountOfTasks]
  @searchText NVARCHAR(150)  
AS  
BEGIN 
SELECT COUNT(Id) FROM Tasks WHERE Name LIKE '%' + @searchText + '%'
END
GO
/****** Object:  StoredProcedure [dbo].[GetEmployeeById]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEmployeeById]
       @id int
AS
BEGIN
       SELECT * FROM Employees Where Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[GetPositionById]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPositionById]
  @id INT
AS
BEGIN
    SELECT * FROM Positions WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[GetProjectById]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProjectById]
       @id int
AS
BEGIN
       SELECT * FROM Projects Where Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[GetStatusById]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetStatusById]
  @id INT
AS
BEGIN
    SELECT * FROM Statuses WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[GetTaskById]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTaskById]
       @id int
AS
BEGIN
       SELECT * FROM Tasks Where Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[GetTasksByProjectId]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTasksByProjectId]
       @ProjectId int
AS
BEGIN
       SELECT * FROM Tasks WHERE ProjectId=@ProjectId
END
GO
/****** Object:  StoredProcedure [dbo].[InsertEmployee]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertEmployee]
       @Name NVARCHAR(50),
	   @Surname NVARCHAR(50),
	   @Patronymic NVARCHAR(50),
	   @PositionId INT
AS
BEGIN
       INSERT INTO Employees VALUES(@Name, @Surname, @Patronymic, @PositionId)
	   SELECT Id, Name, Surname, Patronymic, PositionId FROM Employees WHERE Id=IDENT_CURRENT('Employees')
END
GO
/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertProject]
	   @Name NVARCHAR(50),
	   @ShortName NVARCHAR(30),
	   @Description NVARCHAR(150)
AS
BEGIN
		INSERT INTO Projects VALUES(@Name, @ShortName, @Description)
		SELECT Id, Name, ShortName, Description FROM Projects WHERE Id=IDENT_CURRENT ('Projects')
END
GO
/****** Object:  StoredProcedure [dbo].[InsertTask]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertTask]
       @ProjectId INT,
	   @Name NVARCHAR(50),
	   @Timing INT,
	   @DateStart DATETIME,
	   @DateEnd DATETIME,
	   @StatusId INT,
	   @EmployeeId INT
AS
BEGIN
		INSERT INTO Tasks VALUES(@ProjectId, @Name, @Timing, @DateStart, @DateEnd, @StatusId, @EmployeeId)
		SELECT Id, ProjectId, Name, Timing, DateStart, DateEnd, StatusId, EmployeeId FROM Tasks WHERE Id=IDENT_CURRENT ('Tasks')
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateEmployee]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[UpdateEmployee]
	   @Id INT,
       @Name NVARCHAR(50),
	   @Surname NVARCHAR(50),
	   @Patronymic NVARCHAR(50),
	   @PositionId INT
AS
BEGIN
       UPDATE Employees SET Name=@Name, Surname=@Surname, Patronymic=@Patronymic, PositionId=@PositionId WHERE Id=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProject]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateProject]
	   @Id INT,
	   @Name NVARCHAR(50),
	   @ShortName NVARCHAR(30),
	   @Description NVARCHAR(150)
AS
BEGIN
		UPDATE Projects SET Name=@Name, ShortName=@ShortName, Description=@Description WHERE Id=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTask]    Script Date: 5/6/2020 8:11:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateTask]
	   @Id INT,
       @ProjectId INT,
	   @Name NVARCHAR(50),
	   @Timing INT,
	   @DateStart DATETIME,
	   @DateEnd DATETIME,
	   @StatusId INT,
	   @EmployeeId INT
AS
BEGIN
		UPDATE Tasks SET ProjectId = @ProjectId, Name = @Name, Timing = @Timing, DateStart = @DateStart, DateEnd = @DateEnd, StatusId = @StatusId, EmployeeId = @EmployeeId  WHERE Id = @Id
END
GO
