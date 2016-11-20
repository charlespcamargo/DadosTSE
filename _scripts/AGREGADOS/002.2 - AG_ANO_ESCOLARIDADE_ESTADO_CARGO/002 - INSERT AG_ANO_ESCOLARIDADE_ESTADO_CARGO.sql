INSERT INTO [dbo].AG_ANO_ESCOLARIDADE_ESTADO_CARGO
           ([Ano]
           ,[CandidatoEscolaridadeID]
           ,[LocalidadeID]
           ,[CargoPoliticoID]
           ,[VlrTotalDeclarado]
           ,[QtdTotalDeclarado])
SELECT ANO, 
	   [Escolaridade].EscolaridadeID, 
	   TDLocalidadeEstado.ID AS LocalidadeID,
	   TFDadoEleitoral.CargoPoliticoID,
	   sum(TFDadoEleitoral.[VlrTotalDeclarado]) as [VlrTotalDeclarado],
	   sum(TFDadoEleitoral.[QtdTotalDeclarado]) as [QtdTotalDeclarado]
FROM TFDadoEleitoral
INNER JOIN [TPCandidatoEscolaridade] ON TFDadoEleitoral.CandidatoEscolaridadeID= [TPCandidatoEscolaridade].ID

INNER JOIN [TPCandidatoEscolaridade] as [Escolaridade] 
	ON [TPCandidatoEscolaridade].EscolaridadeID = [Escolaridade].EscolaridadeID
		AND [Escolaridade].CandidatoID IS NULL

INNER JOIN TDLocalidade ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
INNER JOIN TDLocalidade as TDLocalidadeEstado ON TDLocalidade.SiglaEstado = TDLocalidadeEstado.SiglaEstado
	and TDLocalidadeEstado.Municipio = 'NULL'

GROUP BY ANO,
	   [Escolaridade].EscolaridadeID,
	   TDLocalidadeEstado.ID,
	   TFDadoEleitoral.CargoPoliticoID
GO