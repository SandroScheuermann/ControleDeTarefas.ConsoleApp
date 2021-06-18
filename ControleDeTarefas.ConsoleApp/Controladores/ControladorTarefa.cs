//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ControleDeTarefas.ConsoleApp.Domínio;

//namespace ControleDeTarefas.ConsoleApp.Controladores
//{
//    class ControladorTarefa : ControladorBase<Tarefa>
//    {
//        public List<Tarefa> ObterTarefasAbertas()
//        {
//            List<Tarefa> tarefas = ObterTodasAsTarefas();
//            tarefas = tarefas.FindAll(x => x.Percentualconcluido != "100%");

//            return OrganizarPorPrioridade(tarefas);
//        }
//        public List<Tarefa> ObterTarefasFechadas()
//        {
//            List<Tarefa> tarefas = ObterTodasAsTarefas();
//            tarefas = tarefas.FindAll(x => x.Percentualconcluido == "100%");

//            return OrganizarPorPrioridade(tarefas);
//        }
//    }
//}
