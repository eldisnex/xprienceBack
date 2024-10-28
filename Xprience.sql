USE [master]
GO
/****** Object:  Database [Xprience]    Script Date: 21/10/2024 15:37:16 ******/
CREATE DATABASE [Xprience]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Xprience', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS02\MSSQL\DATA\Xprience.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Xprience_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS02\MSSQL\DATA\Xprience_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Xprience] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Xprience].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Xprience] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Xprience] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Xprience] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Xprience] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Xprience] SET ARITHABORT OFF 
GO
ALTER DATABASE [Xprience] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Xprience] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Xprience] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Xprience] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Xprience] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Xprience] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Xprience] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Xprience] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Xprience] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Xprience] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Xprience] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Xprience] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Xprience] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Xprience] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Xprience] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Xprience] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Xprience] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Xprience] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Xprience] SET  MULTI_USER 
GO
ALTER DATABASE [Xprience] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Xprience] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Xprience] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Xprience] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Xprience] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Xprience] SET QUERY_STORE = OFF
GO
USE [Xprience]
GO
/****** Object:  Table [dbo].[Explore]    Script Date: 21/10/2024 15:37:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Explore](
	[idExplore] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[idPlan] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idExplore] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Folder]    Script Date: 21/10/2024 15:37:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Folder](
	[idFolder] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idFolder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FolderUser]    Script Date: 21/10/2024 15:37:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FolderUser](
	[idFolderUser] [int] IDENTITY(1,1) NOT NULL,
	[idPlanUser] [int] NULL,
	[idFolder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idFolderUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plan]    Script Date: 21/10/2024 15:37:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plan](
	[idPlan] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[gastronomy] [varchar](50) NULL,
	[entertainment] [varchar](50) NULL,
	[idUser] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idPlan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlanUser]    Script Date: 21/10/2024 15:37:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanUser](
	[idPlanUser] [int] IDENTITY(1,1) NOT NULL,
	[idPlan] [int] NULL,
	[idUser] [int] NULL,
	[IsFav] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idPlanUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 21/10/2024 15:37:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[idUser] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[mail] [varchar](50) NULL,
	[password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Explore]  WITH CHECK ADD FOREIGN KEY([idPlan])
REFERENCES [dbo].[Plan] ([idPlan])
GO
ALTER TABLE [dbo].[FolderUser]  WITH CHECK ADD FOREIGN KEY([idFolder])
REFERENCES [dbo].[Folder] ([idFolder])
GO
ALTER TABLE [dbo].[FolderUser]  WITH CHECK ADD FOREIGN KEY([idPlanUser])
REFERENCES [dbo].[PlanUser] ([idPlanUser])
GO
ALTER TABLE [dbo].[Plan]  WITH CHECK ADD FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[PlanUser]  WITH CHECK ADD FOREIGN KEY([idPlan])
REFERENCES [dbo].[Plan] ([idPlan])
GO
ALTER TABLE [dbo].[PlanUser]  WITH CHECK ADD FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO
USE [master]
GO
ALTER DATABASE [Xprience] SET  READ_WRITE 
GO
