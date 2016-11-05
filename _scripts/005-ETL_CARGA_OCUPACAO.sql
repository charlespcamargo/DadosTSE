DELETE FROM TDOcupacao;
GO

DBCC CHECKIDENT ('[TDOcupacao]', RESEED, 0);
GO

INSERT INTO dbo.TDOcupacao (Descricao, VlrMedioDeclarado)	
SELECT 
	   --Candidato.ANO_ELEICAO							AS Ano,
	   --Candidato.CODIGO_OCUPACAO						AS Codigo,
	   Candidato.DESCRICAO_OCUPACAO						AS Descricao,
	   AVG(CAST(Bens.VALOR_BEM AS NUMERIC(16,2)))		AS Valor

  FROM ImportacaoCandidato										AS Candidato
  JOIN dbo.ImportacaoBensCandidato								AS Bens
    ON Candidato.ANO_ELEICAO = bens.ANO_ELEICAO
   AND Candidato.SEQUENCIAL_CANDIDATO = bens.SQ_CANDIDATO
 GROUP BY Candidato.ANO_ELEICAO,
		  Candidato.CODIGO_OCUPACAO,
		  Candidato.DESCRICAO_OCUPACAO

		   

  