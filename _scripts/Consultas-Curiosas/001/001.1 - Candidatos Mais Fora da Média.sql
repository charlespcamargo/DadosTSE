DECLARE	@ANO INT = NULL;
DECLARE	@TODOSANOS BIT = NULL;
DECLARE	@SEXO VARCHAR(20) = NULL;
DECLARE	@IDESCOLARIDADE INT = NULL;
DECLARE	@IDOCUPACAO INT = NULL;
DECLARE	@REGIAO VARCHAR(50) = NULL;
DECLARE	@SIGLAESTADO VARCHAR(2) = NULL;
DECLARE	@IDMUNICIPIO INT = NULL;
DECLARE	@SIGLAPARTIDO VARCHAR(50) = NULL;
DECLARE	@IDCARGOPRETENDIDO INT = NULL;




SELECT TOP 100 TFDadoEleitoral.Ano													AS Ano, 
			TDLocalidade.Regiao													AS Regiao, 
			TDLocalidade.SiglaEstado											AS SiglaEstado, 
			TDLocalidade.Municipio												AS Municipio,
			TDCandidato.Nome													AS Nome,
			TDOcupacao.Descricao												AS Ocupacao,
			TDOcupacao.VlrMedioDeclarado										AS VlrMedioOcupacao,	   
			TFDadoEleitoral.VlrTotalDeclarado									AS VlrTotalDeclarado,
			(TFDadoEleitoral.VlrTotalDeclarado / 
							CASE WHEN TDOcupacao.VlrMedioDeclarado = 0 THEN
									1
							ELSE TDOcupacao.VlrMedioDeclarado END
							
							) * 100.0 AS PorcentagemDaMedia ,
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
		
	ORDER BY PorcentagemDaMedia DESC