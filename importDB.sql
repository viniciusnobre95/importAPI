USE [ImportDB]
GO
/****** Object:  User [import]    Script Date: 16/12/2021 12:11:35 ******/
CREATE USER [import] FOR LOGIN [import] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [import]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [import]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [import]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [import]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [import]
GO
ALTER ROLE [db_datareader] ADD MEMBER [import]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [import]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [import]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [import]
GO
/****** Object:  Table [dbo].[tblArquivo]    Script Date: 16/12/2021 12:11:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblArquivo](
	[ArquivoId] [int] NOT NULL,
	[QuantidadeRegistros] [int] NULL,
	[QuantidadeRegistrosImportados] [int] NULL,
	[DTH_Import] [datetime] NOT NULL,
 CONSTRAINT [PK_tblArquivo] PRIMARY KEY CLUSTERED 
(
	[ArquivoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblRegistro]    Script Date: 16/12/2021 12:11:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblRegistro](
	[ArquivoId] [int] NOT NULL,
	[Tconst] [varchar](100) NOT NULL,
	[TitleType] [varchar](50) NULL,
	[PrimaryTitle] [varchar](500) NULL,
	[OriginalTitle] [varchar](500) NULL,
	[IsAdult] [varchar](2) NULL,
	[StartYear] [varchar](5) NULL,
	[EndYear] [varchar](5) NULL,
	[RuntimeMinutes] [varchar](5) NULL,
	[Genres] [varchar](100) NULL,
	[DTH_Insert] [datetime] NULL,
 CONSTRAINT [PK_tblRegistro] PRIMARY KEY CLUSTERED 
(
	[ArquivoId] ASC,
	[Tconst] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblRegistro]  WITH CHECK ADD  CONSTRAINT [FK_tblRegistro_tblArquivo] FOREIGN KEY([ArquivoId])
REFERENCES [dbo].[tblArquivo] ([ArquivoId])
GO
ALTER TABLE [dbo].[tblRegistro] CHECK CONSTRAINT [FK_tblRegistro_tblArquivo]
GO
