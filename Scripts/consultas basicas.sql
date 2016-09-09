SELECT COUNT(ID) AS QtdArquivo 
  FROM dbo.ImportacaoArquivo WITH(NOLOCK)

SELECT COUNT(ID) AS QtdCandidatos 
  FROM dbo.ImportacaoCandidato WITH(NOLOCK)

SELECT COUNT(ID) AS QtdBensCandidato
  FROM dbo.ImportacaoBensCandidato WITH(NOLOCK)

SELECT COUNT(ID) AS QtdLegendas 
  FROM dbo.ImportacaoLegenda WITH(NOLOCK)

SELECT COUNT(ID) AS QtdVaga 
  FROM dbo.ImportacaoVaga WITH(NOLOCK)
