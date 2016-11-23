-- ================================================
-- 
-- ================================================
-- =============================================
-- Author:		GRUPO - DADOS ELEITORIAS
-- Create date: 20/11/2016
-- Description:	Procedure para responder os dados da primeira Consulta
-- =============================================
IF EXISTS (SELECT 1 FROM sys.Objects WHERE  Object_id = OBJECT_ID(N'[SP_ANALISE1_1]') AND Type = N'P')
BEGIN
   DROP PROCEDURE SP_ANALISE1_1
END
GO

CREATE PROCEDURE SP_ANALISE1_1
(
	@ANO INT = NULL,
	@TODOSANOS BIT = NULL,
	@SEXO VARCHAR(20) = NULL,
	@IDESCOLARIDADE INT = NULL,
	@IDOCUPACAO INT = NULL,
	@REGIAO VARCHAR(50) = NULL,
	@SIGLAESTADO VARCHAR(2) = NULL,
	@IDMUNICIPIO INT = NULL,
	@SIGLAPARTIDO VARCHAR(50) = NULL,
	@IDCARGOPRETENDIDO INT = NULL
)
AS
BEGIN

	/*
		EXEC SP_ANALISE1_1		@ANO				= 2016,
								@TODOSANOS			= 1,
								@SEXO				= NULL,
								@IDESCOLARIDADE		= NULL,
								@IDOCUPACAO			= NULL,
								@REGIAO				= NULL,
								@SIGLAESTADO		= NULL,
								@IDMUNICIPIO		= 4884,
								@SIGLAPARTIDO		= NULL,
								@IDCARGOPRETENDIDO	= 8

	*/

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


IF ( ISNULL(@TODOSANOS, 0) = 0 )
BEGIN 
    -- Insert statements for procedure here
	SELECT  TFDadoEleitoral.Ano													AS Ano, 
			TDLocalidade.Regiao													AS Regiao, 
			TDLocalidade.SiglaEstado											AS SiglaEstado, 
			TDLocalidade.Municipio												AS Municipio,
			TDCandidato.Nome													AS Nome,
			TDOcupacao.Descricao												AS Ocupacao,
			TDOcupacao.VlrMedioDeclarado										AS VlrMedioOcupacao,	   
			TFDadoEleitoral.VlrTotalDeclarado									AS VlrTotalDeclarado,
			(TFDadoEleitoral.VlrTotalDeclarado - TDOcupacao.VlrMedioDeclarado)  AS DiferencaMedia ,
			TDCargoPolitico.Descricao											AS CargoPolitico,
			TDPartidoColigacao.Partido											AS Partido,
			TDCandidato.CPF														AS CPF

	FROM TFDadoEleitoral
    JOIN TPCandidatoEscolaridade 
	  ON TFDadoEleitoral.CandidatoEscolaridadeID = TPCandidatoEscolaridade.ID
	JOIN TDCandidato 
	  ON TPCandidatoEscolaridade.CandidatoID = TDCandidato.ID
	JOIN TDCargoPolitico 
	  ON TFDadoEleitoral.CargoPoliticoID = TDCargoPolitico.ID
	JOIN TDLocalidade 
	  ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
	JOIN TDOcupacao 
	  ON TFDadoEleitoral.OcupacaoID = TDOcupacao.ID
	JOIN TDPartidoColigacao 
	  ON TFDadoEleitoral.PartidoColigacaoID = TDPartidoColigacao.ID   
   WHERE ISNULL(@ANO, TFDadoEleitoral.Ano) = TFDadoEleitoral.Ano
	 AND ISNULL(@SEXO,TDCandidato.Sexo) = TDCandidato.Sexo		
	 AND ISNULL(@IDESCOLARIDADE,TPCandidatoEscolaridade.EscolaridadeID) = TPCandidatoEscolaridade.EscolaridadeID
	 AND ISNULL(@IDOCUPACAO,TDOcupacao.ID) = TDOcupacao.ID
	 AND ISNULL(@REGIAO,TDLocalidade.Regiao) = TDLocalidade.Regiao
	 AND ISNULL(@SIGLAESTADO,TDLocalidade.SiglaEstado) = TDLocalidade.SiglaEstado
	 AND ISNULL(@IDMUNICIPIO,TDLocalidade.ID) = TDLocalidade.ID
	 AND ISNULL(@SIGLAPARTIDO,TDPartidoColigacao.Partido) = TDPartidoColigacao.Partido
	 AND ISNULL(@IDCARGOPRETENDIDO,TDCargoPolitico.ID) = TDCargoPolitico.ID

		
		ORDER BY TFDadoEleitoral.Ano, 
			   TDLocalidade.Regiao, 
			   TDLocalidade.SiglaEstado, 
			   TDLocalidade.Municipio,
			   DiferencaMedia DESC
