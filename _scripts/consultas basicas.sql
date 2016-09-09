 
 SELECT TOP 10 *
   FROM dbo.ImportacaoCandidato


SELECT Candidato.NOME_CANDIDATO, Candidato.SIGLA_PARTIDO, Tabela.Valor
  FROM ImportacaoCandidato	AS Candidato WITH(NOLOCK)				
  JOIN (
			
			SELECT C.SEQUENCIAL_CANDIDATO					AS Sequencia,
				   SUM(CAST(b.VALOR_BEM AS NUMERIC(14,2)))	AS Valor

			  FROM ImportacaoCandidato				AS C	    WITH(NOLOCK)				
			  JOIN dbo.ImportacaoBensCandidato		AS B		WITH(NOLOCK)		
				ON C.SEQUENCIAL_CANDIDATO = B.SQ_CANDIDATO 
			 WHERE C.SIGLA_UF = 'SP'
			   AND C.NOME_MUNICIPIO_NASCIMENTO = 'SOROCABA'
			 GROUP BY C.SEQUENCIAL_CANDIDATO

	   ) AS Tabela
	ON Candidato.SEQUENCIAL_CANDIDATO = Tabela.Sequencia
 ORDER BY Valor DESC


   SELECT SIGLA_PARTIDO								AS Sigla,
		  SUM(CAST(b.VALOR_BEM AS NUMERIC(14,2)))	AS Valor,
		  COUNT(C.SEQUENCIAL_CANDIDATO)				AS Candidatos,
		  AVG(CAST(b.VALOR_BEM AS NUMERIC(14,2)))	AS MediaPorCandidato

	 FROM ImportacaoCandidato				AS C	    WITH(NOLOCK)				
	 JOIN dbo.ImportacaoBensCandidato		AS B		WITH(NOLOCK)		
	   ON C.SEQUENCIAL_CANDIDATO = B.SQ_CANDIDATO 
	 WHERE C.SIGLA_UF = 'SP'
	  AND C.NOME_MUNICIPIO_NASCIMENTO = 'SOROCABA'
	GROUP BY SIGLA_PARTIDO
	ORDER BY Valor DESC

/*
	SELECT COUNT(ID) AS QtdRegistrosBens		FROM ImportacaoBensCandidato	WITH(NOLOCK)
	SELECT COUNT(ID) AS QtdRegistrosCandidato	FROM ImportacaoCandidato		WITH(NOLOCK)
	SELECT COUNT(ID) AS QtdRegistrosLegendas	FROM ImportacaoLegenda			WITH(NOLOCK)
	SELECT COUNT(ID) AS QtdRegistrosVagas		FROM ImportacaoVaga				WITH(NOLOCK)
	SELECT COUNT(ID) AS QtdArquivosImportados	FROM ImportacaoArquivo 			WITH(NOLOCK)


*/