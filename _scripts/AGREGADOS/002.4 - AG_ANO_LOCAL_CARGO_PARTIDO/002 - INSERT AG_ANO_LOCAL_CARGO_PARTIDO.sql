DELETE FROM AG_ANO_LOCAL_CARGO_PARTIDO;

INSERT INTO [dbo].AG_ANO_LOCAL_CARGO_PARTIDO
           ([Ano]
           ,[LocalidadeID]
           ,[CargoPoliticoID]
           ,[PartidoColigacaoID]
           ,[QtdCandidatos]
           ,[VlrTotalDeclarado]
           ,[QtdTotalDeclarado])
SELECT TFDadoEleitoral.Ano, 
	   TDLocalidade.ID,
	   TDCargoPolitico.ID,
	   TDPartidoColigacao.ID,
	   COUNT(1) AS Quantidade,
	   SUM(TFDadoEleitoral.VlrTotalDeclarado),
	   SUM(TFDadoEleitoral.QtdTotalDeclarado)
	    FROM TFDadoEleitoral
INNER JOIN TPCandidatoEscolaridade ON TFDadoEleitoral.CandidatoEscolaridadeID = TPCandidatoEscolaridade.ID
INNER JOIN TDCandidato ON TPCandidatoEscolaridade.CandidatoID = TDCandidato.ID
INNER JOIN TDEscolaridade ON TPCandidatoEscolaridade.EscolaridadeID = TDEscolaridade.ID
INNER JOIN TDCargoPolitico ON TFDadoEleitoral.CargoPoliticoID = TDCargoPolitico.ID
INNER JOIN TDLocalidade ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
INNER JOIN TDOcupacao ON TFDadoEleitoral.OcupacaoID = TDOcupacao.ID
INNER JOIN TDPartidoColigacao ON TFDadoEleitoral.PartidoColigacaoID = TDPartidoColigacao.ID
GROUP BY TFDadoEleitoral.Ano, 
	   TDLocalidade.ID,
	   TDCargoPolitico.ID,
	   TDPartidoColigacao.ID