CREATE TABLE [dbo].[AG_ANO_SEXO_PARTIDO_REGIAO](
	[Ano] [int] NOT NULL,
	[CandidatoEscolaridadeID] [int] NOT NULL,
	[PartidoColigacaoID] [int] NOT NULL,
	[LocalidadeID] [int] NOT NULL,
	[VlrTotalDeclarado] [numeric](16, 2) NOT NULL,
	[QtdTotalDeclarado] [int] NOT NULL,
 CONSTRAINT [PK_AG_ANO_SEXO_PARTIDO_REGIAO] PRIMARY KEY CLUSTERED 
(
	[Ano] ASC,
	[CandidatoEscolaridadeID] ASC,
	[PartidoColigacaoID] ASC,
	[LocalidadeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

