CREATE TABLE [dbo].[AG_ANO_LOCAL_CARGO_PARTIDO](
	[Ano] [int] NOT NULL,
	[LocalidadeID] [int] NOT NULL,
	[CargoPoliticoID] [int] NOT NULL,
	[PartidoColigacaoID] [int] NOT NULL,
	[QtdCandidatos] [int] NOT NULL,
	[VlrTotalDeclarado] [numeric](16, 2) NOT NULL,
	[QtdTotalDeclarado] [int] NOT NULL,
 CONSTRAINT [PK_AG_ANO_LOCAL_CARGO_PARTIDO] PRIMARY KEY CLUSTERED 
(
	[Ano] ASC,
	[LocalidadeID] ASC,
	[CargoPoliticoID] ASC,
	[PartidoColigacaoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AG_ANO_LOCAL_CARGO_PARTIDO]  WITH CHECK ADD  CONSTRAINT [FK_AG_ANO_LOCAL_CARGO_PARTIDO_TDCargoPolitico] FOREIGN KEY([CargoPoliticoID])
REFERENCES [dbo].[TDCargoPolitico] ([ID])
GO

ALTER TABLE [dbo].[AG_ANO_LOCAL_CARGO_PARTIDO] CHECK CONSTRAINT [FK_AG_ANO_LOCAL_CARGO_PARTIDO_TDCargoPolitico]
GO

ALTER TABLE [dbo].[AG_ANO_LOCAL_CARGO_PARTIDO]  WITH CHECK ADD  CONSTRAINT [FK_AG_ANO_LOCAL_CARGO_PARTIDO_TDLocalidade] FOREIGN KEY([LocalidadeID])
REFERENCES [dbo].[TDLocalidade] ([ID])
GO

ALTER TABLE [dbo].[AG_ANO_LOCAL_CARGO_PARTIDO] CHECK CONSTRAINT [FK_AG_ANO_LOCAL_CARGO_PARTIDO_TDLocalidade]
GO

ALTER TABLE [dbo].[AG_ANO_LOCAL_CARGO_PARTIDO]  WITH CHECK ADD  CONSTRAINT [FK_AG_ANO_LOCAL_CARGO_PARTIDO_TDPartidoColigacao] FOREIGN KEY([PartidoColigacaoID])
REFERENCES [dbo].[TDPartidoColigacao] ([ID])
GO

ALTER TABLE [dbo].[AG_ANO_LOCAL_CARGO_PARTIDO] CHECK CONSTRAINT [FK_AG_ANO_LOCAL_CARGO_PARTIDO_TDPartidoColigacao]
GO

