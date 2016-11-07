DELETE FROM TDCandidato;
GO

DBCC CHECKIDENT ('[TDCandidato]', RESEED, 0);
GO

INSERT INTO TDCandidato
SELECT ImportacaoCandidato.CPF_CANDIDATO			AS CPF_CANDIDATO, 
	   MAX(ImportacaoCandidato.Nome_urna_candidato) AS NOME_URNA_CANDIDATO,
	   MAX(ImportacaoCandidato.descricao_Sexo)		AS DESCRICAO_SEXO
  
  FROM ImportacaoCandidato

  JOIN (SELECT  CPF_CANDIDATO, 
				MAX(ANO_ELEICAO) AS ANO_ELEICAO 
		  
		  FROM ImportacaoCandidato 
		 GROUP BY CPF_CANDIDATO
	    ) UltimaCandidatura
   
    ON ImportacaoCandidato.CPF_CANDIDATO = UltimaCandidatura.CPF_CANDIDATO 
   AND ImportacaoCandidato.ANO_ELEICAO  = UltimaCandidatura.ANO_ELEICAO
 WHERE (ISNULL(ImportacaoCandidato.CPF_CANDIDATO,'#NULO#') <> '#NULO#' )
 GROUP BY  ImportacaoCandidato.CPF_CANDIDATO
 ORDER BY Nome_urna_candidato