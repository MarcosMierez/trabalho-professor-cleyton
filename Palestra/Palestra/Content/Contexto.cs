using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using MySql.Data.MySqlClient;

namespace Palestra.Repositorio
{
    public class Contexto :IDisposable
    {
        private MySqlConnection conexao;

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["TDCbd"].ConnectionString;
        }
        public Contexto()
        {
            conexao = new MySqlConnection(GetConnectionString());
        }
        private void AbrirConexao()
        {
            var tentarivas = 3;
            if (conexao.State == ConnectionState.Open)
                return;
            while (tentarivas >=0 && conexao.State != ConnectionState.Open)
            {
                conexao.Open();
                tentarivas--;
                Thread.Sleep(30);
            }
        }
        private void FecharConexao()
        {
            if (conexao.State == ConnectionState.Open)
                conexao.Close();
        }
        public void Dispose()
        {
            if (conexao==null) return;
            conexao.Dispose();
            conexao = null;
        }


    }
}