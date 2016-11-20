ALTER TABLE TDESCOLARIDADE 
ADD NIVEL INT NULL
GO
UPDATE [dbo].[TDEscolaridade]
   SET [Nivel] = 0
 WHERE [Descricao] = 'N�O INFORMADO'
 GO
UPDATE [dbo].[TDEscolaridade]
   SET [Nivel] = 1
 WHERE [Descricao] = 'ANALFABETO'
 GO
UPDATE [dbo].[TDEscolaridade]
   SET [Nivel] = 2
 WHERE [Descricao] = 'L� E ESCREVE'
 GO
UPDATE [dbo].[TDEscolaridade]
   SET [Nivel] = 3
 WHERE [Descricao] = 'ENSINO FUNDAMENTAL INCOMPLETO'
 GO
UPDATE [dbo].[TDEscolaridade]
   SET [Nivel] = 4
 WHERE [Descricao] = 'ENSINO FUNDAMENTAL COMPLETO'
 GO
UPDATE [dbo].[TDEscolaridade]
   SET [Nivel] = 5
 WHERE [Descricao] = 'ENSINO M�DIO INCOMPLETO'
 GO
UPDATE [dbo].[TDEscolaridade]
   SET [Nivel] = 6
 WHERE [Descricao] = 'ENSINO M�DIO COMPLETO'
 GO
UPDATE [dbo].[TDEscolaridade]
   SET [Nivel] = 7
 WHERE [Descricao] = 'SUPERIOR INCOMPLETO'
 GO
UPDATE [dbo].[TDEscolaridade]
   SET [Nivel] = 8
 WHERE [Descricao] = 'SUPERIOR COMPLETO'




