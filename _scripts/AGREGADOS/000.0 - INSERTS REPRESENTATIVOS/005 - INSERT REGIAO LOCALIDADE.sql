SET IDENTITY_INSERT [TDLocalidade] ON; 
GO
DECLARE @MAXID INT = ((SELECT MAX(ID) FROM [TDLocalidade]) + 10000000);


IF NOT EXISTS(SELECT TOP 1 1 FROM [TDLocalidade] WHERE Municipio ='NULL' AND [SiglaEstado] = 'NA')
BEGIN
	INSERT INTO [dbo].[TDLocalidade]
			   (ID
			   ,[Municipio]
			   ,[SiglaEstado]
			   ,[Regiao])		 
	SELECT (ROW_NUMBER() OVER(ORDER BY [TDLocalidade].Regiao)) + @MAXID, 'NULL', 'NA', [TDLocalidade].Regiao
			   FROM [TDLocalidade] 
			   GROUP BY [TDLocalidade].Regiao

END
SET IDENTITY_INSERT [TDLocalidade] OFF;
GO