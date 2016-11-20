INSERT INTO [dbo].[AG_ANO_ESCOLARIDADE_LOCAL_CARGO]
           ([Ano]
           ,[CandidatoEscolaridadeID]
           ,[LocalidadeID]
           ,[CargoPoliticoID]
           ,[VlrTotalDeclarado]
           ,[QtdTotalDeclarado])
SELECT ANO, 
	   [Escolaridade].EscolaridadeID, 
	   TFDadoEleitoral.LocalidadeID,
	   TFDadoEleitoral.CargoPoliticoID,
	   sum(TFDadoEleitoral.[VlrTotalDeclarado]) as [VlrTotalDeclarado],
	   sum(TFDadoEleitoral.[QtdTotalDeclarado]) as [QtdTotalDeclarado]
FROM TFDadoEleitoral
INNER JOIN [TPCandidatoEscolaridade] ON TFDadoEleitoral.CandidatoEscolaridadeID= [TPCandidatoEscolaridade].ID

INNER JOIN [TPCandidatoEscolaridade] as [Escolaridade] 
	ON [TPCandidatoEscolaridade].EscolaridadeID = [Escolaridade].EscolaridadeID
		AND [Escolaridade].CandidatoID IS NULL

GROUP BY ANO,
	   [Escolaridade].EscolaridadeID,
	   TFDadoEleitoral.LocalidadeID,
	   TFDadoEleitoral.CargoPoliticoID
GO