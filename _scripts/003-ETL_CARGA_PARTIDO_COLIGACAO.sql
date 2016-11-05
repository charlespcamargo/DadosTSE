DELETE FROM TDPartidoColigacao;
GO
DBCC CHECKIDENT ('[TDPartidoColigacao]', RESEED, 0);
GO
INSERT INTO TDPartidoColigacao
SELECT DISTINCT ImportacaoCandidato.SIGLA_PARTIDO, ImportacaoCandidato.COMPOSICAO_LEGENDA FROM ImportacaoCandidato
ORDER BY ImportacaoCandidato.SIGLA_PARTIDO