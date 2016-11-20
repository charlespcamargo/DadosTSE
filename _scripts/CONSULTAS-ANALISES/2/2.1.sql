-- ================================================
-- 
-- ================================================
-- =============================================
-- Author:		GRUPO - DADOS ELEITORIAS
-- Create date: 20/11/2016
-- Description:	Procedure para responder os dados da primeira Consulta
-- =============================================
CREATE PROCEDURE SP_ANALISE2_1
	-- Add the parameters for the stored procedure here
	@ANO INT,
	@SEXO VARCHAR(20),
	@IDESCOLARIDADE INT,
	@IDOCUPACAO INT,
	@REGIAO VARCHAR(50),
	@SIGLAESTADO VARCHAR(2),
	@MUNICIPIO INT,
	@PARTIDO VARCHAR(50),
	@CARGOPRETENDIDO INT
AS
BEGIN

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
				ISNULL(@MUNICIPIO,TDLocalidade.ID) = TDLocalidade.ID
			AND 
				ISNULL(@MUNICIPIO,TDPartidoColigacao.Partido) = TDPartidoColigacao.Partido
			AND 
				ISNULL(@CARGOPRETENDIDO,TDCargoPolitico.ID) = TDCargoPolitico.ID

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