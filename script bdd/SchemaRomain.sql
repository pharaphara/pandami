USE [master]
GO
/****** Object:  Database [PandamiV1]    Script Date: 01/02/2021 18:44:47 ******/
CREATE DATABASE [PandamiV1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PandamiV1', FILENAME = N'C:\Users\PC\PandamiV1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PandamiV1_log', FILENAME = N'C:\Users\PC\PandamiV1_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PandamiV1] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PandamiV1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PandamiV1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PandamiV1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PandamiV1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PandamiV1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PandamiV1] SET ARITHABORT OFF 
GO
ALTER DATABASE [PandamiV1] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PandamiV1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PandamiV1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PandamiV1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PandamiV1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PandamiV1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PandamiV1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PandamiV1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PandamiV1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PandamiV1] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PandamiV1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PandamiV1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PandamiV1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PandamiV1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PandamiV1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PandamiV1] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [PandamiV1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PandamiV1] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PandamiV1] SET  MULTI_USER 
GO
ALTER DATABASE [PandamiV1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PandamiV1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PandamiV1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PandamiV1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PandamiV1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PandamiV1] SET QUERY_STORE = OFF
GO
USE [PandamiV1]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [PandamiV1]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Adresses]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroDeVoie] [nvarchar](max) NULL,
	[NomDeVoie] [nvarchar](max) NULL,
	[CodePostal] [nvarchar](max) NULL,
	[Ville] [nvarchar](max) NULL,
	[latitude] [real] NOT NULL,
	[longitude] [real] NOT NULL,
 CONSTRAINT [PK_Adresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategorieAides]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategorieAides](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Categorie] [nvarchar](max) NULL,
 CONSTRAINT [PK_CategorieAides] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Disponibilites]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Disponibilites](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DebutHeure] [datetime2](7) NOT NULL,
	[FinHeure] [datetime2](7) NOT NULL,
	[ValiditeDebutDate] [datetime2](7) NOT NULL,
	[ValiditeFinDate] [datetime2](7) NULL,
	[JourId] [int] NULL,
	[membreId] [int] NULL,
 CONSTRAINT [PK_Disponibilites] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feats]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feats](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[RealisationDate] [datetime2](7) NOT NULL,
	[HeureDebut] [datetime2](7) NOT NULL,
	[HeureFin] [datetime2](7) NOT NULL,
	[AcceptationDate] [datetime2](7) NULL,
	[EnCoursRealisation] [datetime2](7) NULL,
	[SurPlace] [datetime2](7) NULL,
	[FinFeatHelper] [datetime2](7) NULL,
	[ClotureDate] [datetime2](7) NULL,
	[SommePrevoir] [real] NULL,
	[SommeAvancee] [real] NULL,
	[SommeRembourseeDate] [datetime2](7) NULL,
	[AnnulationDate] [datetime2](7) NULL,
	[EchangeMonetaire] [bit] NOT NULL,
	[CreateurId] [int] NULL,
	[TypeId] [int] NULL,
	[MaterielId] [int] NULL,
	[AdresseId] [int] NULL,
 CONSTRAINT [PK_Feats] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JourDeLaSemaines]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JourDeLaSemaines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomDuJour] [nvarchar](max) NULL,
 CONSTRAINT [PK_JourDeLaSemaines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Litiges]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Litiges](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[ClotureDate] [datetime2](7) NULL,
	[Commentaire] [nvarchar](max) NULL,
	[TypeLitigeId] [int] NULL,
	[CreateurId] [int] NULL,
	[FeatId] [int] NULL,
 CONSTRAINT [PK_Litiges] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Materiels]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Materiels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomMateriel] [nvarchar](max) NULL,
 CONSTRAINT [PK_Materiels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Membres]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Membres](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Nom] [nvarchar](50) NULL,
	[Prenom] [nvarchar](50) NULL,
	[Naissance] [datetime2](7) NOT NULL,
	[Telephone] [nvarchar](max) NULL,
	[Inscription] [datetime2](7) NOT NULL,
	[Mdp] [nvarchar](max) NULL,
	[SexeId] [int] NULL,
	[AdresseId] [int] NULL,
 CONSTRAINT [PK_Membres] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Negociations]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Negociations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateCreationNegociation] [datetime2](7) NOT NULL,
	[DateClotureNegociation] [datetime2](7) NULL,
	[NewDateProposee] [datetime2](7) NOT NULL,
	[HeureDbtProposee] [datetime2](7) NOT NULL,
	[HeureFinProposee] [datetime2](7) NOT NULL,
	[IsAccepted] [bit] NOT NULL,
	[featId] [int] NULL,
	[DemandeurId] [int] NOT NULL,
	[RepondeurId] [int] NOT NULL,
 CONSTRAINT [PK_Negociations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PreferenceAides]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PreferenceAides](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MembreId] [int] NULL,
	[TypeAideId] [int] NULL,
	[ValiditeDebut] [datetime2](7) NOT NULL,
	[ValiditeFin] [datetime2](7) NULL,
 CONSTRAINT [PK_PreferenceAides] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RayonActions]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RayonActions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rayon] [real] NOT NULL,
	[ValiditeDebut] [datetime2](7) NOT NULL,
	[ValiditeFin] [datetime2](7) NULL,
	[MembreId] [int] NULL,
 CONSTRAINT [PK_RayonActions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReponseHelpers]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReponseHelpers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AcceptationDate] [datetime2](7) NOT NULL,
	[DesistementDate] [datetime2](7) NULL,
	[HelperId] [int] NULL,
	[FeatId] [int] NULL,
 CONSTRAINT [PK_ReponseHelpers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sexes]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sexes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomSexe] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sexes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suspensions]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suspensions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DebutDate] [datetime2](7) NOT NULL,
	[FinDate] [datetime2](7) NULL,
	[MembreId] [int] NULL,
 CONSTRAINT [PK_Suspensions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeAides]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeAides](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomAide] [nvarchar](max) NULL,
	[CategorieAideId] [int] NULL,
 CONSTRAINT [PK_TypeAides] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeLitiges]    Script Date: 01/02/2021 18:44:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeLitiges](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Libelle] [nvarchar](max) NULL,
 CONSTRAINT [PK_TypeLitiges] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Disponibilites_JourId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Disponibilites_JourId] ON [dbo].[Disponibilites]
