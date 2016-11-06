DELETE FROM TFDADOELEITORAL;
GO
INSERT INTO TFDADOELEITORAL (Ano,
CandidatoEscolaridadeID,
PartidoColigacaoID,
LocalidadeID,
OcupacaoID,
CargoPoliticoID,
VlrTotalDeclarado,
QtdTotalDeclarado)
SELECT 
 CAST(ImportacaoCandidato.ANO_ELEICAO AS INT) AS ANO,
     TPCandidatoEscolaridade.ID      AS CandidatoEscolaridadeID,
 -- ,
  TDPARTIDOCOLIGACAO.ID       AS PARTIDOCOLIGACAOID,
--  ,
  TDLocalidade.ID         AS LocalidadeID,
  TDOcupacao.ID         AS OcupacaoID,-- 1, 0, 0
 TDCargoPolitico.ID        AS CargoPoliticoID,
  SUM
 (
 ---- CAST(VALOR_BEM AS NUMERIC(16, 2))
 VLRTOTAL
 
  ) 
    AS VlrTotalDeclarado,
  SUM
 (
  QTDTOTAL
  )          AS QtdTotalDeclarado
 ---- ,SEQUENCIAL_CANDIDATO, *
 FROM TPCandidatoEscolaridade WITH(NOLOCK)
INNER JOIN TDCandidato  WITH(NOLOCK)
   ON TPCandidatoEscolaridade.CandidatoID = TDCandidato.ID
INNER JOIN TDEscolaridade WITH(NOLOCK)
   ON TPCandidatoEscolaridade.EscolaridadeID = TDEscolaridade.ID
INNER JOIN ImportacaoCandidato WITH(NOLOCK)
   ON ImportacaoCandidato.CPF_CANDIDATO = TDCandidato.CPF 
  AND ImportacaoCandidato.DESCRICAO_GRAU_INSTRUCAO = TDEscolaridade.Descricao
INNER JOIN TDPARTIDOCOLIGACAO WITH(NOLOCK)
   ON ImportacaoCandidato.SIGLA_PARTIDO = TDPARTIDOCOLIGACAO.Partido 
  AND ImportacaoCandidato.COMPOSICAO_LEGENDA = TDPARTIDOCOLIGACAO.Coligacao
INNER JOIN ImportacaoVaga WITH(NOLOCK)
   ON ImportacaoCandidato.CODIGO_CARGO = ImportacaoVaga.CODIGO_CARGO
  AND ImportacaoCandidato.ANO_ELEICAO = ImportacaoVaga.ANO_ELEICAO
  AND ImportacaoCandidato.SIGLA_UE = ImportacaoVaga.SIGLA_UE
  AND ImportacaoCandidato.SIGLA_UF = ImportacaoVaga.SIGLA_UF
INNER JOIN TDLocalidade WITH(NOLOCK)
   ON ImportacaoVaga.NOME_UE = TDLocalidade.Municipio  
  AND ImportacaoVaga.SIGLA_UF = TDLocalidade.SiglaEstado  
INNER JOIN OcupacaoValor WITH(NOLOCK)
   ON ImportacaoCandidato.ANO = OcupacaoValor.ANO
  AND ImportacaoCandidato.CODIGO_OCUPACAO = OcupacaoValor.Codigo
INNER JOIN TDOcupacao WITH(NOLOCK)
  ON OcupacaoValor.OcupacaoID = TDOcupacao.ID
INNER JOIN TDCargoPolitico WITH(NOLOCK)
   ON ImportacaoVaga.DESCRICAO_CARGO = TDCargoPolitico.Descricao
INNER JOIN BensCandidadtoAgrupados WITH(NOLOCK)
   ON ImportacaoCandidato.SEQUENCIAL_CANDIDATO = BensCandidadtoAgrupados.SQ_CANDIDATO
  AND ImportacaoCandidato.ANO_ELEICAO = BensCandidadtoAgrupados.ANO_ELEICAO
  AND ImportacaoCandidato.SIGLA_UF = BensCandidadtoAgrupados.SIGLA_UF
--WHERE ImportacaoCandidato.CPF_CANDIDATO = '28341228220' AND ImportacaoCandidato.ANO_ELEICAO = 2006

GROUP BY ImportacaoCandidato.ANO_ELEICAO,
   TPCandidatoEscolaridade.ID
   ,
   TDPARTIDOCOLIGACAO.ID ,
   TDLocalidade.ID ,
    TDOcupacao.ID ,
    TDCargoPolitico.ID



--SELECT * FROM TFDADOELEITORAL

--1427075
--1409515
--1409515
--1409422 2MINUTOS
--1409422 2:13MINUTOS
--1409422 2:28MINUTOS
--913049 2:08MINUTOS
--914690 2:31MINUTOS
--914690 2:36MINUTOS