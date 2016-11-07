/************************************************************** VALOR M�DIO POR ANO DA OCUPA��O ********************************************************************/
IF EXISTS (SELECT 1 FROM sys.Objects WHERE  Object_id = OBJECT_ID(N'[OcupacaoValor]') AND Type = N'U')
BEGIN
   DROP TABLE OcupacaoValor
END
GO


CREATE TABLE OcupacaoValor
(
	OcupacaoID		    INT,
	Ano					INT NOT NULL,
	Codigo				VARCHAR(100),
	Descricao			VARCHAR(100),
	ValorMedio			NUMERIC(16,2)
)
GO
/************************************************************** VALOR M�DIO POR ANO DA OCUPA��O ********************************************************************/

INSERT INTO dbo.OcupacaoValor(Ano, Codigo, Descricao, ValorMedio)	
SELECT Candidato.ANO_ELEICAO							AS Ano,
	   Candidato.CODIGO_OCUPACAO						AS Codigo,
	   Candidato.DESCRICAO_OCUPACAO						AS Descricao,
	   AVG(CAST(Bens.VALOR_BEM AS NUMERIC(16,2)))		AS ValorMedio

  FROM ImportacaoCandidato										AS Candidato
  JOIN dbo.ImportacaoBensCandidato								AS Bens
    ON Candidato.ANO_ELEICAO = bens.ANO_ELEICAO
   AND Candidato.SEQUENCIAL_CANDIDATO = bens.SQ_CANDIDATO
 GROUP BY Candidato.ANO_ELEICAO,
		  Candidato.CODIGO_OCUPACAO,
		  Candidato.DESCRICAO_OCUPACAO
GO

/************************************************************** VALOR M�DIO POR ANO DA OCUPA��O ********************************************************************/
DELETE FROM TDOcupacao;
GO

DBCC CHECKIDENT ('[TDOcupacao]', RESEED, 1);
GO
/************************************************************** VALOR M�DIO DA OCUPA��O ********************************************************************/ 

INSERT INTO TDOcupacao (Descricao, VlrMedioDeclarado)
SELECT 
	OcupacaoValor.Descricao,
	OcupacaoValor.ValorMedio
FROM OcupacaoValor
