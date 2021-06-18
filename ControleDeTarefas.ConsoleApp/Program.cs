using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ControleDeTarefas.ConsoleApp.Telas;
using ControleDeTarefas.ConsoleApp.Controladores;

namespace ControleDeTarefas.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Tela tela = new Tela();
            tela.Menu();
        }
    }
}

