IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spListarBensPrefeitos')
	DROP PROCEDURE spListarBensPrefeitos
GO

CREATE PROCEDURE spListarBensPrefeitos
(
	@SIGLA_UE	VARCHAR(05)
)
AS
BEGIN

	SET NOCOUNT ON;

	/*
		
		EXEC spListarBensPrefeitos @SIGLA_UE = '72435'

	*/

	DECLARE @candidatos AS TABLE
	(
		CPF						VARCHAR(12),
		NomeUrna				VARCHAR(50),
		Bens2006				NUMERIC(12,2),
		Bens2008				NUMERIC(12,2),
		Bens2010				NUMERIC(12,2),
		Bens2012				NUMERIC(12,2),
		Bens2014				NUMERIC(12,2),
		Bens2016				NUMERIC(12,2)
	)

	INSERT INTO @candidatos
	SELECT CPF_CANDIDATO, NOME_URNA_CANDIDATO, 0, 0, 0, 0, 0, 0
	  FROM dbo.ImportacaoCandidato 
	 WHERE SIGLA_UE = @SIGLA_UE
	   AND ANO_ELEICAO = YEAR(GETDATE())
	   AND CODIGO_CARGO = 11


	SELECT Candidato.ANO_ELEICAO, Candidato.CPF_CANDIDATO, SUM(CAST(Bens.VALOR_BEM AS NUMERIC(12,2))) AS VALOR_BENS
	  INTO #tempCandidatos
	  FROM dbo.ImportacaoCandidato		AS Candidato
	  JOIN dbo.ImportacaoBensCandidato	AS Bens
	    ON Candidato.ANO_ELEICAO = Bens.ANO_ELEICAO
	   AND Candidato.SEQUENCIAL_CANDIDATO  = Bens.SQ_CANDIDATO
	 WHERE CPF_CANDIDATO IN (SELECT CPF FROM @candidatos) 
	 GROUP BY Candidato.CPF_CANDIDATO, Candidato.ANO_ELEICAO

	  
   
	DECLARE @Ano			INT, 
			@CPF			VARCHAR(12),
			@Valor			AS NUMERIC(12,2);    
  

	DECLARE candidato_cursor CURSOR FOR   
	 SELECT CAST(ANO_ELEICAO AS INT), CAST(CPF_CANDIDATO AS VARCHAR(12)), VALOR_BENS 
	   FROM #tempCandidatos 
	  ORDER BY ANO_ELEICAO, CPF_CANDIDATO;  
  
		OPEN candidato_cursor  
  
			FETCH NEXT FROM candidato_cursor   
			INTO @Ano, @CPF, @Valor  
  
			WHILE @@FETCH_STATUS = 0  
			BEGIN  

			UPDATE @candidatos
			   SET Bens2006 = (CASE @Ano WHEN 2006 THEN @Valor ELSE Bens2006 END),
				   Bens2008 = (CASE @Ano WHEN 2008 THEN @Valor ELSE Bens2008 END),
				   Bens2010 = (CASE @Ano WHEN 2010 THEN @Valor ELSE Bens2010 END),
				   Bens2012 = (CASE @Ano WHEN 2012 THEN @Valor ELSE Bens2012 END),
				   Bens2014 = (CASE @Ano WHEN 2014 THEN @Valor ELSE Bens2014 END),
				   Bens2016 = (CASE @Ano WHEN 2016 THEN @Valor ELSE Bens2016 END)

			 WHERE CPF = @CPF 

			 
				FETCH NEXT FROM candidato_cursor   
				INTO @Ano, @CPF, @Valor
			END


		   
	CLOSE candidato_cursor;  
	DEALLOCATE candidato_cursor;  


 
	SELECT *
	  FROM @candidatos

END  
GO
