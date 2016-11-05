CREATE TABLE [dbo].[Cargo]
(
	[ID]		[int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Codigo]	[int] NULL,
	[Descricao] [varchar](150) NOT NULL
) 
GO


CREATE TABLE [dbo].[Cidade]
(
	[ID]		[int]		IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Nome]		[varchar](50) NOT NULL,
	[EstadoID]	[int] NOT NULL,
)
GO

ALTER TABLE [dbo].[Cidade]  WITH CHECK ADD  CONSTRAINT [FK_Cidade_Estado] FOREIGN KEY([EstadoID])
	REFERENCES [dbo].[Estado] ([ID])
GO

ALTER TABLE [dbo].[Cidade] CHECK CONSTRAINT [FK_Cidade_Estado]
GO

CREATE TABLE [dbo].[Eleicao]
(
	[ID] [int] IDENTITY(1,1) NOT NULL	PRIMARY KEY,
	[Ano] [int] NOT NULL,
	[Descricao] [varchar](50) NOT NULL
)
GO


CREATE TABLE [dbo].[EleicaoCargo]
(
	[ID] [int] IDENTITY(1,1) NOT NULL	PRIMARY KEY,
	[EleicaoID] [int] NOT NULL,
	[CargoID] [int] NOT NULL,

)
GO

ALTER TABLE [dbo].[EleicaoCargo]  WITH CHECK ADD  CONSTRAINT [FK_EleicaoCargo_Cargo1] FOREIGN KEY([CargoID])
	REFERENCES [dbo].[Cargo] ([ID])
GO

ALTER TABLE [dbo].[EleicaoCargo] CHECK CONSTRAINT [FK_EleicaoCargo_Cargo1]
GO

ALTER TABLE [dbo].[EleicaoCargo]  WITH CHECK ADD  CONSTRAINT [FK_EleicaoCargo_Eleicao1] FOREIGN KEY([EleicaoID])
	REFERENCES [dbo].[Eleicao] ([ID])
GO

ALTER TABLE [dbo].[EleicaoCargo] CHECK CONSTRAINT [FK_EleicaoCargo_Eleicao1]
GO


CREATE TABLE [dbo].[Estado]
(
	[ID] [int] IDENTITY(1,1) NOT NULL	PRIMARY KEY,
	[Sigla] [varchar](2) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[PaisID] [int] NOT NULL,
	[SiglaUE] [varchar](10) NULL,

) 
GO

ALTER TABLE [dbo].[Estado]  WITH CHECK ADD  CONSTRAINT [FK_Estado_Pais] FOREIGN KEY([PaisID])
	REFERENCES [dbo].[Pais] ([ID])
GO

ALTER TABLE [dbo].[Estado] CHECK CONSTRAINT [FK_Estado_Pais]
GO

