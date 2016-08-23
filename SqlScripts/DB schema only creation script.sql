USE [master]
GO
/****** Object:  Database [StudentTestDB]    Script Date: 23.08.2016 13:38:26 ******/
CREATE DATABASE [StudentTestDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StudentTestDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\StudentTestDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'StudentTestDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\StudentTestDB_log.ldf' , SIZE = 3136KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [StudentTestDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StudentTestDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StudentTestDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StudentTestDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StudentTestDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StudentTestDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StudentTestDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [StudentTestDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StudentTestDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [StudentTestDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StudentTestDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StudentTestDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StudentTestDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StudentTestDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StudentTestDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StudentTestDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StudentTestDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StudentTestDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StudentTestDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StudentTestDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StudentTestDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StudentTestDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StudentTestDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StudentTestDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StudentTestDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StudentTestDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [StudentTestDB] SET  MULTI_USER 
GO
ALTER DATABASE [StudentTestDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StudentTestDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StudentTestDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StudentTestDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'StudentTestDB', N'ON'
GO
USE [StudentTestDB]
GO
/****** Object:  Table [dbo].[Accesses]    Script Date: 23.08.2016 13:38:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accesses](
	[AccessId] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NOT NULL,
	[TestId] [int] NOT NULL,
 CONSTRAINT [PK_Accesses1] PRIMARY KEY CLUSTERED 
(
	[AccessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Answers]    Script Date: 23.08.2016 13:38:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Answers](
	[AnswerId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[AnswerText] [nvarchar](max) NULL,
	[AnswerImage] [varbinary](max) NULL,
	[AnswerIsCorrect] [bit] NOT NULL,
 CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED 
(
	[AnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 23.08.2016 13:38:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Questions]    Script Date: 23.08.2016 13:38:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Questions](
	[QuestionId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionText] [nvarchar](max) NOT NULL,
	[QuestionImage] [varbinary](max) NULL,
	[TestId] [int] NULL,
 CONSTRAINT [PK_Questions1] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ResultAnswers]    Script Date: 23.08.2016 13:38:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResultAnswers](
	[ResultAnswerId] [int] IDENTITY(1,1) NOT NULL,
	[ResultId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[AnswerId] [int] NOT NULL,
 CONSTRAINT [PK_ResultAnswers] PRIMARY KEY CLUSTERED 
(
	[ResultAnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Results]    Script Date: 23.08.2016 13:38:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Results](
	[TestResultId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[TestId] [int] NOT NULL,
	[CorrectCount] [int] NOT NULL,
	[SpentTime] [time](7) NOT NULL,
	[ResultDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Results] PRIMARY KEY CLUSTERED 
(
	[TestResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Students]    Script Date: 23.08.2016 13:38:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[StudentName] [nvarchar](200) NOT NULL,
	[GroupID] [int] NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tests]    Script Date: 23.08.2016 13:38:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[TestId] [int] IDENTITY(1,1) NOT NULL,
	[TestName] [nvarchar](300) NOT NULL,
	[TestTime] [time](1) NOT NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Accesses]  WITH CHECK ADD  CONSTRAINT [FK_Accesses_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([GroupID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accesses] CHECK CONSTRAINT [FK_Accesses_Groups]
GO
ALTER TABLE [dbo].[Accesses]  WITH CHECK ADD  CONSTRAINT [FK_Accesses_Tests] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([TestId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accesses] CHECK CONSTRAINT [FK_Accesses_Tests]
GO
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Answers_Questions] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Questions] ([QuestionId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Answers_Questions]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Tests] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([TestId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Tests]
GO
ALTER TABLE [dbo].[ResultAnswers]  WITH CHECK ADD  CONSTRAINT [FK_ResultAnswers_Answers] FOREIGN KEY([AnswerId])
REFERENCES [dbo].[Answers] ([AnswerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ResultAnswers] CHECK CONSTRAINT [FK_ResultAnswers_Answers]
GO
ALTER TABLE [dbo].[ResultAnswers]  WITH CHECK ADD  CONSTRAINT [FK_ResultAnswers_Questions] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Questions] ([QuestionId])
GO
ALTER TABLE [dbo].[ResultAnswers] CHECK CONSTRAINT [FK_ResultAnswers_Questions]
GO
ALTER TABLE [dbo].[ResultAnswers]  WITH CHECK ADD  CONSTRAINT [FK_ResultAnswers_Results] FOREIGN KEY([ResultId])
REFERENCES [dbo].[Results] ([TestResultId])
GO
ALTER TABLE [dbo].[ResultAnswers] CHECK CONSTRAINT [FK_ResultAnswers_Results]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Results_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK_Results_Students]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Results_Tests] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([TestId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK_Results_Tests]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([GroupID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Groups]
GO
USE [master]
GO
ALTER DATABASE [StudentTestDB] SET  READ_WRITE 
GO
