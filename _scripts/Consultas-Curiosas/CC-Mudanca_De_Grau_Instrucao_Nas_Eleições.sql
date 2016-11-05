 SELECT CPF_CANDIDATO, COUNT(descricao_grau_instrucao) QtdDiferentes, MIN(DATA_NASCIMENTO) DtNascimento, 
	max(descricao_grau_instrucao) Instrucao1, min(descricao_grau_instrucao) Instrucao2 FROM ImportacaoCandidato
 GROUP BY  CPF_CANDIDATO
 HAVING COUNT(descricao_grau_instrucao) > 1 AND max(descricao_grau_instrucao) <> min(descricao_grau_instrucao)