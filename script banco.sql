USE [master]
GO

CREATE DATABASE PetShop;

USE [PetShop]
GO
/****** Object:  Table [dbo].[Pessoa]    Script Date: 05/01/2023 14:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pessoa](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](30) NOT NULL,
	[DataNascimento] [date] NOT NULL,
	[Sexo] [varchar](1) NOT NULL,
	[Ide] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ide] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pet]    Script Date: 05/01/2023 14:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pet](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](30) NOT NULL,
	[DataNascimento] [datetime] NOT NULL,
	[Sexo] [varchar](1) NOT NULL,
	[QuantidadeVacinas] [int] NOT NULL,
	[IdeRaca] [uniqueidentifier] NOT NULL,
	[CodigoRaca] [int] NOT NULL,
	[Ide] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ide] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PetsDaPessoa]    Script Date: 05/01/2023 14:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PetsDaPessoa](
	[CodigoPessoa] [int] NOT NULL,
	[IdePessoa] [uniqueidentifier] NOT NULL,
	[CodigoPet] [int] NOT NULL,
	[IdePet] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdePessoa] ASC,
	[IdePet] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Raca]    Script Date: 05/01/2023 14:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Raca](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](30) NOT NULL,
	[Ide] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ide] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pet]  WITH CHECK ADD FOREIGN KEY([CodigoRaca])
REFERENCES [dbo].[Raca] ([Codigo])
GO
ALTER TABLE [dbo].[Pet]  WITH CHECK ADD FOREIGN KEY([IdeRaca])
REFERENCES [dbo].[Raca] ([Ide])
GO
ALTER TABLE [dbo].[PetsDaPessoa]  WITH CHECK ADD FOREIGN KEY([IdePessoa])
REFERENCES [dbo].[Pessoa] ([Ide])
GO
ALTER TABLE [dbo].[PetsDaPessoa]  WITH CHECK ADD FOREIGN KEY([CodigoPessoa])
REFERENCES [dbo].[Pessoa] ([Codigo])
GO
ALTER TABLE [dbo].[PetsDaPessoa]  WITH CHECK ADD FOREIGN KEY([CodigoPet])
REFERENCES [dbo].[Pet] ([Codigo])
GO
ALTER TABLE [dbo].[PetsDaPessoa]  WITH CHECK ADD FOREIGN KEY([IdePet])
REFERENCES [dbo].[Pet] ([Ide])
GO
ALTER TABLE [dbo].[Pessoa]  WITH CHECK ADD CHECK  (([sexo]='O' OR [sexo]='F' OR [sexo]='M'))
GO
ALTER TABLE [dbo].[Pet]  WITH CHECK ADD CHECK  (([Sexo]='F' OR [Sexo]='M'))
GO
