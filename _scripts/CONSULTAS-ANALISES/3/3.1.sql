-- ================================================
-- 
-- ================================================
-- =============================================
-- Author:		GRUPO - DADOS ELEITORIAS
-- Create date: 20/11/2016
-- Description:	Procedure para responder os dados da primeira Consulta
-- =============================================

IF EXISTS (SELECT 1 FROM sys.Objects WHERE  Object_id = OBJECT_ID(N'[SP_ANALISE3_1]') AND Type = N'P')
BEGIN
   DROP PROCEDURE SP_ANALISE3_1
END
GO

CREATE PROCEDURE SP_ANALISE3_1(
	@ANO INT = NULL,
	@SEXO VARCHAR(20) = NULL,
	@IDESCOLARIDADE INT = NULL,
	@OCUPACAO VARCHAR(100) = NULL,
	@REGIAO VARCHAR(50) = NULL,
	@SIGLAESTADO VARCHAR(2) = NULL,
	@IDMUNICIPIO INT = NULL,
	@SIGLAPARTIDO VARCHAR(50) = NULL,
	@IDCARGOPRETENDIDO INT = NULL)
AS
BEGIN

	/*
		EXEC SP_ANALISE3_1		@ANO				= NULL,
								@SEXO				= NULL,
								@IDESCOLARIDADE		= NULL,
								@OCUPACAO			= NULL,
								@REGIAO				= NULL,
								@SIGLAESTADO		= NULL,
								@IDMUNICIPIO		= NULL,
								@SIGLAPARTIDO		= NULL,
								@IDCARGOPRETENDIDO	= NULL

	*/

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT Ano, 
	   Regiao, 
	   SiglaEstado, 
	   Municipio,
	   CargoPolitico,
	   Partido,
	   Escolaridade,
	   QtdMasculino,
	   QtdFeminino,
	   QtdTotal ,
	   QtdFeminino/(QtdTotal + 0.000) * 100.0 AS PercentualFeminino,
	   QtdMasculino/(QtdTotal + 0.000)* 100.0 AS PercentualMasculino
		
	   FROM (
    -- Insert statements for procedure here
	SELECT TFDadoEleitoral.Ano, 
	   TDLocalidade.Regiao, 
	   TDLocalidade.SiglaEstado, 
	   TDLocalidade.Municipio,
	   TDCargoPolitico.Descricao as CargoPolitico,
	   TDPartidoColigacao.Partido,
	   TDEscolaridade.Descricao AS Escolaridade,
	   TDEscolaridade.Nivel AS NivelEscolaridade,
	   SUM(CASE 
			WHEN TDCandidato.Sexo = 'MASCULINO' THEN
					1
			ELSE
				0
		END) as QtdMasculino,
		SUM(CASE 
			WHEN TDCandidato.Sexo = 'FEMININO' THEN
					1
			ELSE
				0
		END)
	    as QtdFeminino,
	   COUNT(1) AS QtdTotal
	    FROM TFDadoEleitoral
		INNER JOIN TPCandidatoEscolaridade ON TFDadoEleitoral.CandidatoEscolaridadeID = TPCandidatoEscolaridade.ID
		INNER JOIN TDCandidato ON TPCandidatoEscolaridade.CandidatoID = TDCandidato.ID
		INNER JOIN TDEscolaridade ON TPCandidatoEscolaridade.EscolaridadeID = TDEscolaridade.ID
		INNER JOIN TDCargoPolitico ON TFDadoEleitoral.CargoPoliticoID = TDCargoPolitico.ID
		INNER JOIN TDLocalidade ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
		INNER JOIN TDOcupacao ON TFDadoEleitoral.OcupacaoID = TDOcupacao.ID
		INNER JOIN TDPartidoColigacao ON TFDadoEleitoral.PartidoColigacaoID = TDPartidoColigacao.ID
		WHERE 	
				ISNULL(@ANO,TFDadoEleitoral.Ano) = TFDadoEleitoral.Ano
			AND 
				ISNULL(@SEXO,TDCandidato.Sexo) = TDCandidato.Sexo		
			AND 
				ISNULL(@IDESCOLARIDADE,TPCandidatoEscolaridade.EscolaridadeID) = TPCandidatoEscolaridade.EscolaridadeID
			AND 
				ISNULL(@OCUPACAO,TDOcupacao.Descricao) = TDOcupacao.Descricao
			AND 
				ISNULL(@REGIAO,TDLocalidade.Regiao) = TDLocalidade.Regiao
			AND 
				ISNULL(@SIGLAESTADO,TDLocalidade.SiglaEstado) = TDLocalidade.SiglaEstado
			AND 
				ISNULL(@IDMUNICIPIO,TDLocalidade.ID) = TDLocalidade.ID
			AND 
				ISNULL(@SIGLAPARTIDO,TDPartidoColigacao.Partido) = TDPartidoColigacao.Partido
			AND 
				ISNULL(@IDCARGOPRETENDIDO,TDCargoPolitico.ID) = TDCargoPolitico.ID

		GROUP BY 
		   TFDadoEleitoral.Ano, 
		   TDLocalidade.Regiao, 
		   TDLocalidade.SiglaEstado, 
		   TDLocalidade.Municipio,
		   TDCargoPolitico.Descricao,
		   TDPartidoColigacao.Partido,
		   TDEscolaridade.Descricao, TDEscolaridade.Nivel) RESULTADO
		   
	   ORDER BY Ano, 
	   Regiao, 
	   SiglaEstado, 
	   Municipio,
	   CargoPolitico,
	   Partido,
	   NivelEscolaridade
END