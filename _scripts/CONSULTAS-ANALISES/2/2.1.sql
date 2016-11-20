SELECT TFDadoEleitoral.Ano, 
	   TDLocalidade.Regiao, 
	   TDLocalidade.SiglaEstado, 
	   TDLocalidade.Municipio,
	   TDCargoPolitico.Descricao,
	   TDPartidoColigacao.Partido,
	   TDEscolaridade.Descricao AS Escolaridade,
	   COUNT(1) AS Quantidade,
	   (COUNT(1) / (AG_ANO_LOCAL_CARGO_PARTIDO.QtdCandidatos + 0.0)) * 100.0 AS Percentual
	    FROM TFDadoEleitoral
INNER JOIN TPCandidatoEscolaridade ON TFDadoEleitoral.CandidatoEscolaridadeID = TPCandidatoEscolaridade.ID
INNER JOIN TDCandidato ON TPCandidatoEscolaridade.CandidatoID = TDCandidato.ID
INNER JOIN TDEscolaridade ON TPCandidatoEscolaridade.EscolaridadeID = TDEscolaridade.ID
INNER JOIN TDCargoPolitico ON TFDadoEleitoral.CargoPoliticoID = TDCargoPolitico.ID
INNER JOIN TDLocalidade ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
INNER JOIN TDOcupacao ON TFDadoEleitoral.OcupacaoID = TDOcupacao.ID
INNER JOIN TDPartidoColigacao ON TFDadoEleitoral.PartidoColigacaoID = TDPartidoColigacao.ID
INNER JOIN AG_ANO_LOCAL_CARGO_PARTIDO ON TFDadoEleitoral.Ano = AG_ANO_LOCAL_CARGO_PARTIDO.ANO AND 
	   TDLocalidade.ID = AG_ANO_LOCAL_CARGO_PARTIDO.LocalidadeID AND
	   TDCargoPolitico.ID = AG_ANO_LOCAL_CARGO_PARTIDO.CargoPoliticoID AND
	   TDPartidoColigacao.ID = AG_ANO_LOCAL_CARGO_PARTIDO.PartidoColigacaoID

GROUP BY TFDadoEleitoral.Ano, 
	   TDLocalidade.Regiao, 
	   TDLocalidade.SiglaEstado, 
	   TDLocalidade.Municipio,
	   TDCargoPolitico.Descricao,
	   TDPartidoColigacao.Partido,
	   AG_ANO_LOCAL_CARGO_PARTIDO.QtdCandidatos,
	   TDEscolaridade.Descricao,
	   TDEscolaridade.Nivel
ORDER BY TFDadoEleitoral.Ano, 
	   TDLocalidade.Regiao, 
	   TDLocalidade.SiglaEstado, 
	   TDLocalidade.Municipio,
	   TDCargoPolitico.Descricao,
	   TDPartidoColigacao.Partido,
	   TDEscolaridade.Nivel



--SELECT TFDadoEleitoral.Ano, 
--	   TDLocalidade.Regiao, 
--	   TDLocalidade.SiglaEstado, 
--	   TDLocalidade.Municipio,
--	   TDCargoPolitico.Descricao,
--	   COUNT(1) AS Quantidade
--	    FROM TFDadoEleitoral
--INNER JOIN TPCandidatoEscolaridade ON TFDadoEleitoral.CandidatoEscolaridadeID = TPCandidatoEscolaridade.ID
--INNER JOIN TDCandidato ON TPCandidatoEscolaridade.CandidatoID = TDCandidato.ID
--INNER JOIN TDEscolaridade ON TPCandidatoEscolaridade.EscolaridadeID = TDEscolaridade.ID
--INNER JOIN TDCargoPolitico ON TFDadoEleitoral.CargoPoliticoID = TDCargoPolitico.ID
--INNER JOIN TDLocalidade ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
--INNER JOIN TDOcupacao ON TFDadoEleitoral.OcupacaoID = TDOcupacao.ID
--INNER JOIN TDPartidoColigacao ON TFDadoEleitoral.PartidoColigacaoID = TDPartidoColigacao.ID
--GROUP BY TFDadoEleitoral.Ano, 
--	   TDLocalidade.Regiao, 
--	   TDLocalidade.SiglaEstado, 
--	   TDLocalidade.Municipio,
--	   TDCargoPolitico.Descricao,
--	   TDPartidoColigacao.Partido
--ORDER BY TFDadoEleitoral.Ano, 
--	   TDLocalidade.Regiao, 
--	   TDLocalidade.SiglaEstado, 
--	   TDLocalidade.Municipio,
--	   TDCargoPolitico.Descricao