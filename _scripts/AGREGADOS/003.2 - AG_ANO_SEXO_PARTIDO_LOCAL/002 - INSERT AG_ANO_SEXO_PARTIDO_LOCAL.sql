delete from [AG_ANO_SEXO_PARTIDO_LOCAL];

INSERT INTO [dbo].[AG_ANO_SEXO_PARTIDO_LOCAL]
           ([Ano]
           ,[CandidatoEscolaridadeID]
           ,[PartidoColigacaoID]
           ,[LocalidadeID]
		   ,QtdCandidatos
           ,[VlrTotalDeclarado]
           ,[QtdTotalDeclarado])
SELECT ANO, 
	   [TPCandidatoEscolaridadeSexo].ID  as TPCandidatoEscolaridadeSexoID, 
	   TFDadoEleitoral.[PartidoColigacaoID],
	   TDLocalidade.ID as TDLocalidadeEstadoID, 
	   COUNT([TDCandidato].ID) as QtdCandidatos,
	   sum(TFDadoEleitoral.[VlrTotalDeclarado]) as [VlrTotalDeclarado],
	   sum(TFDadoEleitoral.[QtdTotalDeclarado]) as [QtdTotalDeclarado]
FROM TFDadoEleitoral
INNER JOIN TDLocalidade ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
INNER JOIN [TPCandidatoEscolaridade] ON TFDadoEleitoral.CandidatoEscolaridadeID= [TPCandidatoEscolaridade].ID
INNER JOIN [TDCandidato] ON [TPCandidatoEscolaridade].CandidatoID= [TDCandidato].ID
INNER JOIN TDCandidato AS TDCandidatoSexo ON TDCandidatoSexo.SEXO = TDCandidato.Sexo 
	and TDCandidatoSexo.CPF = 'NULL' 
	and TDCandidatoSexo.Nome = 'NULL'

LEFT JOIN [TPCandidatoEscolaridade] as [TPCandidatoEscolaridadeSexo] 
	ON [TPCandidatoEscolaridade].EscolaridadeID = [TPCandidatoEscolaridadeSexo].EscolaridadeID 
		AND TDCandidatoSexo.ID = [TPCandidatoEscolaridadeSexo].CandidatoID

GROUP BY ANO, 
	   [TPCandidatoEscolaridadeSexo].ID, 
	   TFDadoEleitoral.[PartidoColigacaoID],
	   TDLocalidade.ID