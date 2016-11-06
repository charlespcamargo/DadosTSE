DELETE FROM BensCandidadtoAgrupados;

INSERT INTO BensCandidadtoAgrupados (ANO_ELEICAO,
SIGLA_UF,
SQ_CANDIDATO,
VLRTOTAL, QTDTOTAL)
SELECT  ANO_ELEICAO, SIGLA_UF, SQ_CANDIDATO, SUM(CAST(VALOR_BEM AS NUMERIC(16,2))) AS VLRTOTAL, COUNT(1) as QTDTOTAL  FROM ImportacaoBensCandidato
GROUP BY  ANO_ELEICAO, SIGLA_UF, SQ_CANDIDATO

UPDATE BensCandidadtoAgrupados SET QTDTOTAL = 0 where VLRTOTAL = 0
--UPDATE ImportacaoCandidato SET ANO = CAST(ANO_ELEICAO AS INT)


Update OcupacaoValor  
	Set OcupacaoID=TDOcupacao.ID
	From OcupacaoValor Inner Join TDOcupacao
			On OcupacaoValor.Descricao = TDOcupacao.Descricao AND 
			OcupacaoValor.ValorMedio = TDOcupacao.VlrMedioDeclarado