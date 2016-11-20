DELETE FROM AG_ANO_SEXO_PARTIDO_ESTADO;

INSERT INTO [dbo].AG_ANO_SEXO_PARTIDO_ESTADO
           ([Ano]
           ,[CandidatoEscolaridadeID]
           ,[PartidoColigacaoID]
           ,[LocalidadeID]
           ,[QtdCandidatos]
           ,[VlrTotalDeclarado]
		   ,[QtdTotalDeclarado])
SELECT ANO															AS Ano, 
	   [TPCandidatoEscolaridadeSexo].ID								AS TPCandidatoEscolaridadeSexoID, 
	   TFDadoEleitoral.[PartidoColigacaoID]							AS PartidoColigacaoID,				
	   TDLocalidadeEstado.ID										AS TDLocalidadeEstadoID, 
	   
	   COUNT([TDCandidato].ID)										AS TDCandidato,
	   SUM(TFDadoEleitoral.[VlrTotalDeclarado])						AS VlrTotalDeclarado,
	   SUM(TFDadoEleitoral.[QtdTotalDeclarado])						AS QtdTotalDeclarado

FROM TFDadoEleitoral
INNER JOIN TDLocalidade ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
INNER JOIN TDLocalidade as TDLocalidadeEstado ON TDLocalidade.SiglaEstado = TDLocalidadeEstado.SiglaEstado
	and TDLocalidadeEstado.Municipio = 'NULL'
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
	   TDLocalidadeEstado.ID