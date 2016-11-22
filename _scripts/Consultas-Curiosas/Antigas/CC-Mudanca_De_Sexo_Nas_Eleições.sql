 SELECT CPF_CANDIDATO, COUNT(descricao_Sexo) QtdDiferentes, MIN(DATA_NASCIMENTO) DtNascimento, 
	max(descricao_Sexo) Instrucao1, min(descricao_Sexo) Instrucao2 FROM ImportacaoCandidato
 GROUP BY  CPF_CANDIDATO
 HAVING COUNT(descricao_Sexo) > 1 AND max(descricao_Sexo) <> min(descricao_Sexo)