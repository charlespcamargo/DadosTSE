SELECT 
	   localidade.Municipio + '/' + localidade.SiglaEstado							AS [Municipio/Estado],
	   candidato.Nome																AS [Nome do Candidato Na Urna],
	   ocupacao.Descricao															AS [Ocupação],
	   escolaridade.Descricao														AS [Escolaridade],
	   fato.Ano																		AS [Ano],		   
	   'R$ ' + CAST(FORMAT(fato.VlrTotalDeclarado, 'N', 'de-de') AS VARCHAR(20))	AS [Valor Total de Bens],
	   fato.QtdTotalDeclarado														AS [Quantidade de Bens]

	    

  FROM dbo.TFDadoEleitoral				AS fato
  JOIN dbo.TDLocalidade					AS localidade
    ON localidade.ID = fato.LocalidadeID
  JOIN dbo.TDCargoPolitico				AS cargo
    ON cargo.ID = fato.CargoPoliticoID
  JOIN dbo.TPCandidatoEscolaridade		AS candidato_escolaridade
    ON fato.CandidatoEscolaridadeID = candidato_escolaridade.ID
  JOIN dbo.TDCandidato					AS candidato
    ON candidato.ID = candidato_escolaridade.CandidatoID
  JOIN dbo.TDEscolaridade				AS escolaridade
    ON escolaridade.ID = candidato_escolaridade.EscolaridadeID
  JOIN dbo.TDOcupacao					AS ocupacao
    ON ocupacao.ID = fato.OcupacaoID
 WHERE localidade.SiglaEstado = 'RJ'	
   AND localidade.Municipio = 'RIO DE JANEIRO'
   AND candidato.Nome IN ('MARCELO FREIXO','CRIVELLA')        
 ORDER BY candidato.Nome, fato.Ano



