DELETE FROM TDEscolaridade;
GO
DBCC CHECKIDENT ('[TDEscolaridade]', RESEED, 0);
GO
INSERT INTO TDEscolaridade
SELECT DISTINCT descricao_grau_instrucao FROM ImportacaoCandidato
ORDER BY descricao_grau_instrucao