using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Domínio
{
    class Tarefa : EntidadeBase
    {
        private string prioridade = "", titulo = "", percentualConcluido = "";
        private DateTime dataCriacao;  
        private Nullable<DateTime> dataConclusao;
        public Tarefa(string prioridade, string titulo, string percentualConcluido, DateTime dataCriacao, Nullable<DateTime> dataConclusao, int id)
        {
            this.prioridade = prioridade;
            this.titulo = titulo;
            this.percentualConcluido = percentualConcluido;
            this.dataCriacao = dataCriacao;
            this.dataConclusao = dataConclusao;
            this.Id = id;
        }
        public Tarefa(string prioridade, string titulo, string percentualConcluido)
        {
            this.prioridade = prioridade;
            this.titulo = titulo;
            this.percentualConcluido = percentualConcluido;
            this.dataCriacao = DateTime.Now;
        }
        public Tarefa()
        {
        }
        public string Validar()
        {
            string validador = "";

            if (prioridade != "ALTA" && prioridade != "NORMAL" && prioridade != "BAIXA")
                validador += "\nPRIORIDADE INVÁLIDA!\n";
            else if (string.IsNullOrEmpty(titulo))
                validador += "TÍTULO INVÁLIDO\n";
            else if (string.IsNullOrEmpty(percentualConcluido))
                validador += "PERCENTUAL DE CONCLUSÃO INVÁLIDO!\n";

            return validador;
        }
        public override string ToString()
        {
            if(dataConclusao.HasValue) 
                return "------------------------------------------------------\n"
                 + "ID : " + Id + "\nTÍTULO : " + titulo + "\nPRIORIDADE : "
                 + prioridade + "\nPERCENTUAL CONCLUÍDO : " + percentualConcluido + "\nDATA DE CRIAÇÃO : " +
                 dataCriacao.ToString("U") + "\nDATA DE CONCLUSÃO : " + dataConclusao.Value.ToString("U");
            else
                return "------------------------------------------------------\n"
                 + "ID : " + Id + "\nTÍTULO : " + titulo + "\nPRIORIDADE : "
                 + prioridade + "\nPERCENTUAL CONCLUÍDO : " + percentualConcluido + "\nDATA DE CRIAÇÃO : " +
                 dataCriacao.ToString("U") + "\nDATA DE CONCLUSÃO : NÃO CONCLUÍDO\n";
        }
        public string Prioridade { get => prioridade; set => prioridade = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Percentualconcluido { get => percentualConcluido; set => percentualConcluido = value; }
        public DateTime DataCriacao { get => dataCriacao; set => dataCriacao = value; }
        public Nullable<DateTime> DataConclusao { get => dataConclusao; set => dataConclusao = value; }
    }
}
