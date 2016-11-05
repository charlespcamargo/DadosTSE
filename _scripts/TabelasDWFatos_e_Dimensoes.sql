/************************************************************** CRIAÇÃO DA DIMENSÃO CANDIDATO ********************************************************************/
IF EXISTS (SELECT 1 FROM sys.Objects WHERE  Object_id = OBJECT_ID(N'[TDCandidato]') AND Type = N'U')
BEGIN
   DROP TABLE [TDCandidato]
END
GO

CREATE TABLE [dbo].[TDCandidato]
(
	[ID]	[int] IDENTITY(1,1) NOT NULL,
	[CPF]	[varchar](11)		NOT NULL,
	[Nome]	[varchar](30)		NOT NULL,
	[Sexo]	[varchar](20)		NOT NULL,
	
	CONSTRAINT [PK_TDCandidato] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY]
GO
/************************************************************** CRIAÇÃO DA DIMENSÃO CANDIDATO ********************************************************************/



/********************************************************* CRIAÇÃO DA DIMENSÃO CARGO POLITICO ********************************************************************/
IF EXISTS (SELECT 1 FROM sys.Objects WHERE  Object_id = OBJECT_ID(N'[TDCargoPolitico]') AND Type = N'U')
BEGIN
   DROP TABLE [TDCargoPolitico]
END
GO

