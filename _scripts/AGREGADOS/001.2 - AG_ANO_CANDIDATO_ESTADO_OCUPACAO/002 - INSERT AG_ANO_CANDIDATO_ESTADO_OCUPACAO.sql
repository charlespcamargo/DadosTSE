INSERT INTO [dbo].[AG_ANO_CANDIDATO_ESTADO_OCUPACAO]
           ([Ano]
           ,[CandidatoEscolaridadeID]
           ,[LocalidadeID]
           ,[OcupacaoID]
           ,[VlrTotalDeclarado]
           ,[QtdTotalDeclarado])
SELECT TFDadoEleitoral.ANO, 
	   TFDadoEleitoral.CandidatoEscolaridadeID, 
	   TDLocalidadeEstado.ID AS LocalidadeID, 
	   TFDadoEleitoral.OcupacaoID,
	   SUM(TFDadoEleitoral.[VlrTotalDeclarado]) as [VlrTotalDeclarado],
	   SUM(TFDadoEleitoral.[QtdTotalDeclarado]) as [QtdTotalDeclarado]
FROM TFDadoEleitoral
INNER JOIN TDLocalidade ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
INNER JOIN TDLocalidade as TDLocalidadeEstado ON TDLocalidade.SiglaEstado = TDLocalidadeEstado.SiglaEstado
	and TDLocalidadeEstado.Municipio = 'NULL'

GROUP BY TFDadoEleitoral.ANO, 
	   TFDadoEleitoral.CandidatoEscolaridadeID,
	   TDLocalidadeEstado.ID, 
	   TFDadoEleitoral.OcupacaoID
GO