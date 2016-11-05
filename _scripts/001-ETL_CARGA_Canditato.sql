INSERT INTO TDCandidato values
SELECT distinct ImportacaoCandidato.CPF_CANDIDATO, 
	ImportacaoCandidato.Nome_urna_candidato,
	ImportacaoCandidato.descricao_Sexo
 FROM ImportacaoCandidato

  INNER JOIN (SELECT CPF_CANDIDATO, MAX(ANO_ELEICAO) ANO_ELEICAO FROM

		 ImportacaoCandidato GROUP BY CPF_CANDIDATO) UltimaCandidatura
		 ON ImportacaoCandidato.CPF_CANDIDATO = UltimaCandidatura.CPF_CANDIDATO 
			AND ImportacaoCandidato.ANO_ELEICAO  = UltimaCandidatura.ANO_ELEICAO
  
ORDER BY Nome_urna_candidato
  


 SELECT CPF_CANDIDATO, COUNT(descricao_grau_instrucao) QtdDiferentes, MIN(DATA_NASCIMENTO) DtNascimento, 
	max(descricao_grau_instrucao) Instrucao1, min(descricao_grau_instrucao) Instrucao2 FROM ImportacaoCandidato
 GROUP BY  CPF_CANDIDATO
 HAVING COUNT(descricao_grau_instrucao) > 1 AND max(descricao_grau_instrucao) <> min(descricao_grau_instrucao)


 SELECT DISTINCT descricao_grau_instrucao FROM ImportacaoCandidato

 SELECT * FROM ImportacaoCandidato WHERE descricao_grau_instrucao = 'MASCULINO'
 UPDATE ImportacaoCandidato set deSCRICAO_SEXO = 'MASCULINO' where descricao_grau_instrucao = '2'


 UPDATE ImportacaoCandidato
 SET COD_SITUACAO_CANDIDATURA	=	DES_SITUACAO_CANDIDATURA,
