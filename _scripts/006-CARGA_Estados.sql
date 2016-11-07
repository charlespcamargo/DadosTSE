DELETE FROM [Estado];
DELETE FROM [Pais];
GO
DBCC CHECKIDENT ('[Pais]', RESEED, 0);
GO


GO
DBCC CHECKIDENT ('[Estado]', RESEED, 0);
GO

INSERT INTO dbo.Pais VALUES ('Brasil')

INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Rondônia', 'RO', 'Norte', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Acre', 'AC', 'Norte', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Amazonas', 'AM', 'Norte', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Roraima', 'RR', 'Norte', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Pará', 'PA', 'Norte', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Amapá', 'AP', 'Norte', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Tocantins', 'TO', 'Norte', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Maranhão', 'MA', 'Nordeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ('Piauí', 'PI', 'Nordeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Ceará', 'CE', 'Nordeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Rio Grande do Norte', 'RN', 'Nordeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Paraíba', 'PB', 'Nordeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Pernambuco', 'PE', 'Nordeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Alagoas', 'AL', 'Nordeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Sergipe', 'SE', 'Nordeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Bahia', 'BA', 'Nordeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Minas Gerais', 'MG', 'Sudeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Espírito Santo', 'ES', 'Sudeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Rio de Janeiro', 'RJ', 'Sudeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'São Paulo', 'SP', 'Sudeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Paraná', 'PR', 'Sul', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Santa Catarina', 'SC', 'Sul', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Rio Grande do Sul', 'RS', 'Sul', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Mato Grosso do Sul', 'MS', 'Centro-Oeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Mato Grosso', 'MT', 'Centro-Oeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Goiás', 'GO', 'Centro-Oeste', 1)
INSERT INTO [dbo].[Estado] ([Nome], [Sigla], [Regiao], [PaisID]) VALUES ( 'Distrito Federal', 'DF', 'Centro-Oeste', 1)