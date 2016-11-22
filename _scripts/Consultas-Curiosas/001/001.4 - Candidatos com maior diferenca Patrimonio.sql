DECLARE	@ANO INT = NULL;
DECLARE	@TODOSANOS BIT = NULL;
DECLARE	@SEXO VARCHAR(20) = NULL;
DECLARE	@IDESCOLARIDADE INT = NULL;
DECLARE	@IDOCUPACAO INT = NULL;
DECLARE	@REGIAO VARCHAR(50) = NULL;
DECLARE	@SIGLAESTADO VARCHAR(2) = NULL;
DECLARE	@IDMUNICIPIO INT = NULL;
DECLARE	@SIGLAPARTIDO VARCHAR(50) = NULL;
DECLARE	@IDCARGOPRETENDIDO INT = NULL;


SELECT TOP 100 Candidato , Partido,Municipio, SiglaEstado, (PorcentagemDaMediaMax - PorcentagemDaMediaMin) DiferencaPorcentagemDaMedia, VlrMedioOcupacao,VlrTotalDeclarado  FROM (

SELECT 
			SUM(TDOcupacao.VlrMedioDeclarado)										AS VlrMedioOcupacao,	   
			SUM(TFDadoEleitoral.VlrTotalDeclarado)									AS VlrTotalDeclarado,
			TDCandidato.Nome AS Candidato,
			TDPartidoColigacao.Partido,TDLocalidade.Municipio, TDLocalidade.SiglaEstado,
			MIN((
					CASE WHEN TDOcupacao.VlrMedioDeclarado = 0 THEN
							1
					ELSE TFDadoEleitoral.VlrTotalDeclarado / TDOcupacao.VlrMedioDeclarado END
							
							)) * 100.0 AS PorcentagemDaMediaMin,
			MAX((
				CASE WHEN TDOcupacao.VlrMedioDeclarado = 0 THEN
						1
				ELSE TFDadoEleitoral.VlrTotalDeclarado / TDOcupacao.VlrMedioDeclarado END
							
						)) * 100.0 AS PorcentagemDaMediaMax			
			

	FROM TFDadoEleitoral
    JOIN TPCandidatoEscolaridade 
	  ON TFDadoEleitoral.CandidatoEscolaridadeID = TPCandidatoEscolaridade.ID
	JOIN TDCandidato 
	  ON TPCandidatoEscolaridade.CandidatoID = TDCandidato.ID
	JOIN TDCargoPolitico 
	  ON TFDadoEleitoral.CargoPoliticoID = TDCargoPolitico.ID
	JOIN TDLocalidade 
	  ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
	JOIN TDOcupacao 
	  ON TFDadoEleitoral.OcupacaoID = TDOcupacao.ID
	JOIN TDPartidoColigacao 
	  ON TFDadoEleitoral.PartidoColigacaoID = TDPartidoColigacao.ID   
   WHERE ISNULL(@ANO, TFDadoEleitoral.Ano) = TFDadoEleitoral.Ano
	 AND ISNULL(@SEXO,TDCandidato.Sexo) = TDCandidato.Sexo		
	 AND ISNULL(@IDESCOLARIDADE,TPCandidatoEscolaridade.EscolaridadeID) = TPCandidatoEscolaridade.EscolaridadeID
	 AND ISNULL(@IDOCUPACAO,TDOcupacao.ID) = TDOcupacao.ID
	 AND ISNULL(@REGIAO,TDLocalidade.Regiao) = TDLocalidade.Regiao
	 AND ISNULL(@SIGLAESTADO,TDLocalidade.SiglaEstado) = TDLocalidade.SiglaEstado
	 AND ISNULL(@IDMUNICIPIO,TDLocalidade.ID) = TDLocalidade.ID
	 AND ISNULL(@SIGLAPARTIDO,TDPartidoColigacao.Partido) = TDPartidoColigacao.Partido
	 AND ISNULL(@IDCARGOPRETENDIDO,TDCargoPolitico.ID) = TDCargoPolitico.ID
	GROUP BY TDCandidato.ID, TDCandidato.Nome, TDPartidoColigacao.Partido,TDLocalidade.Municipio, TDLocalidade.SiglaEstado
	) RESULTADO
	ORDER BY DiferencaPorcentagemDaMedia DESC