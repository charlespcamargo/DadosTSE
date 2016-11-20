SET IDENTITY_INSERT [TDLocalidade] ON; 
GO
DECLARE @MAXID INT = ((SELECT MAX(ID) FROM [TDLocalidade]) + 10000000);


IF NOT EXISTS(SELECT TOP 1 1 FROM [TDLocalidade] WHERE Municipio ='NULL')
BEGIN
	INSERT INTO [dbo].[TDLocalidade]
			   (ID
			   ,[Municipio]
			   ,[SiglaEstado]
			   ,[Regiao])		 
	SELECT (ROW_NUMBER() OVER(ORDER BY [TDLocalidade].SiglaEstado)) + @MAXID, 'NULL', [TDLocalidade].SiglaEstado, [TDLocalidade].Regiao
			   FROM [TDLocalidade] 
			   GROUP BY [TDLocalidade].SiglaEstado, [TDLocalidade].Regiao

END
SET IDENTITY_INSERT [TDLocalidade] OFF;
GO