DES_SITUACAO_CANDIDATURA	=	NUMERO_PARTIDO				,
NUMERO_PARTIDO	=	SIGLA_PARTIDO							,
SIGLA_PARTIDO	=	NOME_PARTIDO							,
NOME_PARTIDO	=	CODIGO_LEGENDA							,
CODIGO_LEGENDA	=	SIGLA_LEGENDA							,
SIGLA_LEGENDA	=	COMPOSICAO_LEGENDA						,
COMPOSICAO_LEGENDA	=	NOME_LEGENDA						,
NOME_LEGENDA	=	CODIGO_OCUPACAO							,
CODIGO_OCUPACAO	=	DESCRICAO_OCUPACAO						,
DESCRICAO_OCUPACAO	=	DATA_NASCIMENTO						,
DATA_NASCIMENTO	=	NUM_TITULO_ELEITORAL_CANDIDATO			,
NUM_TITULO_ELEITORAL_CANDIDATO	=	IDADE_DATA_ELEICAO		,
IDADE_DATA_ELEICAO	=	CODIGO_SEXO							,
CODIGO_SEXO	=	DESCRICAO_SEXO								,
DESCRICAO_SEXO	=	COD_GRAU_INSTRUCAO						,
COD_GRAU_INSTRUCAO	=	DESCRICAO_GRAU_INSTRUCAO			,
DESCRICAO_GRAU_INSTRUCAO	=	CODIGO_ESTADO_CIVIL			,
CODIGO_ESTADO_CIVIL	=	DESCRICAO_ESTADO_CIVIL				,
DESCRICAO_ESTADO_CIVIL	=	CODIGO_COR_RACA					,
CODIGO_COR_RACA	=	DESCRICAO_COR_RACA						,
DESCRICAO_COR_RACA	=	CODIGO_NACIONALIDADE				,
CODIGO_NACIONALIDADE	=	DESCRICAO_NACIONALIDADE			,
DESCRICAO_NACIONALIDADE	=	SIGLA_UF_NASCIMENTO				,
SIGLA_UF_NASCIMENTO	=	CODIGO_MUNICIPIO_NASCIMENTO			,
CODIGO_MUNICIPIO_NASCIMENTO	=	NOME_MUNICIPIO_NASCIMENTO	,
NOME_MUNICIPIO_NASCIMENTO	=	DESPESA_MAX_CAMPANHA		,
DESPESA_MAX_CAMPANHA	=	COD_SIT_TOT_TURNO				,
COD_SIT_TOT_TURNO	=	DESC_SIT_TOT_TURNO					,
DESC_SIT_TOT_TURNO	=	NM_EMAIL
 WHERE ID IN (
 --COD_SITUACAO_CANDIDATURA
 --DES_SITUACAO_CANDIDATURA

 --NUMERO_PARTIDO


 --Feminino, 1887280

 --MASCULINO, 
--1888858
--1960699
--1960712
--1960718
--1960720
--1960729
--1960736
--2030909
--2020652
--1950187
--1950197
--1807154
--1904483

 
 1714370,
1967285	,
1967292	,
1967302	,
1967304	,
-1		,
-1		,
1647580	,
1671781	,
1674865	,
1935554	,
1938333	,
1938335	,
1684181	,
1701402	,
1906764	,
1947989	,
1947993	,
1714375	,
1807126	,
1807134	,
1807143	,
1807162	,
1960432	,
1948036	,
1948748	,
1998153	,
1838765	,
2019210	,
1950196	,
1960734	,
2004627	,
2017992	,
2044524	,
-1		,
-1		,
1674675	,
1714360	,
1714372	,
1714374	,
1714384	,
1807142	,
1837978	,
1883952	,
1960450	,
1960491	,
1960495	,
1960711	,
1906845	,
1947655	,
1948768	,
1967289	,
1967299	,
1967305	,
1684193	,
1762225	,
1919847	,
1919950	,
1998146	,
2017224	,
1960721	,
1960723	,
1960728	,
2005172	,
2005237	,
2026881	,
2027010	,
-1		,
-1		,
1695420	,
1695473	,
1906795	,
1960443	,
2019125	,
2020666	,
1931817	,
1938346	,
2004677	,
-1		,
-1		,
1714368	,
1714380	,
1714383	,
1714389	,
1714391	,
1883940	,
1674673	,
1675105	,
1838773	,
1859324	,
1960430	,
1960707	,
1701397	,
1807148	,
1807155	,
1887265	,
1889551	,
1948745	,
1960713	,
1960731	,
2019111	,
2020649	,
2020656	,
2020668	,
2023738	,
2023778	,
2020676	,
1647310	,
1684189	,
1938319	,
1950184	,
1998121	,
1998138	,
2018146	,
-1		,
1671811	,
1675267	,
1965235	,
1965239	,
2018074	,
2027086	,
1960388	,
1960724	,
-1		,
1762177	,
1778944	,
1807141	,
1807147	,
1960407	,
1960425	,
1960474	,
1960476	,
1960702	,
1960732	,
1962593	,
1962640	,
1674710	,
1675230	,
1872670	,
1984143	,
1998106	,
1998115	,
1889512	,
1889556	,
1889594	,
1890056	,
1925382	,
2019128	,
1838764	,
1957452	,
2017987	,
2018003	,
2018005	,
2018006	,
1998124	,
1998127	,
1998134	,
1998142	,
1998144	,
2027240	,
2019209	,
2018033	,
2044469	)

*/




 -- SELECT CPF_CANDIDATO, COUNT(descricao_Sexo) QtdDiferentes, MIN(DATA_NASCIMENTO) DtNascimento, 
	--max(descricao_Sexo) Instrucao1, min(descricao_Sexo) Instrucao2 FROM ImportacaoCandidato
 --GROUP BY  CPF_CANDIDATO
 --HAVING COUNT(descricao_Sexo) > 1 AND max(descricao_Sexo) <> min(descricao_Sexo)