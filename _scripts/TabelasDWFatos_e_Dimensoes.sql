/****** Object:  Table [dbo].[TDCandidato]    Script Date: 21/10/2016 17:40:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TDCandidato](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](30) NOT NULL,
	[Sexo] [varchar](20) NOT NULL,
	[Escolaridade] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TDCandidato] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TDCargoPolitico]    Script Date: 21/10/2016 17:40:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TDCargoPolitico](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](100) NOT NULL,
	[Municipal] [char](3) NOT NULL,
	[Tipo] [varchar](100) NOT NULL,
 CONSTRAINT [PK_TDCargoPolitico] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TDLocalidade]    Script Date: 21/10/2016 17:40:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TDLocalidade](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Municipio] [varchar](50) NOT NULL,
	[SiglaEstado] [varchar](2) NOT NULL,
	[Regiao] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TDLocalidade] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TDOcupacao]    Script Date: 21/10/2016 17:40:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TDOcupacao](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](100) NOT NULL,
	[VlrMedioDeclarado] [decimal](16, 2) NOT NULL,
 CONSTRAINT [PK_TDOcupacao] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TDPartidoColigacao]    Script Date: 21/10/2016 17:40:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TDPartidoColigacao](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Partido] [varchar](50) NOT NULL,
	[Coligacao] [varchar](200) NOT NULL,
 CONSTRAINT [PK_TDPartidoColigacao] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TFDadoEleitoral]    Script Date: 21/10/2016 17:40:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TFDadoEleitoral](
	[Ano] [int] NOT NULL,
	[CandidatoID] [int] NOT NULL,
	[PartidoColigacaoID] [int] NOT NULL,
	[LocalidadeID] [int] NOT NULL,
	[OcupacaoID] [int] NOT NULL,
	[CargoPoliticoID] [int] NOT NULL,
	[VlrTotalDeclarado] [numeric](16, 2) NULL,
	[QtdTotalDeclarado] [int] NULL,
 CONSTRAINT [PK_TFDadoEleitoral] PRIMARY KEY CLUSTERED 
(
	[Ano] ASC,
	[CandidatoID] ASC,
	[PartidoColigacaoID] ASC,
	[LocalidadeID] ASC,
	[OcupacaoID] ASC,
	[CargoPoliticoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TFDadoEleitoral]  WITH CHECK ADD  CONSTRAINT [FK_TFDadoEleitoral_TDCandidato] FOREIGN KEY([CandidatoID])
REFERENCES [dbo].[TDCandidato] ([ID])
GO
ALTER TABLE [dbo].[TFDadoEleitoral] CHECK CONSTRAINT [FK_TFDadoEleitoral_TDCandidato]
GO
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
