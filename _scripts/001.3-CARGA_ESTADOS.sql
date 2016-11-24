DELETE FROM [Estado];
DELETE FROM [Pais];
GO
DBCC CHECKIDENT ('[Pais]', RESEED, 0);
GO

GO
DBCC CHECKIDENT ('[Estado]', RESEED, 0);
GO

INSERT INTO dbo.Pais VALUES ('Brasil')
GO



DECLARE @pais INT
 SELECT @pais = MAX(ID) 
   FROM dbo.Pais

INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Rond�nia', 'RO', 'Norte', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Acre', 'AC', 'Norte', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Amazonas', 'AM', 'Norte', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Roraima', 'RR', 'Norte', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Par�', 'PA', 'Norte', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Amap�', 'AP', 'Norte', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Tocantins', 'TO', 'Norte', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Maranh�o', 'MA', 'Nordeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Piau�', 'PI', 'Nordeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Cear�', 'CE', 'Nordeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Rio Grande do Norte', 'RN', 'Nordeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Para�ba', 'PB', 'Nordeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Pernambuco', 'PE', 'Nordeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Alagoas', 'AL', 'Nordeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Sergipe', 'SE', 'Nordeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Bahia', 'BA', 'Nordeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Minas Gerais', 'MG', 'Sudeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Esp�rito Santo', 'ES', 'Sudeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Rio de Janeiro', 'RJ', 'Sudeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'S�o Paulo', 'SP', 'Sudeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Paran�', 'PR', 'Sul', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Santa Catarina', 'SC', 'Sul', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Rio Grande do Sul', 'RS', 'Sul', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Mato Grosso do Sul', 'MS', 'Centro-Oeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Mato Grosso', 'MT', 'Centro-Oeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Goi�s', 'GO', 'Centro-Oeste', @pais)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Distrito Federal', 'DF', 'Centro-Oeste', @pais)
GO