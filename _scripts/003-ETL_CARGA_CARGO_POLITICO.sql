DELETE FROM TDCargoPolitico;
GO
DBCC CHECKIDENT ('[TDCargoPolitico]', RESEED, 0);
GO
INSERT INTO TDCargoPolitico
SELECT DISTINCT 
 DESCRICAO_CARGO, 
 case DESCRICAO_CARGO 
	WHEN 'Vereador' THEN 'SIM'
	WHEN 'Prefeito' THEN 'SIM'
	WHEN 'Vice-prefeito' THEN 'SIM'
	ELSE
		'N�O'
	END as Municipais,
	case DESCRICAO_CARGO 
		WHEN 'Vereador' THEN 'Municipais'
		WHEN 'Prefeito' THEN 'Municipais'
		WHEN 'Vice-prefeito' THEN 'Municipais'

		WHEN 'Governador' THEN 'Estaduais'
		WHEN 'Deputado Distrital' THEN 'Estaduais'
		WHEN 'Deputado Estadual' THEN 'Estaduais'

		WHEN 'Senador' THEN 'Federais'
		WHEN '1� Suplente' THEN 'Federais'
		WHEN '2� Suplente' THEN 'Federais'
		WHEN 'Deputado Federal' THEN 'Federais'
		WHEN 'Presidente' THEN 'Federais'
	END as Tipo
FROM ImportacaoVaga
where DESCRICAO_CARGO in ( 'Vereador'
		,'Prefeito'
		,'Vice-prefeito' 
		,'Governador'
		,'Deputado Distrital'
		,'Deputado Estadual' 
		,'Senador' 
		,'1� Suplente' 
		,'2� Suplente' 
		,'Deputado Federal'
		,'Presidente')