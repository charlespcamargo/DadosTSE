INSERT INTO [dbo].AG_ANO_CANDIDATO_REGIAO_OCUPACAO
           ([Ano]
           ,[CandidatoEscolaridadeID]
           ,[LocalidadeID]
           ,[OcupacaoID]
           ,[VlrTotalDeclarado]
           ,[QtdTotalDeclarado])
SELECT TFDadoEleitoral.ANO, 
	   TFDadoEleitoral.CandidatoEscolaridadeID, 
	   TDLocalidadeRegiao.ID AS LocalidadeID, 
	   TFDadoEleitoral.OcupacaoID,
	   SUM(TFDadoEleitoral.[VlrTotalDeclarado]) as [VlrTotalDeclarado],
	   SUM(TFDadoEleitoral.[QtdTotalDeclarado]) as [QtdTotalDeclarado]
FROM TFDadoEleitoral
INNER JOIN TDLocalidade ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
INNER JOIN TDLocalidade as TDLocalidadeRegiao ON TDLocalidade.Regiao = TDLocalidadeRegiao.Regiao
	and TDLocalidadeRegiao.Municipio = 'NULL' AND TDLocalidadeRegiao.SiglaEstado = 'NA'

GROUP BY TFDadoEleitoral.ANO, 
	   TFDadoEleitoral.CandidatoEscolaridadeID,
	   TDLocalidadeRegiao.ID, 
	   TFDadoEleitoral.OcupacaoID
GO