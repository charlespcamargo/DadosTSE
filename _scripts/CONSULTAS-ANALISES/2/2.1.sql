-- ================================================
-- 
-- ================================================
-- =============================================
-- Author:		GRUPO - DADOS ELEITORIAS
-- Create date: 20/11/2016
-- Description:	Procedure para responder os dados da primeira Consulta
-- =============================================

IF EXISTS (SELECT 1 FROM sys.Objects WHERE  Object_id = OBJECT_ID(N'[SP_ANALISE2_1]') AND Type = N'P')
BEGIN
   DROP PROCEDURE SP_ANALISE2_1
END
GO


CREATE PROCEDURE SP_ANALISE2_1(
	@ANO INT = NULL,
	@SEXO VARCHAR(20) = NULL,
	@IDESCOLARIDADE INT = NULL,
	@IDOCUPACAO INT = NULL,
	@REGIAO VARCHAR(50) = NULL,
	@SIGLAESTADO VARCHAR(2) = NULL,
	@IDMUNICIPIO INT = NULL,
	@SIGLAPARTIDO VARCHAR(50) = NULL,
	@IDCARGOPRETENDIDO INT = NULL)
AS
BEGIN

	/*
		EXEC SP_ANALISE2_1		@ANO				= NULL,
								@SEXO				= NULL,
								@IDESCOLARIDADE		= NULL,
								@IDOCUPACAO			= NULL,
								@REGIAO				= NULL,
								@SIGLAESTADO		= NULL,
								@IDMUNICIPIO		= NULL,
								@SIGLAPARTIDO		= NULL,
								@IDCARGOPRETENDIDO	= NULL

	*/


	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TFDadoEleitoral.Ano, 
	   TDLocalidade.Regiao, 
	   TDLocalidade.SiglaEstado, 
	   TDLocalidade.Municipio,
	   TDCargoPolitico.Descricao as CargoPolitico,
	   TDPartidoColigacao.Partido,
	   TDEscolaridade.Descricao AS Escolaridade,
	   COUNT(1) AS Quantidade,
	   AG_ANO_LOCAL_CARGO_PARTIDO.QtdCandidatos as Total,
	   (COUNT(1) / (AG_ANO_LOCAL_CARGO_PARTIDO.QtdCandidatos + 0.0)) * 100.0 AS Percentual,
	   (RIGHT('000' + CAST(COUNT(1) AS VARCHAR(20)), 3) + ' de ' + RIGHT('000' + CAST(AG_ANO_LOCAL_CARGO_PARTIDO.QtdCandidatos AS VARCHAR(20)), 3) ) AS Fracao
	   


	    FROM TFDadoEleitoral
		INNER JOIN TPCandidatoEscolaridade ON TFDadoEleitoral.CandidatoEscolaridadeID = TPCandidatoEscolaridade.ID
		INNER JOIN TDCandidato ON TPCandidatoEscolaridade.CandidatoID = TDCandidato.ID
		INNER JOIN TDEscolaridade ON TPCandidatoEscolaridade.EscolaridadeID = TDEscolaridade.ID
		INNER JOIN TDCargoPolitico ON TFDadoEleitoral.CargoPoliticoID = TDCargoPolitico.ID
		INNER JOIN TDLocalidade ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
		INNER JOIN TDOcupacao ON TFDadoEleitoral.OcupacaoID = TDOcupacao.ID
		INNER JOIN TDPartidoColigacao ON TFDadoEleitoral.PartidoColigacaoID = TDPartidoColigacao.ID
		INNER JOIN AG_ANO_LOCAL_CARGO_PARTIDO ON TFDadoEleitoral.Ano = AG_ANO_LOCAL_CARGO_PARTIDO.ANO AND 
			   TDLocalidade.ID = AG_ANO_LOCAL_CARGO_PARTIDO.LocalidadeID AND
			   TDCargoPolitico.ID = AG_ANO_LOCAL_CARGO_PARTIDO.CargoPoliticoID AND
			   TDPartidoColigacao.ID = AG_ANO_LOCAL_CARGO_PARTIDO.PartidoColigacaoID
		WHERE 	
				ISNULL(@ANO,TFDadoEleitoral.Ano) = TFDadoEleitoral.Ano
			AND 
				ISNULL(@SEXO,TDCandidato.Sexo) = TDCandidato.Sexo		
			AND 
				ISNULL(@IDESCOLARIDADE,TPCandidatoEscolaridade.EscolaridadeID) = TPCandidatoEscolaridade.EscolaridadeID
			AND 
				ISNULL(@IDOCUPACAO,TDOcupacao.ID) = TDOcupacao.ID
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

		--TDPartidoColigacao.Partido = 'PT' AND TDCargoPolitico.Descricao = 'VEREADOR' AND Municipio = 'RIO DE JANEIRO'

		GROUP BY TFDadoEleitoral.Ano, 
			   TDLocalidade.Regiao, 
			   TDLocalidade.SiglaEstado, 
			   TDLocalidade.Municipio,
			   TDCargoPolitico.Descricao,
			   TDPartidoColigacao.Partido,
			   AG_ANO_LOCAL_CARGO_PARTIDO.QtdCandidatos,
			   TDEscolaridade.Descricao,
			   TDEscolaridade.Nivel
		ORDER BY TFDadoEleitoral.Ano, 
			   TDLocalidade.Regiao, 
			   TDLocalidade.SiglaEstado, 
			   TDLocalidade.Municipio,
			   TDCargoPolitico.Descricao,
			   TDPartidoColigacao.Partido,
			   TDEscolaridade.Nivel
END
--GO

--SELECT * FROM TDCargoPolitico



--SELECT TFDadoEleitoral.Ano, 
--	   TDLocalidade.Regiao, 
--	   TDLocalidade.SiglaEstado, 
--	   TDLocalidade.Municipio,
--	   TDCargoPolitico.Descricao,
--	   COUNT(1) AS Quantidade
--	    FROM TFDadoEleitoral
--INNER JOIN TPCandidatoEscolaridade ON TFDadoEleitoral.CandidatoEscolaridadeID = TPCandidatoEscolaridade.ID
--INNER JOIN TDCandidato ON TPCandidatoEscolaridade.CandidatoID = TDCandidato.ID
--INNER JOIN TDEscolaridade ON TPCandidatoEscolaridade.EscolaridadeID = TDEscolaridade.ID
--INNER JOIN TDCargoPolitico ON TFDadoEleitoral.CargoPoliticoID = TDCargoPolitico.ID
--INNER JOIN TDLocalidade ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
--INNER JOIN TDOcupacao ON TFDadoEleitoral.OcupacaoID = TDOcupacao.ID
--INNER JOIN TDPartidoColigacao ON TFDadoEleitoral.PartidoColigacaoID = TDPartidoColigacao.ID
--GROUP BY TFDadoEleitoral.Ano, 
--	   TDLocalidade.Regiao, 
--	   TDLocalidade.SiglaEstado, 
--	   TDLocalidade.Municipio,
--	   TDCargoPolitico.Descricao,
--	   TDPartidoColigacao.Partido
--ORDER BY TFDadoEleitoral.Ano, 
--	   TDLocalidade.Regiao, 
--	   TDLocalidade.SiglaEstado, 
--	   TDLocalidade.Municipio,
--	   TDCargoPolitico.Descricao