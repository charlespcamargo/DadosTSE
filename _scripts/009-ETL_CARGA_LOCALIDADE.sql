DELETE FROM TDLocalidade;
GO

DBCC CHECKIDENT ('[TDLocalidade]', RESEED, 0);
GO

INSERT INTO TDLocalidade
SELECT DISTINCT NOME_UE, SIGLA_UF, ISNULL(ESTADO.REGIAO, 'BRASIL')
 FROM ImportacaoVaga
 LEFT JOIN Estado 
   ON ESTADO.Sigla = ImportacaoVaga.SIGLA_UF
ORDER BY NOME_UE

GO
SET IDENTITY_INSERT dbo.Cidade ON;  
GO
DELETE FROM Cidade;
GO
INSERT INTO Cidade (ID,
Nome,
EstadoID)
SELECT TDLocalidade.ID, TDLocalidade.Municipio, Estado.ID	
FROM TDLocalidade
INNER JOIN Estado ON TDLocalidade.SiglaEstado = Estado.Sigla
GO 
SET IDENTITY_INSERT dbo.Cidade OFF;  
GO