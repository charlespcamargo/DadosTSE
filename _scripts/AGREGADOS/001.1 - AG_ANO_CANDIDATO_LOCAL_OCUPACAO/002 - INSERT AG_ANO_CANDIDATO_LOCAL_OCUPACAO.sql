DELETE FROM [AG_ANO_CANDIDATO_LOCAL_OCUPACAO]
GO
INSERT INTO [dbo].[AG_ANO_CANDIDATO_LOCAL_OCUPACAO]
           ([Ano]
           ,[CandidatoEscolaridadeID]
           ,[LocalidadeID]
           ,[OcupacaoID]
           ,[VlrTotalDeclarado]
           ,[QtdTotalDeclarado])
SELECT TFDadoEleitoral.ANO, 
	   TFDadoEleitoral.CandidatoEscolaridadeID, 
	   TFDadoEleitoral.LocalidadeID, 
	   TFDadoEleitoral.OcupacaoID,
	   SUM(TFDadoEleitoral.[VlrTotalDeclarado]) as [VlrTotalDeclarado],
	   SUM(TFDadoEleitoral.[QtdTotalDeclarado]) as [QtdTotalDeclarado]
FROM TFDadoEleitoral
GROUP BY TFDadoEleitoral.ANO, 
	   TFDadoEleitoral.CandidatoEscolaridadeID,
	   TFDadoEleitoral.LocalidadeID, 
	   TFDadoEleitoral.OcupacaoID

	   