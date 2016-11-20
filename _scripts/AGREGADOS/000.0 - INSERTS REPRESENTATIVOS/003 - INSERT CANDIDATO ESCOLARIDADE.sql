SET IDENTITY_INSERT [TPCandidatoEscolaridade] ON; 
GO
DECLARE @MAXID INT = ((SELECT MAX(ID) FROM [TPCandidatoEscolaridade]) + 10000000);


INSERT INTO [dbo].[TPCandidatoEscolaridade]
           ([ID]
		   ,[CandidatoID]
           ,[EscolaridadeID])
SELECT ROW_NUMBER() OVER(ORDER BY [CandidatoID]) + @MAXID, [CandidatoID], [EscolaridadeID] FROM (
	SELECT DISTINCT  TDCandidatoSexo.ID AS [CandidatoID], TDEscolaridade.ID AS [EscolaridadeID] 
	FROM [TPCandidatoEscolaridade]
	INNER JOIN TDCandidato ON [TPCandidatoEscolaridade].CandidatoID = TDCandidato.ID
	INNER JOIN TDEscolaridade ON [TPCandidatoEscolaridade].EscolaridadeID = TDEscolaridade.ID
	INNER JOIN TDCandidato AS TDCandidatoSexo ON TDCandidatoSexo.SEXO = TDCandidato.Sexo 
		AND TDCandidatoSexo.CPF = 'NULL' 
		AND TDCandidatoSexo.Nome = 'NULL'
	LEFT JOIN [TPCandidatoEscolaridade] AS [TPCandidatoEscolaridadeExistente] ON [TPCandidatoEscolaridadeExistente].CandidatoID = TDCandidatoSexo.ID
		AND TDEscolaridade.ID = [TPCandidatoEscolaridadeExistente].EscolaridadeID
	WHERE [TPCandidatoEscolaridadeExistente].ID IS NULL
) RESULTADO


SET IDENTITY_INSERT [TPCandidatoEscolaridade] OFF;
GO


