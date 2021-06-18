using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleDeTarefas.ConsoleApp.Domínio;

namespace ControleDeTarefas.ConsoleApp.Controladores
{
    class ControladorBase
    {
        List<Tarefa> tarefas = new List<Tarefa>();
        internal List<Tarefa> Tarefas { get => tarefas; set => tarefas = value; }

        public ControladorBase()
        {
            tarefas = ObterTodasAsTarefas();
        }

        private readonly string enderecoConexaoBD =
            @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=TarefasDB;Integrated Security=True;Pooling=False";
        public void Inserir(Tarefa tarefa)
        {
            SqlConnection conexaoComBD = new SqlConnection(enderecoConexaoBD);
            conexaoComBD.Open();

            string sqlInserir =
                @"INSERT INTO TBTAREFAS
                (
                     [PRIORIDADE],
                     [TITULO],
                     [DATACRIACAO],
                     [PERCENTUALCONCLUIDO]
                )
                 VALUES
                (
                     @PRIORIDADE,
                     @TITULO,
                     @DATACRIACAO,
                     @PERCENTUALCONCLUIDO
                )";

            sqlInserir +=
                @"SELECT SCOPE_IDENTITY();";

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBD);

            comandoInsercao.Parameters.AddWithValue("PRIORIDADE", tarefa.Prioridade);
            comandoInsercao.Parameters.AddWithValue("TITULO", tarefa.Titulo);
            comandoInsercao.Parameters.AddWithValue("DATACRIACAO", DateTime.Now);
            comandoInsercao.Parameters.AddWithValue("PERCENTUALCONCLUIDO", tarefa.Percentualconcluido);

            if (tarefa.Percentualconcluido.Equals("100%"))
            {
                comandoInsercao.Parameters.AddWithValue("DATACONCLUSAO", DateTime.Now);
                tarefa.DataConclusao = DateTime.Now;
            }

            tarefa.Id = Convert.ToInt32(comandoInsercao.ExecuteScalar());
            conexaoComBD.Close();
        }
        public void Excluir(Tarefa tarefa)
        {
            List<Tarefa> tarefas = ObterTodasAsTarefas();

            SqlConnection conexaoComBD = new SqlConnection(enderecoConexaoBD);
            conexaoComBD.Open();

            string sqlExcluir =
                @"DELETE FROM TBTAREFAS
                    WHERE
                     [ID] = @ID";

            SqlCommand comandoInsercao = new SqlCommand(sqlExcluir, conexaoComBD);

            comandoInsercao.Parameters.AddWithValue("ID", tarefa.Id);

            comandoInsercao.ExecuteNonQuery();
            conexaoComBD.Close();
        }
        public void Editar(Tarefa tarefa)
        {
            SqlConnection conexaoComBD = new SqlConnection(enderecoConexaoBD);
            conexaoComBD.Open();

            string sqlEditar =
                @"UPDATE TBTAREFAS
                    SET
                        [PRIORIDADE] = @PRIORIDADE,
                        [TITULO] = @TITULO,
                        [PERCENTUALCONCLUIDO] = @PERCENTUALCONCLUIDO,
                        [DATACONCLUSAO] = @DATACONCLUSAO
                    WHERE
                        [ID] = @ID";

            SqlCommand comandoEditar = new SqlCommand(sqlEditar, conexaoComBD);

            comandoEditar.Parameters.AddWithValue("ID", tarefa.Id);
            comandoEditar.Parameters.AddWithValue("PRIORIDADE", tarefa.Prioridade);
            comandoEditar.Parameters.AddWithValue("TITULO", tarefa.Titulo);
            comandoEditar.Parameters.AddWithValue("PERCENTUALCONCLUIDO", tarefa.Percentualconcluido);

            if (tarefa.Percentualconcluido.Equals("100%"))
            {
                comandoEditar.Parameters.AddWithValue("DATACONCLUSAO", DateTime.Now);
                tarefa.DataConclusao = DateTime.Now;
            }

            comandoEditar.ExecuteNonQuery();
            conexaoComBD.Close();
            return;
        }
        public List<Tarefa> ObterTarefasAbertas()
        {
            List<Tarefa> tarefas = ObterTodasAsTarefas();
            tarefas = tarefas.FindAll(x => x.Percentualconcluido != "100%");

            return OrganizarPorPrioridade(tarefas);
        }
        public List<Tarefa> ObterTarefasFechadas()
        {
            List<Tarefa> tarefas = ObterTodasAsTarefas();
            tarefas = tarefas.FindAll(x => x.Percentualconcluido == "100%");

            return OrganizarPorPrioridade(tarefas);
        }
        public List<Tarefa> ObterTodasAsTarefas()
        {
            SqlConnection conexaoComBD = new SqlConnection(enderecoConexaoBD);
            conexaoComBD.Open();

            string sqlSelecionar =
                @"SELECT
                     [ID],
                     [PRIORIDADE],
                     [TITULO],
                     [DATACRIACAO],
                     [PERCENTUALCONCLUIDO],
                     [DATACONCLUSAO]
                  FROM
                     TBTAREFAS";

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionar, conexaoComBD);

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> listaTarefas = new List<Tarefa>();

            while (leitorTarefas.Read())
            {
                Nullable<DateTime> dataConclusao;
                int id = Convert.ToInt32(leitorTarefas["ID"]);
                string prioridade = Convert.ToString(leitorTarefas["PRIORIDADE"]);
                string titulo = Convert.ToString(leitorTarefas["TITULO"]);
                DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
                string percentual = Convert.ToString(leitorTarefas["PERCENTUALCONCLUIDO"]);

                if (leitorTarefas["DATACONCLUSAO"].Equals(DBNull.Value))
                    dataConclusao = null;
                else
                    dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);

                listaTarefas.Add(new Tarefa(prioridade, titulo, percentual, dataCriacao, dataConclusao, id));
            }

            return OrganizarPorPrioridade(listaTarefas);
        }
        public Tarefa ObterTarefaPorId(int id)
        {
            List<Tarefa> tarefas = ObterTodasAsTarefas();
            Tarefa tarefa = new Tarefa();

            if (tarefas.Exists(x => x.Id == id))
                return tarefa = tarefas.Find(x => x.Id == id);
            else
                return tarefa;
        }
        protected static List<Tarefa> OrganizarPorPrioridade(List<Tarefa> tarefas)
        {
            tarefas = tarefas.OrderBy(x => x.Prioridade == "BAIXA").ThenBy(x => x.Prioridade == "NORMAL").ThenBy(x => x.Prioridade == "ALTA").ToList();
            return tarefas;
        }
    }
}
