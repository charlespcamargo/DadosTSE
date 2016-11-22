DECLARE	@ANO INT = NULL;
DECLARE	@SEXO VARCHAR(20) = NULL;
DECLARE	@IDESCOLARIDADE INT = (SELECT TOP 1 ID FROM TDEscolaridade WHERE Descricao LIKE '%NULO%');
DECLARE	@IDOCUPACAO INT = (SELECT TOP 1  ID FROM TDOcupacao WHERE Descricao LIKE '%NULO%');
DECLARE	@REGIAO VARCHAR(50) = NULL;
DECLARE	@SIGLAESTADO VARCHAR(2) = NULL;
DECLARE	@IDMUNICIPIO INT = (SELECT  TOP 1 ID FROM TDLocalidade WHERE Municipio LIKE '%NULO%');
DECLARE	@SIGLAPARTIDO VARCHAR(50) = NULL;
DECLARE	@IDCARGOPRETENDIDO INT = (SELECT  TOP 1 ID FROM TDCargoPolitico WHERE Descricao LIKE '%NULO%');

SELECT 
	   SiglaEstado,
	   QtdMasculino,
	   QtdFeminino,
	   QtdTotal ,
	   QtdFeminino/(QtdTotal + 0.000) * 100.0 AS PercentualFeminino,
	   QtdMasculino/(QtdTotal + 0.000)* 100.0 AS PercentualMasculino
		
	   FROM (
    -- Insert statements for procedure here
	SELECT 
	   TDLocalidade.SiglaEstado,
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

		--TDPartidoColigacao.Partido = 'PT' AND TDLocalidade.SiglaEstado = 'SP'

		GROUP BY 
		   TDLocalidade.SiglaEstado) RESULTADO
		   
	   ORDER BY PercentualFeminino DESC