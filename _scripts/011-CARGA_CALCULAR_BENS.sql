DELETE FROM BensCandidatoAgrupado;

/************************************************************** CALCULA OS VALORES DOS BENS DECLARADOS ********************************************************************/
INSERT INTO BensCandidatoAgrupado (ANO_ELEICAO,SIGLA_UF,SQ_CANDIDATO,VLRTOTAL, QTDTOTAL)
SELECT  CAST(ANO_ELEICAO AS INT)				AS ANO_ELEICAO, 
		SIGLA_UF								AS SIGLA_UF, 
		SQ_CANDIDATO							AS SQ_CANDIDATO, 
		SUM(CAST(VALOR_BEM AS NUMERIC(16,2)))	AS VLRTOTAL, 
		COUNT(1)								AS QTDTOTAL  

 FROM ImportacaoBensCandidato
GROUP BY  ANO_ELEICAO, SIGLA_UF, SQ_CANDIDATO


/********************************************** ZERA A QUANTIDADE PARA AQUELES QUE NÃO DECLARARAM BENS ***********************************************************/
UPDATE BensCandidatoAgrupado 
   SET QTDTOTAL = 0 
 WHERE VLRTOTAL = 0
 
 
 
/********************************************************** CAST DO ANO PARA MAIOR DESEMPENHO NAS CONSULTAS ********************************************************************/
UPDATE ImportacaoCandidato 
   SET ANO = CAST(ANO_ELEICAO AS INT)