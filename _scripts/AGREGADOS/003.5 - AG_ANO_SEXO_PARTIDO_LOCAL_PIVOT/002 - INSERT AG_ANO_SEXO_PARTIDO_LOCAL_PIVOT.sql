DELETE FROM [AG_ANO_SEXO_PARTIDO_LOCAL_PIVOT];

INSERT INTO [dbo].[AG_ANO_SEXO_PARTIDO_LOCAL_PIVOT]
           ([Ano] ,
			[PartidoColigacaoID] ,
			[LocalidadeID]  ,
			QtdMasculino  ,
			QtdFeminino ,
			QtdTotal )
SELECT TFDadoEleitoral.Ano, 
	   TDLocalidade.ID,
	   TDPartidoColigacao.ID,
	   SUM(CASE 
			WHEN TDCandidato.Sexo = 'MASCULINO' THEN
					1
			ELSE
				0
		END) as QtdMasculino,
		SUM(CASE 
			WHEN TDCandidato.Sexo = 'FEMININO' THEN
					1
			ELSE
				0
		END)
	    as QtdFeminino,
	   COUNT(1) AS QtdTotal
	    FROM TFDadoEleitoral
		INNER JOIN TPCandidatoEscolaridade ON TFDadoEleitoral.CandidatoEscolaridadeID = TPCandidatoEscolaridade.ID
		INNER JOIN TDCandidato ON TPCandidatoEscolaridade.CandidatoID = TDCandidato.ID
		INNER JOIN TDEscolaridade ON TPCandidatoEscolaridade.EscolaridadeID = TDEscolaridade.ID
		INNER JOIN TDCargoPolitico ON TFDadoEleitoral.CargoPoliticoID = TDCargoPolitico.ID
		INNER JOIN TDLocalidade ON TFDadoEleitoral.LocalidadeID = TDLocalidade.ID
		INNER JOIN TDOcupacao ON TFDadoEleitoral.OcupacaoID = TDOcupacao.ID
		INNER JOIN TDPartidoColigacao ON TFDadoEleitoral.PartidoColigacaoID = TDPartidoColigacao.ID
		--TDPartidoColigacao.Partido = 'PT' AND TDCargoPolitico.Descricao = 'VEREADOR' AND Municipio = 'RIO DE JANEIRO'

		GROUP BY 
		   TFDadoEleitoral.Ano, 
		   TDLocalidade.ID, 		   
		   TDPartidoColigacao.ID