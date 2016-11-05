DELETE FROM TDCandidato;
GO
DBCC CHECKIDENT ('[TDCandidato]', RESEED, 0);
GO
INSERT INTO TDCandidato
SELECT ImportacaoCandidato.CPF_CANDIDATO, 
	MAX(ImportacaoCandidato.Nome_urna_candidato) AS Nome_urna_candidato,
	MAX(ImportacaoCandidato.descricao_Sexo) AS descricao_Sexo
 FROM ImportacaoCandidato

  INNER JOIN (SELECT CPF_CANDIDATO, MAX(ANO_ELEICAO) ANO_ELEICAO FROM

		 ImportacaoCandidato GROUP BY CPF_CANDIDATO) UltimaCandidatura
		 ON ImportacaoCandidato.CPF_CANDIDATO = UltimaCandidatura.CPF_CANDIDATO 
			AND ImportacaoCandidato.ANO_ELEICAO  = UltimaCandidatura.ANO_ELEICAO
where (ImportacaoCandidato.CPF_CANDIDATO <> '#NULO#') and (ImportacaoCandidato.CPF_CANDIDATO is not null)
GROUP BY  ImportacaoCandidato.CPF_CANDIDATO
ORDER BY Nome_urna_candidato