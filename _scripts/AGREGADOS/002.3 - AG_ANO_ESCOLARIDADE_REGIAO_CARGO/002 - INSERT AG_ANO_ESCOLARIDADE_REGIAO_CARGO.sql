INSERT INTO [dbo].AG_ANO_ESCOLARIDADE_REGIAO_CARGO
           ([Ano]
           ,[CandidatoEscolaridadeID]
           ,[LocalidadeID]
           ,[CargoPoliticoID]
           ,[VlrTotalDeclarado]
           ,[QtdTotalDeclarado])
SELECT ANO, 
	   [Escolaridade].EscolaridadeID, 
	   TDLocalidadeRegiao.ID AS LocalidadeID,
	   TFDadoEleitoral.CargoPoliticoID,
	   sum(TFDadoEleitoral.[VlrTotalDeclarado]) as [VlrTotalDeclarado],
	   sum(TFDadoEleitoral.[QtdTotalDeclarado]) as [QtdTotalDeclarado]
FROM TFDadoEleitoral
INNER JOIN [TPCandidatoEscolaridade] ON TFDadoEleitoral.CandidatoEscolaridadeID= [TPCandidatoEscolaridade].ID

INNER JOIN [TPCandidatoEscolaridade] as [Escolaridade] 
	ON [TPCandidatoEscolaridade].EscolaridadeID = [Escolaridade].EscolaridadeID
		AND [Escolaridade].CandidatoID IS NULL

INNER JOIN TDLocalidade ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
INNER JOIN TDLocalidade as TDLocalidadeRegiao ON TDLocalidade.Regiao = TDLocalidadeRegiao.Regiao
	and TDLocalidadeRegiao.Municipio = 'NULL' and TDLocalidadeRegiao.SiglaEstado = 'NA'

GROUP BY ANO,
	   [Escolaridade].EscolaridadeID,
	   TDLocalidadeRegiao.ID,
	   TFDadoEleitoral.CargoPoliticoID
GO