(
	[JourId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Disponibilites_membreId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Disponibilites_membreId] ON [dbo].[Disponibilites]
(
	[membreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Feats_AdresseId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Feats_AdresseId] ON [dbo].[Feats]
(
	[AdresseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Feats_CreateurId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Feats_CreateurId] ON [dbo].[Feats]
(
	[CreateurId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Feats_MaterielId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Feats_MaterielId] ON [dbo].[Feats]
(
	[MaterielId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Feats_TypeId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Feats_TypeId] ON [dbo].[Feats]
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Litiges_CreateurId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Litiges_CreateurId] ON [dbo].[Litiges]
(
	[CreateurId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Litiges_FeatId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Litiges_FeatId] ON [dbo].[Litiges]
(
	[FeatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Litiges_TypeLitigeId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Litiges_TypeLitigeId] ON [dbo].[Litiges]
(
	[TypeLitigeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Membres_AdresseId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Membres_AdresseId] ON [dbo].[Membres]
(
	[AdresseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Membres_SexeId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Membres_SexeId] ON [dbo].[Membres]
(
	[SexeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Negociations_DemandeurId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Negociations_DemandeurId] ON [dbo].[Negociations]
(
	[DemandeurId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Negociations_featId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Negociations_featId] ON [dbo].[Negociations]
(
	[featId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Negociations_RepondeurId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Negociations_RepondeurId] ON [dbo].[Negociations]
(
	[RepondeurId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PreferenceAides_MembreId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_PreferenceAides_MembreId] ON [dbo].[PreferenceAides]
(
	[MembreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PreferenceAides_TypeAideId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_PreferenceAides_TypeAideId] ON [dbo].[PreferenceAides]
(
	[TypeAideId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RayonActions_MembreId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_RayonActions_MembreId] ON [dbo].[RayonActions]
(
	[MembreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ReponseHelpers_FeatId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_ReponseHelpers_FeatId] ON [dbo].[ReponseHelpers]
(
	[FeatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ReponseHelpers_HelperId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_ReponseHelpers_HelperId] ON [dbo].[ReponseHelpers]
(
	[HelperId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Suspensions_MembreId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_Suspensions_MembreId] ON [dbo].[Suspensions]
(
	[MembreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TypeAides_CategorieAideId]    Script Date: 01/02/2021 18:44:47 ******/
CREATE NONCLUSTERED INDEX [IX_TypeAides_CategorieAideId] ON [dbo].[TypeAides]
(
	[CategorieAideId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Disponibilites]  WITH CHECK ADD  CONSTRAINT [FK_Disponibilites_JourDeLaSemaines_JourId] FOREIGN KEY([JourId])
REFERENCES [dbo].[JourDeLaSemaines] ([Id])
GO
ALTER TABLE [dbo].[Disponibilites] CHECK CONSTRAINT [FK_Disponibilites_JourDeLaSemaines_JourId]
GO
ALTER TABLE [dbo].[Disponibilites]  WITH CHECK ADD  CONSTRAINT [FK_Disponibilites_Membres_membreId] FOREIGN KEY([membreId])
REFERENCES [dbo].[Membres] ([Id])
GO
ALTER TABLE [dbo].[Disponibilites] CHECK CONSTRAINT [FK_Disponibilites_Membres_membreId]
GO
ALTER TABLE [dbo].[Feats]  WITH CHECK ADD  CONSTRAINT [FK_Feats_Adresses_AdresseId] FOREIGN KEY([AdresseId])
REFERENCES [dbo].[Adresses] ([Id])
GO
ALTER TABLE [dbo].[Feats] CHECK CONSTRAINT [FK_Feats_Adresses_AdresseId]
GO
ALTER TABLE [dbo].[Feats]  WITH CHECK ADD  CONSTRAINT [FK_Feats_Materiels_MaterielId] FOREIGN KEY([MaterielId])
REFERENCES [dbo].[Materiels] ([Id])
GO
ALTER TABLE [dbo].[Feats] CHECK CONSTRAINT [FK_Feats_Materiels_MaterielId]
GO
ALTER TABLE [dbo].[Feats]  WITH CHECK ADD  CONSTRAINT [FK_Feats_Membres_CreateurId] FOREIGN KEY([CreateurId])
REFERENCES [dbo].[Membres] ([Id])
GO
ALTER TABLE [dbo].[Feats] CHECK CONSTRAINT [FK_Feats_Membres_CreateurId]
GO
ALTER TABLE [dbo].[Feats]  WITH CHECK ADD  CONSTRAINT [FK_Feats_TypeAides_TypeId] FOREIGN KEY([TypeId])
REFERENCES [dbo].[TypeAides] ([Id])
GO
ALTER TABLE [dbo].[Feats] CHECK CONSTRAINT [FK_Feats_TypeAides_TypeId]
GO
ALTER TABLE [dbo].[Litiges]  WITH CHECK ADD  CONSTRAINT [FK_Litiges_Feats_FeatId] FOREIGN KEY([FeatId])
REFERENCES [dbo].[Feats] ([Id])
GO
ALTER TABLE [dbo].[Litiges] CHECK CONSTRAINT [FK_Litiges_Feats_FeatId]
GO
ALTER TABLE [dbo].[Litiges]  WITH CHECK ADD  CONSTRAINT [FK_Litiges_Membres_CreateurId] FOREIGN KEY([CreateurId])
REFERENCES [dbo].[Membres] ([Id])
GO
ALTER TABLE [dbo].[Litiges] CHECK CONSTRAINT [FK_Litiges_Membres_CreateurId]
GO
ALTER TABLE [dbo].[Litiges]  WITH CHECK ADD  CONSTRAINT [FK_Litiges_TypeLitiges_TypeLitigeId] FOREIGN KEY([TypeLitigeId])
REFERENCES [dbo].[TypeLitiges] ([Id])
GO
ALTER TABLE [dbo].[Litiges] CHECK CONSTRAINT [FK_Litiges_TypeLitiges_TypeLitigeId]
GO
ALTER TABLE [dbo].[Membres]  WITH CHECK ADD  CONSTRAINT [FK_Membres_Adresses_AdresseId] FOREIGN KEY([AdresseId])
REFERENCES [dbo].[Adresses] ([Id])
GO
ALTER TABLE [dbo].[Membres] CHECK CONSTRAINT [FK_Membres_Adresses_AdresseId]
GO
ALTER TABLE [dbo].[Membres]  WITH CHECK ADD  CONSTRAINT [FK_Membres_Sexes_SexeId] FOREIGN KEY([SexeId])
REFERENCES [dbo].[Sexes] ([Id])
GO
ALTER TABLE [dbo].[Membres] CHECK CONSTRAINT [FK_Membres_Sexes_SexeId]
GO
ALTER TABLE [dbo].[Negociations]  WITH CHECK ADD  CONSTRAINT [FK_Negociations_Feats_featId] FOREIGN KEY([featId])
REFERENCES [dbo].[Feats] ([Id])
GO
ALTER TABLE [dbo].[Negociations] CHECK CONSTRAINT [FK_Negociations_Feats_featId]
GO
ALTER TABLE [dbo].[Negociations]  WITH CHECK ADD  CONSTRAINT [FK_Negociations_Membres_DemandeurId] FOREIGN KEY([DemandeurId])
REFERENCES [dbo].[Membres] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Negociations] CHECK CONSTRAINT [FK_Negociations_Membres_DemandeurId]
GO
ALTER TABLE [dbo].[Negociations]  WITH CHECK ADD  CONSTRAINT [FK_Negociations_Membres_RepondeurId] FOREIGN KEY([RepondeurId])
REFERENCES [dbo].[Membres] ([Id])
GO
ALTER TABLE [dbo].[Negociations] CHECK CONSTRAINT [FK_Negociations_Membres_RepondeurId]
GO
ALTER TABLE [dbo].[PreferenceAides]  WITH CHECK ADD  CONSTRAINT [FK_PreferenceAides_Membres_MembreId] FOREIGN KEY([MembreId])
REFERENCES [dbo].[Membres] ([Id])
GO
ALTER TABLE [dbo].[PreferenceAides] CHECK CONSTRAINT [FK_PreferenceAides_Membres_MembreId]
GO
ALTER TABLE [dbo].[PreferenceAides]  WITH CHECK ADD  CONSTRAINT [FK_PreferenceAides_TypeAides_TypeAideId] FOREIGN KEY([TypeAideId])
REFERENCES [dbo].[TypeAides] ([Id])
GO
ALTER TABLE [dbo].[PreferenceAides] CHECK CONSTRAINT [FK_PreferenceAides_TypeAides_TypeAideId]
GO
ALTER TABLE [dbo].[RayonActions]  WITH CHECK ADD  CONSTRAINT [FK_RayonActions_Membres_MembreId] FOREIGN KEY([MembreId])
REFERENCES [dbo].[Membres] ([Id])
GO
ALTER TABLE [dbo].[RayonActions] CHECK CONSTRAINT [FK_RayonActions_Membres_MembreId]
GO
ALTER TABLE [dbo].[ReponseHelpers]  WITH CHECK ADD  CONSTRAINT [FK_ReponseHelpers_Feats_FeatId] FOREIGN KEY([FeatId])
REFERENCES [dbo].[Feats] ([Id])
GO
ALTER TABLE [dbo].[ReponseHelpers] CHECK CONSTRAINT [FK_ReponseHelpers_Feats_FeatId]
GO
ALTER TABLE [dbo].[ReponseHelpers]  WITH CHECK ADD  CONSTRAINT [FK_ReponseHelpers_Membres_HelperId] FOREIGN KEY([HelperId])
REFERENCES [dbo].[Membres] ([Id])
GO
ALTER TABLE [dbo].[ReponseHelpers] CHECK CONSTRAINT [FK_ReponseHelpers_Membres_HelperId]
GO
ALTER TABLE [dbo].[Suspensions]  WITH CHECK ADD  CONSTRAINT [FK_Suspensions_Membres_MembreId] FOREIGN KEY([MembreId])
REFERENCES [dbo].[Membres] ([Id])
GO
ALTER TABLE [dbo].[Suspensions] CHECK CONSTRAINT [FK_Suspensions_Membres_MembreId]
GO
ALTER TABLE [dbo].[TypeAides]  WITH CHECK ADD  CONSTRAINT [FK_TypeAides_CategorieAides_CategorieAideId] FOREIGN KEY([CategorieAideId])
REFERENCES [dbo].[CategorieAides] ([Id])
GO
ALTER TABLE [dbo].[TypeAides] CHECK CONSTRAINT [FK_TypeAides_CategorieAides_CategorieAideId]
GO
USE [master]
GO
ALTER DATABASE [PandamiV1] SET  READ_WRITE 
GO
