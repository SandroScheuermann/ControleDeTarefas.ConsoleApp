using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleDeTarefas.ConsoleApp.Controladores;
using ControleDeTarefas.ConsoleApp.Domínio;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    class Tela
    {
        Controlador controlador = new Controlador();
        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("1 - Cadastrar Tarefas");
                Console.WriteLine("2 - Editar Tarefas");
                Console.WriteLine("3 - Excluir Tarefas");
                Console.WriteLine("4 - Visualizar Tarefas Abertas");
                Console.WriteLine("5 - Visualizar Tarefas Concluídas");
                Console.WriteLine("6 - Visualizar Todas as Tarefas");
                Console.WriteLine("S - Para sair");
                Console.WriteLine("------------------------------------------------------");
                Console.Write("Selecione a opção : ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        InserirTarefa(); continue;
                    case "2":
                        EditarTarefa(); continue;
                    case "3":
                        ExcluirTarefa(); continue;
                    case "4":
                        VisualizarTarefasAbertas(); continue;
                    case "5":
                        VisualizarTarefasFechadas(); continue;
                    case "6":
                        VisualizarTodasAsTarefas(); continue;
                    default:
                        Console.Clear(); break;
                }

                if (opcao.Equals("S", StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }
        public void VisualizarTarefasAbertas()
        {
            List<Tarefa> tarefas = controlador.ObterTarefasAbertas();

            VerificaSeHaTarefas(tarefas);

            foreach (Tarefa tarefa in tarefas)
                Console.WriteLine(tarefa.ToString());
            Console.WriteLine();
        }
        public void VisualizarTarefasFechadas()
        {
            List<Tarefa> tarefas = controlador.ObterTarefasFechadas();
            VerificaSeHaTarefas(tarefas);

            foreach (Tarefa tarefa in tarefas)
                Console.WriteLine(tarefa.ToString());
            Console.WriteLine();
        }
        public void VisualizarTodasAsTarefas()
        {
            List<Tarefa> tarefas = controlador.ObterTodasAsTarefas();
            VerificaSeHaTarefas(tarefas);

            foreach (Tarefa tarefa in tarefas)
                Console.WriteLine(tarefa.ToString());
            Console.WriteLine();
        }
        public void InserirTarefa()
        {
            controlador = new Controlador();

            Console.WriteLine("Digite o título da tarefa : ");
            string titulo = Console.ReadLine();
            Console.WriteLine("Digite a prioridade da tarefa (ALTA/NORMAL/BAIXA) : ");
            string prioriade = Console.ReadLine();
            Console.WriteLine("Digite o percentual concluído da tarefa : ");
            string percentual = Console.ReadLine();

            Tarefa tarefa = new Tarefa(prioriade, titulo, percentual);

            if (string.IsNullOrEmpty(tarefa.Validar()))
            {
                controlador.Inserir(tarefa);
                Console.Clear();
                Console.WriteLine("TAREFA CADASTRADA COM SUCESSO\n");
            }
            else
                Console.WriteLine(tarefa.Validar());

        }
        public void EditarTarefa()
        {
            controlador = new Controlador();

            Console.WriteLine("Digite o ID da tarefa que deseja editar : ");

            VisualizarTodasAsTarefas();

            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            if (controlador.Tarefas.Exists(x => x.Id == idSelecionado))
            {
                Console.WriteLine("Digite o título da tarefa : ");
                string titulo = Console.ReadLine();
                Console.WriteLine("Digite a prioridade da tarefa (ALTA/NORMAL/BAIXA) : ");
                string prioriade = Console.ReadLine();
                Console.WriteLine("Digite o percentual concluído da tarefa : ");
                string percentual = Console.ReadLine();

                Tarefa tarefa = controlador.ObterTarefaPorId(idSelecionado);
                tarefa.Titulo = titulo;
                tarefa.Prioridade = prioriade;
                tarefa.Percentualconcluido = percentual;

                if (string.IsNullOrEmpty(tarefa.Validar()))
                {
                    controlador.Editar(tarefa);
                    Console.Clear();
                    Console.WriteLine("Tarefa Editada com Sucesso!!!");

                }
                else
                    Console.WriteLine(tarefa.Validar());
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Não há tarefas cadastradas com o ID selecionado!!!\n");
            }
        }
        public void ExcluirTarefa()
        {
            controlador = new Controlador();

            VisualizarTodasAsTarefas();

            Console.WriteLine("------------------------------------------------------");
            Console.Write("Digite o ID da tarefa que deseja excluir : ");

            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            if (controlador.Tarefas.Exists(x => x.Id == idSelecionado))
            {
                controlador.Excluir(controlador.Tarefas.Find(x => x.Id == idSelecionado));
                Console.Clear();
                Console.WriteLine("Tarefa excluída com sucesso!!!");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Não há tarefas cadastradas com o ID selecionado!!!\n");
            }
        }
        private static void VerificaSeHaTarefas(List<Tarefa> tarefas)
        {
            if (tarefas.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Não há tarefas cadastradas!!!");
            }
        }
    }
}
