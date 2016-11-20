SET IDENTITY_INSERT [TPCandidatoEscolaridade] ON; 
GO
DECLARE @MAXID INT = ((SELECT MAX(ID) FROM [TPCandidatoEscolaridade]) + 10000000);


INSERT INTO [dbo].[TPCandidatoEscolaridade]
           ([ID]
		   ,[CandidatoID]
           ,[EscolaridadeID])
SELECT ROW_NUMBER() OVER(ORDER BY [EscolaridadeID]) + @MAXID, NULL, [EscolaridadeID] FROM (
	SELECT DISTINCT  TDEscolaridade.ID AS [EscolaridadeID] 
	FROM TDEscolaridade
	LEFT JOIN [TPCandidatoEscolaridade] ON [TPCandidatoEscolaridade].EscolaridadeID = TDEscolaridade.ID
		AND [TPCandidatoEscolaridade].[CandidatoID] IS NULL
	WHERE [TPCandidatoEscolaridade].ID IS NULL
) RESULTADO


SET IDENTITY_INSERT [TPCandidatoEscolaridade] OFF;
GO


