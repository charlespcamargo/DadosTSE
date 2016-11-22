SELECT candidato.Nome            AS [Nome Na Urna],
    fato.Ano              AS [Ano],     
    fato.VlrTotalDeclarado   AS [Valor Total de Bens],
    fato.QtdTotalDeclarado          AS [Quantidade de Bens]

  FROM dbo.TFDadoEleitoral    AS fato
  JOIN dbo.TDLocalidade     AS localidade
    ON localidade.ID = fato.LocalidadeID
  JOIN dbo.TDCargoPolitico    AS cargo
    ON cargo.ID = fato.CargoPoliticoID
  JOIN dbo.TPCandidatoEscolaridade  AS candidato_escolaridade
    ON fato.CandidatoEscolaridadeID = candidato_escolaridade.ID
  JOIN dbo.TDCandidato     AS candidato
    ON candidato.ID = candidato_escolaridade.CandidatoID
  JOIN dbo.TDEscolaridade    AS escolaridade
    ON escolaridade.ID = candidato_escolaridade.EscolaridadeID
  JOIN dbo.TDOcupacao     AS ocupacao
    ON ocupacao.ID = fato.OcupacaoID
 WHERE localidade.SiglaEstado = 'RJ'
   AND localidade.Municipio = 'RIO DE JANEIRO'
   AND candidato.Nome IN ('PEDRO PAULO', 'MARCELO FREIXO','JANDIRA FEGHALI','ALESSANDRO MOLON','CRIVELLA')
 ORDER BY candidato.Nome, fato.Ano


-- select * from ImportacaoCandidato where NOME_CANDIDATO = 'MARCELO RIBEIRO FREIXO' and ANO_ELEICAO = 2006
--
-- select sum(CAST(valor_beM AS NUMERIC(16,2)))  from ImportacaoBensCandidato where SQ_CANDIDATO = '11049' and ANO_ELEICAO = 2006 and SIGLA_UF = 'RJ'
--
-- select sum(CAST(valor_beM AS NUMERIC(16,2)))  from ImportacaoBensCandidato where SQ_CANDIDATO = '11049' and ANO_ELEICAO = 2006 --and SIGLA_UF = 'RJ'
