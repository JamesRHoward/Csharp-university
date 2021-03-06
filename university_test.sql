USE [university_test]
GO
/****** Object:  Table [dbo].[courses]    Script Date: 7/19/2016 11:12:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[courses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[course_name] [varchar](max) NULL,
	[course_number] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[courses_students]    Script Date: 7/19/2016 11:12:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[courses_students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[student_id] [int] NULL,
	[course_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[students]    Script Date: 7/19/2016 11:12:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[courses_students] ON 

INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (1, 38, 8)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (2, 39, 11)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (3, 42, 14)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (4, 45, 17)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (5, 51, 21)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (6, 52, 24)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (7, 56, 28)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (8, 63, 33)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (9, 66, 34)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (10, 67, 37)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (11, 70, 38)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (12, 71, 41)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (13, 74, 42)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (15, 78, 46)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (14, 75, 45)
INSERT [dbo].[courses_students] ([id], [student_id], [course_id]) VALUES (16, 79, 49)
SET IDENTITY_INSERT [dbo].[courses_students] OFF
