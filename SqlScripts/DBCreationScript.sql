USE [master]
GO
/****** Object:  Database [StudentTestDB]    Script Date: 8/11/2016 12:25:07 PM ******/
CREATE DATABASE [StudentTestDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StudentTestDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\StudentTestDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'StudentTestDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\StudentTestDB_log.ldf' , SIZE = 2816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
USE [StudentTestDB]
GO
/****** Object:  Table [dbo].[Accesses]    Script Date: 8/11/2016 12:25:07 PM ******/
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
/****** Object:  Table [dbo].[Answers]    Script Date: 8/11/2016 12:25:07 PM ******/
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
/****** Object:  Table [dbo].[Groups]    Script Date: 8/11/2016 12:25:07 PM ******/
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
/****** Object:  Table [dbo].[Questions]    Script Date: 8/11/2016 12:25:07 PM ******/
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
/****** Object:  Table [dbo].[ResultAnswers]    Script Date: 8/11/2016 12:25:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResultAnswers](
	[ResultId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[AnswerId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Results]    Script Date: 8/11/2016 12:25:07 PM ******/
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
/****** Object:  Table [dbo].[Students]    Script Date: 8/11/2016 12:25:07 PM ******/
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
/****** Object:  Table [dbo].[Tests]    Script Date: 8/11/2016 12:25:07 PM ******/
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
SET IDENTITY_INSERT [dbo].[Accesses] ON 

INSERT [dbo].[Accesses] ([AccessId], [GroupId], [TestId]) VALUES (2, 1, 4)
INSERT [dbo].[Accesses] ([AccessId], [GroupId], [TestId]) VALUES (3, 2, 1)
INSERT [dbo].[Accesses] ([AccessId], [GroupId], [TestId]) VALUES (5, 1, 1)
INSERT [dbo].[Accesses] ([AccessId], [GroupId], [TestId]) VALUES (8, 6, 18)
INSERT [dbo].[Accesses] ([AccessId], [GroupId], [TestId]) VALUES (9, 3, 18)
INSERT [dbo].[Accesses] ([AccessId], [GroupId], [TestId]) VALUES (10, 2, 18)
INSERT [dbo].[Accesses] ([AccessId], [GroupId], [TestId]) VALUES (11, 1, 18)
SET IDENTITY_INSERT [dbo].[Accesses] OFF
SET IDENTITY_INSERT [dbo].[Answers] ON 

INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (1, 1, N'враховує в основному цінність інформації, її корисність', NULL, 0)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (2, 1, N'оцінює інформацію з точки зору невизначеності, яка знімається після отримання інформації', NULL, 1)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (4, 1, N'враховує структуру побудови окремих інформаційних масивів, при цьому за одиницю інформації приймаються певні елементарні одиниці – кванти, а кількість інформації визначається простим підрахунком квантів у масиві даних', NULL, 0)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (6, 1, N'враховує формат розташування даних у комп’ютері і визначає кількість у байтовому форматі', NULL, 0)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (7, 2, N'за Шенноном', NULL, 1)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (8, 2, N'за Хартлі', NULL, 0)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (10, 2, N'семантичною', NULL, 0)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (11, 2, N'структурною', NULL, 0)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (12, 3, N'2', NULL, 0)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (13, 3, N'4', NULL, 0)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (14, 3, N'8', NULL, 0)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (15, 3, N'16', NULL, 1)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (22, 5, N'ячсмсячм', NULL, 1)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (23, 5, N'счямсч', NULL, 0)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (24, 5, N'чясмчся', NULL, 0)
INSERT [dbo].[Answers] ([AnswerId], [QuestionId], [AnswerText], [AnswerImage], [AnswerIsCorrect]) VALUES (25, 5, N'чсямчсям', NULL, 0)
SET IDENTITY_INSERT [dbo].[Answers] OFF
SET IDENTITY_INSERT [dbo].[Groups] ON 

INSERT [dbo].[Groups] ([GroupID], [GroupName]) VALUES (1, N'2СІ-12б')
INSERT [dbo].[Groups] ([GroupID], [GroupName]) VALUES (2, N'1СІ-12б')
INSERT [dbo].[Groups] ([GroupID], [GroupName]) VALUES (3, N'2АВ-12б')
INSERT [dbo].[Groups] ([GroupID], [GroupName]) VALUES (6, N'1АВ-12б')
SET IDENTITY_INSERT [dbo].[Groups] OFF
SET IDENTITY_INSERT [dbo].[Questions] ON 

INSERT [dbo].[Questions] ([QuestionId], [QuestionText], [QuestionImage], [TestId]) VALUES (1, N'Статистична теорія…', NULL, 1)
INSERT [dbo].[Questions] ([QuestionId], [QuestionText], [QuestionImage], [TestId]) VALUES (2, N'Міра інформації, яка визначається формулою - є мірою інформації', 0xFFD8FFE000104A46494600010101006000600000FFDB0043000302020302020303030304030304050805050404050A070706080C0A0C0C0B0A0B0B0D0E12100D0E110E0B0B1016101113141515150C0F171816141812141514FFDB00430103040405040509050509140D0B0D1414141414141414141414141414141414141414141414141414141414141414141414141414141414141414141414141414FFC0001108003E009303012200021101031101FFC4001F0000010501010101010100000000000000000102030405060708090A0BFFC400B5100002010303020403050504040000017D01020300041105122131410613516107227114328191A1082342B1C11552D1F02433627282090A161718191A25262728292A3435363738393A434445464748494A535455565758595A636465666768696A737475767778797A838485868788898A92939495969798999AA2A3A4A5A6A7A8A9AAB2B3B4B5B6B7B8B9BAC2C3C4C5C6C7C8C9CAD2D3D4D5D6D7D8D9DAE1E2E3E4E5E6E7E8E9EAF1F2F3F4F5F6F7F8F9FAFFC4001F0100030101010101010101010000000000000102030405060708090A0BFFC400B51100020102040403040705040400010277000102031104052131061241510761711322328108144291A1B1C109233352F0156272D10A162434E125F11718191A262728292A35363738393A434445464748494A535455565758595A636465666768696A737475767778797A82838485868788898A92939495969798999AA2A3A4A5A6A7A8A9AAB2B3B4B5B6B7B8B9BAC2C3C4C5C6C7C8C9CAD2D3D4D5D6D7D8D9DAE2E3E4E5E6E7E8E9EAF2F3F4F5F6F7F8F9FAFFDA000C03010002110311003F00FD53A28A2800A28A2800A28A28019452126BE57F177ED11F11B43F8A5ABE91A469DE1DD43C3B6FAEFF00665BDC5E433DBC8618B4DFB55E4B24A662A120629BE658C8E446B1BB92521C95ECC3A1F557A518E2BCFBE08F8E2EFE247C25F08F8A2FA6D3EE2F756D361BC964D2F8B6DCEB921079926DC742BBDB0411B8E335E83EB4DAB369F4262D495D0EA28A2A8A0A28A2800A28A2800A28A2800A28A28019C6E15C247F19BC0575E346F07C3E3AF0D4DE2C595A13A0C7AC5BB5F89154B327901FCCDC141246DC8009AEEB1B54FD2BC0BE08CD078EBE327C61F1E048E78E2D4A0F08E9F3ED1916F6316E9C29CF437571700F4CF963AE054ADEDF316CAE7D03452357CE3E3CFDAABFE10DF186ADA37F6DFC1BB7FB0CE61F2B5EF89FFD9D7CB8ED35B7F6749E53FF00B3BDBEB4EFAD867D1A0D3ABCF7E0FF00C48FF85A1E116D68DDF856F7172F079BE0EF10FF006DD97CA14E3ED1E443F3F3CA6CE38E4E6AA7883E3C786BC3BAC5CE9977A678CE6B8B590C724963E07D6EF20247749A1B378E41FED2311EF4DEE25A9CD7ED31FB52689FB2DE83A7EBDE25F0B78A35BD0EEA43049A8787ED6DE78AD24E362CDE64F194DF93B4E08246320900DAD2FE3A788B5CB1B5BFD3BE0CF8E6FB4FBA8967B7BAB7D47C3CF1CD1B00CAEAC355C15208208EA0D7511C9E18F8E5E05D4EC6FF0046BEBEF0EEA0B2595DE9FE22D12EF4E79570320C175147263904385C64641C8E3E4BF83ADE32FD87FE38693F07F52B7D57C5DF05FC5F7AD1F83F578E36B89F47B960D23DA4C1173B3EF313F7428320C0F342CC77B3DDEC37B5D74DCFA5BC37F1C2E35CF88D67E0BD67E1D78BFC1DA8DEE9F3EA56B75AD2D84B693470BC68E825B4BB9C0901954ED3838E7D33EB0ABF28C5675C69769797965773421EE6CD99A0932728594AB7E6091CD7927ED69E2AF13F857E10C92F82F58BBD23C6579A95969BA39B54B67F3EEAE2758523905C432AF97F39762ABB808C9040068BECBBB125BEBA1EDDC52FE95F3C687E23F881A0FED31A0F85751F15FF00C24DA6EA5E1BBCD5B59D2D6C2DE1B6D22449A18ED9ADDD104DB642665DB33C85BCB761B71B57DD753BCFECDD3EEEEFC99AE7C889A5F26DD37CB26D04ED45EEC71803B934FB3E8C356EC733F143E2A784FE0CF84EEBC4FE34D72DF41D12DD9524BAB8DCC599BEEA22282F239C1C2A0278271C1AEB2CEEA2BAB78A789B7C52A8746C11904641E7DABE76FDAB3C55FF0009BFEC33E3FD7BFB1F56D006A3E1A967FECBD72D7ECD7B6DB87FAB9A2C9D8E3B8C9C543E1BF1478B3E3778DBC6DE14F0CF8E2FBE1F5AF81EDAC6C239B4BB1B3BA9AF2F2E2D56732DC0BA8651E4A028AA9188D98890993950AB5BBD355F90EDA2D77BFE163E990D4AB9EF5F17D8FED41E2CF147C31F865A9F8A35987E17D8EA5AEEA5A078ABC5F60B6E60B2B8B4F3922F29AED248A18EE25871BE5470BCA020B2B57B17ECAFE3EF197C48F01DEEB7E2AD42C754B2FED1B8B3D1EFACF4B7B36D42CE195A38EF598CAC9289D42C81A348D393B41520D3DFA89FBBB9EDE4F345784FED25E26F1AE93AD7C33D1FE1FEB3269DE23D6FC42B6CF692456F25A4F651C6D3DD3DC7990BC811638881E4B46C5A4037648C47F08FC4DE318FF680F883E0ED57C51378CBC3BA3E97A6DDB5EDD595B5BBD86A1706667B44F2234CA794B148164DF22874CBB6EC916A37A1EFB4514530317C41A5DCEB5A3DDD8DAEAD79A15CCE9B5351D3D2169E039FBC8268E48C9FF791873D2B82F843F05F4BF81BE01BCF0D0F11EADE23D0FCDB8BB927F13B5A48E9E6B349397922822F30333BBB34BB9BE63F36381EABB8D26E34ADBF985F63C7FF0065BD6BC35AC7C1DD357C257FA55EE8967777D6B08D1668A4B68156EA5291AF964AA808C8428E8A57B115EC47A52D34F143D5DC5B0EA28A298CE7BC55E26D33C17E1DBED775ABD8F4FD26C22334F712E708A3D8724938014024920004902B84F86BE1DD5BC4DAF37C43F1869E6C358B885ADF44D1E600C9A2583ED251C8247DA662AAF291C2ED48C12232CF4FF680FD9E6CFF00680D3F47B4D4BC6BE32F095A69374BA8471F84EFA1B4792E10831CAEED0492168C8CA85600139C12148F2CD37F61F8B56D36D6FE0FDA1FF6828E0BA89668D6EBC5F25BCA1580203C525B2BA373CAB286078201A98EEDB1BD9247D3FAC6BDA7E8B369B6F7D7696F3EA573F63B38D89DD3CDE5BC9B140EFB2391BD8213DABE7CF8F4973F14BE3B7C36F045B378CFC3B63A1DDCFAE5C789B49F0E4D2DB477AB0797690ADCCD6935B1CACF3B333651766D243900667ECFBFB32E8763F1122F1D5E78DBE2C789B5EF09DD5EE936D61F113C416FA9476CF2468B24B10895800F1B21043838237282303EB05A2CB462EE8F13F1178774EFD9EFE14FC43F185BEAB2DFF889AC27D4AFFC4BE256134D77345137922511792AB12F0AB143E522EE2542B3331F47F06DAB59F847448649FED4F159C2A671732DC0908419612CAEF2480F5DCECCC7A924F35D2FE347E34FF5176F2383F8C5F0AECBE35FC37D67C15A9EAFA968FA66AF17D9EEE7D24C0276889F99019A291406E8485CFA115C6B7ECC76767E245F12687E3AF16785FC5371A6C3A66ADAC697FD9FE66B51C20085EEA196D1E032C632AB245146C158AE71803DB873494D69AA1DCF937F680F843A2E996FF0F7C27629F12BC11E0DD1E1BC787C47F0C24BBB8BE86E1F6FFA3CE96F1CF3B2CA0CB234CD1B65D402E0B9DDE81FB30783EF3C23E1FD6447E2DF1FF8B7C3D75708FA7DC7C48F37FB555C0226204D14532424EC0A92C6A728EC328EAC7DCA97F1A23B3F316F6F23E54F1169FA8FC66FDAB205B2D4BC75E04B3F06E8B716D61AE5AF870C56F797771305BB58E7BEB196DD912382101860BF9998CB2AB13D9EBDA1E99F0763F87BE11D0AFA3B47F1378A43DFDD6A77971F6FD5255865BA9A669E39A366959A040436E8CA911797E5E147BB6D1C71C52FB0A5D12EC0F56D8514FA298C28A28A0028A28A0028A28A00E13E30786751F1C7C2BF16F87B49874FB8D4756D2EE2C604D55996D4B4B194FDE15473B7E6CF0ADD3A5797F8C3F635F07F8FBC456BAEDFCB1D8DE436D6D6E2D63F0EE817E91886354555B8BCD325B860028196938EC17A0FA2E93D2975BA03CD7E15E83E29D0F54F1949E24D3F48B28750D59AFAC1B4BD4A5BB6688C691E250F6D088DB1129C2971F3119F9727D229D814D5FBC68EC2B6E3E8A28A630A28A2800A28A2800A28A2800A28A2803FFFD9, 1)
INSERT [dbo].[Questions] ([QuestionId], [QuestionText], [QuestionImage], [TestId]) VALUES (3, N'Відомо, що кожне з т можливих повідомлень несе 4 біти інформації. Чому дорівнює т?', NULL, 1)
INSERT [dbo].[Questions] ([QuestionId], [QuestionText], [QuestionImage], [TestId]) VALUES (5, N'ФФФФ', NULL, 4)
SET IDENTITY_INSERT [dbo].[Questions] OFF
INSERT [dbo].[ResultAnswers] ([ResultId], [QuestionId], [AnswerId]) VALUES (2, 1, 2)
INSERT [dbo].[ResultAnswers] ([ResultId], [QuestionId], [AnswerId]) VALUES (2, 2, 7)
INSERT [dbo].[ResultAnswers] ([ResultId], [QuestionId], [AnswerId]) VALUES (2, 3, 15)
SET IDENTITY_INSERT [dbo].[Results] ON 

INSERT [dbo].[Results] ([TestResultId], [StudentId], [TestId], [CorrectCount], [SpentTime], [ResultDate]) VALUES (2, 1, 1, 3, CAST(0x07001A7118020000 AS Time), CAST(0x0000A64700000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[Results] OFF
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([StudentID], [StudentName], [GroupID]) VALUES (1, N'Луп''як Дмитро', 1)
INSERT [dbo].[Students] ([StudentID], [StudentName], [GroupID]) VALUES (2, N'Величко Петро', 1)
INSERT [dbo].[Students] ([StudentID], [StudentName], [GroupID]) VALUES (3, N'Шевчук Віталій', 2)
INSERT [dbo].[Students] ([StudentID], [StudentName], [GroupID]) VALUES (8, N'Дрозд Інна', 1)
INSERT [dbo].[Students] ([StudentID], [StudentName], [GroupID]) VALUES (28, N'Резніченко Роман', 2)
INSERT [dbo].[Students] ([StudentID], [StudentName], [GroupID]) VALUES (29, N'Алєксєєнко Анна', 6)
INSERT [dbo].[Students] ([StudentID], [StudentName], [GroupID]) VALUES (30, N'Лендьєл Максим', 1)
SET IDENTITY_INSERT [dbo].[Students] OFF
SET IDENTITY_INSERT [dbo].[Tests] ON 

INSERT [dbo].[Tests] ([TestId], [TestName], [TestTime]) VALUES (1, N'ОЗПОІ - Колоквіум 1', CAST(0x0178690000000000 AS Time))
INSERT [dbo].[Tests] ([TestId], [TestName], [TestTime]) VALUES (4, N'ОЗПОІ - Колоквіум 2', CAST(0x0178690000000000 AS Time))
INSERT [dbo].[Tests] ([TestId], [TestName], [TestTime]) VALUES (18, N'ТАУ', CAST(0x0100000000000000 AS Time))
SET IDENTITY_INSERT [dbo].[Tests] OFF
ALTER TABLE [dbo].[Accesses]  WITH CHECK ADD  CONSTRAINT [FK_Accesses_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([GroupID])
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
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Answers_Questions]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Tests] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([TestId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Tests]
GO
ALTER TABLE [dbo].[ResultAnswers]  WITH CHECK ADD  CONSTRAINT [FK_ResultAnswers_Answers] FOREIGN KEY([AnswerId])
REFERENCES [dbo].[Answers] ([AnswerId])
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
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK_Results_Students]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Results_Tests] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([TestId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK_Results_Tests]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([GroupID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Groups]
GO
USE [master]
GO
ALTER DATABASE [StudentTestDB] SET  READ_WRITE 
GO