END
ELSE
BEGIN

	SELECT  TFDadoEleitoral.Ano													AS Ano, 
			TDLocalidade.Regiao													AS Regiao, 
			TDLocalidade.SiglaEstado											AS SiglaEstado, 
			TDLocalidade.Municipio												AS Municipio,
			TDCandidato.Nome													AS Nome,
			TDOcupacao.Descricao												AS Ocupacao,
			TDOcupacao.VlrMedioDeclarado										AS VlrMedioOcupacao,	   
			TFDadoEleitoral.VlrTotalDeclarado									AS VlrTotalDeclarado,
			(TFDadoEleitoral.VlrTotalDeclarado - TDOcupacao.VlrMedioDeclarado)  AS DiferencaMedia ,
			TDCargoPolitico.Descricao											AS CargoPolitico,
			TDPartidoColigacao.Partido											AS Partido,
			TDCandidato.CPF														AS CPF
	   
	FROM TFDadoEleitoral
    JOIN TPCandidatoEscolaridade 
	  ON TFDadoEleitoral.CandidatoEscolaridadeID = TPCandidatoEscolaridade.ID
	JOIN TDCandidato 
	  ON TPCandidatoEscolaridade.CandidatoID = TDCandidato.ID
	JOIN TDCargoPolitico 
	  ON TFDadoEleitoral.CargoPoliticoID = TDCargoPolitico.ID
	JOIN TDLocalidade 
	  ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
	JOIN TDOcupacao 
	  ON TFDadoEleitoral.OcupacaoID = TDOcupacao.ID
	JOIN TDPartidoColigacao 
	  ON TFDadoEleitoral.PartidoColigacaoID = TDPartidoColigacao.ID   
   WHERE TDCandidato.CPF IN (
								  SELECT DISTINCT TDCandidato.CPF			   
									FROM TFDadoEleitoral		 
									JOIN TPCandidatoEscolaridade 
									  ON TFDadoEleitoral.CandidatoEscolaridadeID = TPCandidatoEscolaridade.ID
									JOIN TDCandidato 
									  ON TPCandidatoEscolaridade.CandidatoID = TDCandidato.ID
									JOIN TDCargoPolitico 
									  ON TFDadoEleitoral.CargoPoliticoID = TDCargoPolitico.ID
									JOIN TDLocalidade 
									  ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
									JOIN TDOcupacao 
									  ON TFDadoEleitoral.OcupacaoID = TDOcupacao.ID
									JOIN TDPartidoColigacao 
									  ON TFDadoEleitoral.PartidoColigacaoID = TDPartidoColigacao.ID   

								   WHERE ISNULL(@ANO, TFDadoEleitoral.Ano) = TFDadoEleitoral.Ano
									 AND ISNULL(@SEXO,TDCandidato.Sexo) = TDCandidato.Sexo		
									 AND ISNULL(@IDESCOLARIDADE,TPCandidatoEscolaridade.EscolaridadeID) = TPCandidatoEscolaridade.EscolaridadeID
									 AND ISNULL(@IDOCUPACAO,TDOcupacao.ID) = TDOcupacao.ID
									 AND ISNULL(@REGIAO,TDLocalidade.Regiao) = TDLocalidade.Regiao
									 AND ISNULL(@SIGLAESTADO,TDLocalidade.SiglaEstado) = TDLocalidade.SiglaEstado
									 AND ISNULL(@IDMUNICIPIO,TDLocalidade.ID) = TDLocalidade.ID
									 AND ISNULL(@SIGLAPARTIDO,TDPartidoColigacao.Partido) = TDPartidoColigacao.Partido
									 AND ISNULL(@IDCARGOPRETENDIDO,TDCargoPolitico.ID) = TDCargoPolitico.ID
							)

		
		ORDER BY TFDadoEleitoral.Ano, 
			   TDLocalidade.Regiao, 
			   TDLocalidade.SiglaEstado, 
			   TDLocalidade.Municipio,
			   DiferencaMedia DESC

  

END


END
GO