CREATE TABLE [dbo].[TDCargoPolitico]
(
	[ID]		[int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](100)		NOT NULL,
	[Municipal] [char](3)			NOT NULL,
	[Tipo]		[varchar](100)		NOT NULL,
	
	CONSTRAINT [PK_TDCargoPolitico] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/********************************************************* CRIAÇÃO DA DIMENSÃO CARGO POLITICO ********************************************************************/



/********************************************************* CRIAÇÃO DA DIMENSÃO ESCOLARIDADE ********************************************************************/
IF EXISTS (SELECT 1 FROM sys.Objects WHERE  Object_id = OBJECT_ID(N'[TDEscolaridade]') AND Type = N'U')
BEGIN
   DROP TABLE [TDEscolaridade]
END
GO

CREATE TABLE [dbo].[TDEscolaridade]
(
	[ID]		[int]		IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50)			  NULL,
 
	CONSTRAINT [PK_TDEscolaridade] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/********************************************************* CRIAÇÃO DA DIMENSÃO ESCOLARIDADE ********************************************************************/



/********************************************************* CRIAÇÃO DA DIMENSÃO LOCALIDADE ********************************************************************/
IF EXISTS (SELECT 1 FROM sys.Objects WHERE  Object_id = OBJECT_ID(N'[TDLocalidade]') AND Type = N'U')
BEGIN
   DROP TABLE [TDLocalidade]
END
GO

CREATE TABLE [dbo].[TDLocalidade]
(
	[ID]			[int] IDENTITY(1,1)	NOT NULL,
	[Municipio]		[varchar](50)		NOT NULL,
	[SiglaEstado]	[varchar](2)		NOT NULL,
	[Regiao]		[varchar](50)		NOT NULL,

	CONSTRAINT [PK_TDLocalidade] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/********************************************************* CRIAÇÃO DA DIMENSÃO LOCALIDADE ********************************************************************/



/********************************************************* CRIAÇÃO DA DIMENSÃO OCUPAÇÃO ********************************************************************/
IF EXISTS (SELECT 1 FROM sys.Objects WHERE  Object_id = OBJECT_ID(N'[TDOcupacao]') AND Type = N'U')
BEGIN
   DROP TABLE [TDOcupacao]
END
GO

CREATE TABLE [dbo].[TDOcupacao]
(
	[ID]				[int] IDENTITY(1,1) NOT NULL,
	[Descricao]			[varchar](100)		NOT NULL,
	[VlrMedioDeclarado] [decimal](16, 2)	NOT NULL,
 CONSTRAINT [PK_TDOcupacao] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/********************************************************* CRIAÇÃO DA DIMENSÃO OCUPAÇÃO ********************************************************************/



/********************************************************* CRIAÇÃO DA DIMENSÃO PARTIDO COLIGAÇÃO ********************************************************************/
IF EXISTS (SELECT 1 FROM sys.Objects WHERE  Object_id = OBJECT_ID(N'[TDPartidoColigacao]') AND Type = N'U')
BEGIN
   DROP TABLE [TDPartidoColigacao]
END
GO

CREATE TABLE [dbo].[TDPartidoColigacao]
(
	[ID]		[int] IDENTITY(1,1) NOT NULL,
	[Partido]	[varchar](50)		NOT NULL,
	[Coligacao] [varchar](200)		NOT NULL,
	
	 CONSTRAINT [PK_TDPartidoColigacao] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/********************************************************* CRIAÇÃO DA DIMENSÃO PARTIDO COLIGAÇÃO ********************************************************************/



/********************************************************* CRIAÇÃO DA TABELA FATO DADO ELEITORAL ******************************************************************/
IF EXISTS (SELECT 1 FROM sys.Objects WHERE  Object_id = OBJECT_ID(N'[TFDadoEleitoral]') AND Type = N'U')
BEGIN
   DROP TABLE [TFDadoEleitoral]
END
GO


CREATE TABLE [dbo].[TFDadoEleitoral]
(
	[Ano]						[int] NOT NULL,
	[CandidatoEscolaridadeID]	[int] NOT NULL,
	[PartidoColigacaoID]		[int] NOT NULL,
	[LocalidadeID]				[int] NOT NULL,
	[OcupacaoID]				[int] NOT NULL,
	[CargoPoliticoID]			[int] NOT NULL,
	[VlrTotalDeclarado]			[numeric](16, 2) NULL,
	[QtdTotalDeclarado]			[int] NULL,
 
	CONSTRAINT [PK_TFDadoEleitoral] PRIMARY KEY CLUSTERED 
	(
		[Ano] ASC,
		[CandidatoEscolaridadeID] ASC,
		[PartidoColigacaoID] ASC,
		[LocalidadeID] ASC,
		[OcupacaoID] ASC,
		[CargoPoliticoID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/********************************************************* CRIAÇÃO DA TABELA FATO DADO ELEITORAL ******************************************************************/



/************************************************ CRIAÇÃO DA TABELA PONTE CANDIDATO ESCOLARIDADE ******************************************************************/
IF EXISTS (SELECT 1 FROM sys.Objects WHERE  Object_id = OBJECT_ID(N'[TPCandidatoEscolaridade]') AND Type = N'U')
BEGIN
   DROP TABLE [TPCandidatoEscolaridade]
END
GO


CREATE TABLE [dbo].[TPCandidatoEscolaridade]
(
	[ID]				[int] IDENTITY(1,1) NOT NULL,
	[CandidatoID]		[int]				NULL,
	[EscolaridadeID]	[int]				NULL,
	
	CONSTRAINT [PK_TPCandidatoEscolaridade] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/************************************************ CRIAÇÃO DA TABELA PONTE CANDIDATO ESCOLARIDADE ******************************************************************/




/************************************************ CRIAÇÃO DAS CHAVES DE REFERENCIA ******************************************************************/
ALTER TABLE [dbo].[TFDadoEleitoral]  WITH CHECK ADD  CONSTRAINT [FK_TFDadoEleitoral_TDCargoPolitico] FOREIGN KEY([CargoPoliticoID])
	REFERENCES [dbo].[TDCargoPolitico] ([ID])
GO

ALTER TABLE [dbo].[TFDadoEleitoral] CHECK CONSTRAINT [FK_TFDadoEleitoral_TDCargoPolitico]
GO

ALTER TABLE [dbo].[TFDadoEleitoral]  WITH CHECK ADD  CONSTRAINT [FK_TFDadoEleitoral_TDLocalidade] FOREIGN KEY([LocalidadeID])
REFERENCES [dbo].[TDLocalidade] ([ID])
GO

ALTER TABLE [dbo].[TFDadoEleitoral] CHECK CONSTRAINT [FK_TFDadoEleitoral_TDLocalidade]
GO

ALTER TABLE [dbo].[TFDadoEleitoral]  WITH CHECK ADD  CONSTRAINT [FK_TFDadoEleitoral_TDOcupacao] FOREIGN KEY([OcupacaoID])
REFERENCES [dbo].[TDOcupacao] ([ID])
GO

ALTER TABLE [dbo].[TFDadoEleitoral] CHECK CONSTRAINT [FK_TFDadoEleitoral_TDOcupacao]
GO

ALTER TABLE [dbo].[TFDadoEleitoral]  WITH CHECK ADD  CONSTRAINT [FK_TFDadoEleitoral_TDPartidoColigacao] FOREIGN KEY([PartidoColigacaoID])
REFERENCES [dbo].[TDPartidoColigacao] ([ID])
GO

ALTER TABLE [dbo].[TFDadoEleitoral] CHECK CONSTRAINT [FK_TFDadoEleitoral_TDPartidoColigacao]
GO

ALTER TABLE [dbo].[TFDadoEleitoral]  WITH CHECK ADD  CONSTRAINT [FK_TFDadoEleitoral_TPCandidatoEscolaridade] FOREIGN KEY([CandidatoEscolaridadeID])
REFERENCES [dbo].[TPCandidatoEscolaridade] ([ID])
GO

ALTER TABLE [dbo].[TFDadoEleitoral] CHECK CONSTRAINT [FK_TFDadoEleitoral_TPCandidatoEscolaridade]
GO

ALTER TABLE [dbo].[TPCandidatoEscolaridade]  WITH CHECK ADD  CONSTRAINT [FK_TPCandidatoEscolaridade_TDCandidato] FOREIGN KEY([CandidatoID])
REFERENCES [dbo].[TDCandidato] ([ID])
GO

ALTER TABLE [dbo].[TPCandidatoEscolaridade] CHECK CONSTRAINT [FK_TPCandidatoEscolaridade_TDCandidato]
GO

ALTER TABLE [dbo].[TPCandidatoEscolaridade]  WITH CHECK ADD  CONSTRAINT [FK_TPCandidatoEscolaridade_TDEscolaridade] FOREIGN KEY([EscolaridadeID])
REFERENCES [dbo].[TDEscolaridade] ([ID])
GO

ALTER TABLE [dbo].[TPCandidatoEscolaridade] CHECK CONSTRAINT [FK_TPCandidatoEscolaridade_TDEscolaridade]
GO
/************************************************ CRIAÇÃO DAS CHAVES DE REFERENCIA ******************************************************************/