DELETE FROM TFDADOELEITORAL;
GO
DBCC CHECKIDENT ('[TFDADOELEITORAL]', RESEED, 0);
GO
INSERT INTO TFDADOELEITORAL
SELECT  CAST(ImportacaoCandidato.ANO_ELEICAO AS INT) AS ANO,
	    TPCandidatoEscolaridade.ID AS CandidatoEscolaridadeID,
		TDPARTIDOCOLIGACAO.ID AS PARTIDOCOLIGACAOID,
		TDLocalidade.ID AS LocalidadeID,
		TDOcupacao.ID as OcupacaoID,
		TDCargoPolitico.ID AS CargoPoliticoID,
		SUM(cast(VALOR_BEM as numeric(16,2))) AS VlrTotalDeclarado,
		COUNT(1) AS QtdTotalDeclarado
FROM TPCandidatoEscolaridade
INNER JOIN TDCandidato ON TPCandidatoEscolaridade.CandidatoID = TDCandidato.ID
INNER JOIN TDEscolaridade ON TPCandidatoEscolaridade.EscolaridadeID = TDEscolaridade.ID
INNER JOIN ImportacaoCandidato ON ImportacaoCandidato.CPF_CANDIDATO = TDCandidato.CPF 
	AND ImportacaoCandidato.DESCRICAO_GRAU_INSTRUCAO = TDEscolaridade.Descricao
INNER JOIN TDPARTIDOCOLIGACAO ON ImportacaoCandidato.SIGLA_PARTIDO = TDPARTIDOCOLIGACAO.Partido 
	AND ImportacaoCandidato.COMPOSICAO_LEGENDA = TDPARTIDOCOLIGACAO.Coligacao
INNER JOIN ImportacaoVaga ON ImportacaoCandidato.CODIGO_CARGO = ImportacaoVaga.CODIGO_CARGO
	AND ImportacaoVaga.ANO_ELEICAO = ImportacaoCandidato.ANO_ELEICAO
INNER JOIN TDLocalidade ON ImportacaoVaga.NOME_UE = TDLocalidade.Municipio  
	AND ImportacaoVaga.SIGLA_UF = TDLocalidade.SiglaEstado  
INNER JOIN OcupacaoValor ON ImportacaoCandidato.ANO_ELEICAO = OcupacaoValor.ANO
	AND ImportacaoCandidato.DESCRICAO_OCUPACAO = OcupacaoValor.Descricao
INNER JOIN TDOcupacao ON OcupacaoValor.Descricao = TDOcupacao.Descricao 
	AND OcupacaoValor.ValorMedio = TDOcupacao.VlrMedioDeclarado
INNER JOIN TDCargoPolitico ON ImportacaoVaga.DESCRICAO_CARGO = TDCargoPolitico.Descricao
INNER JOIN ImportacaoBensCandidato ON ImportacaoCandidato.SEQUENCIAL_CANDIDATO = ImportacaoBensCandidato.SQ_CANDIDATO
	AND ImportacaoBensCandidato.ANO_ELEICAO = ImportacaoCandidato.ANO_ELEICAO

group by ImportacaoCandidato.ANO_ELEICAO,
		TPCandidatoEscolaridade.ID,
		TDPARTIDOCOLIGACAO.ID ,
		TDLocalidade.ID ,
		TDOcupacao.ID ,
		TDCargoPolitico.ID