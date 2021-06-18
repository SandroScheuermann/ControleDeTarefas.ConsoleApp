insert into TBTarefas
    (
        Prioridade,
        Titulo,
        DataCriacao,
        PercentualConcluido,
        DataConclusao
    )
    values
    (
        'ALTA',
        'TAREFA1',
        '06/17/2021',
        '50%',
        '07/17/2021'
    )

update TBTarefas
    set
        [Prioridade] = '',
        [Titulo] = '',
        [DataCriacao] = '',
        [PercentualConcluido] = '',
        [DataConclusao] = ''
    where
        [Id] = 1

delete from TBTarefas
    where
        [Id] = 1

select * from TBTarefas