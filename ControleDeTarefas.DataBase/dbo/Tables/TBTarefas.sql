CREATE TABLE [dbo].[TBTarefas] (
    [Id]                  INT          IDENTITY (1, 1) NOT NULL,
    [Prioridade]          VARCHAR (10) NULL,
    [Titulo]              VARCHAR (50) NULL,
    [DataCriacao]         DATETIME     NULL,
    [PercentualConcluido] VARCHAR (50) NULL,
    [DataConclusao]       DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

