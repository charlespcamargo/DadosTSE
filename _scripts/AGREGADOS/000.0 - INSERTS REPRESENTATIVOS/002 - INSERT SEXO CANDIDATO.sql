SET IDENTITY_INSERT [TDCandidato] ON; 
GO
DECLARE @MAXID INT = ((SELECT MAX(ID) FROM [TDCandidato]) + 10000000);
IF NOT EXISTS(SELECT TOP 1 1 FROM [TDCandidato] WHERE [Sexo] ='MASCULINO' AND [CPF] = 'NULL'  )
BEGIN
	INSERT INTO [dbo].[TDCandidato]
			   (ID
			   ,[CPF]
			   ,[Nome]
			   ,[Sexo])
		 VALUES
			   (@MAXID
			   ,'NULL'
			   ,'NULL'
			   ,'MASCULINO');
END

IF NOT EXISTS(SELECT TOP 1 1 FROM [TDCandidato] WHERE [Sexo] ='FEMININO' AND [CPF] = 'NULL'  )
BEGIN
	INSERT INTO [dbo].[TDCandidato]
			(ID
			,[CPF]
			,[Nome]
			,[Sexo])
		VALUES
			(
			@MAXID + 1
			,'NULL'
			,'NULL'
			,'FEMININO');
END
SET IDENTITY_INSERT [TDCandidato] OFF;