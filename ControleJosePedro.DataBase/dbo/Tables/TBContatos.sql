CREATE TABLE [dbo].[TBContatos]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [nome] VARCHAR(50) NULL, 
    [email] VARCHAR(50) NULL, 
    [telefone] VARCHAR(50) NULL, 
    [empresa] VARCHAR(50) NULL
)
