using System.Data;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Threading;


namespace Palestra.Repositorio
{
    public class Contexto : IDisposable
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
            var tentativas = 3;
            if (conexao.State == ConnectionState.Open)
                return;
            while (tentativas >= 0 && conexao.State != ConnectionState.Open)
            {
                conexao.Open();
                tentativas--;
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
            if (conexao == null) return;
            conexao.Dispose();
            conexao = null;
        }

        public int ExecutaComando(string comandoSQL, Dictionary<string, object> parametros)
        {
            var resultado = 0;
            if (string.IsNullOrEmpty(comandoSQL))
                throw  new ArgumentException("O parametro nao pode ser nulo ou vazio");
            try
            {
                AbrirConexao();
                var cmdComando = CriarComando(comandoSQL, parametros);
                resultado = cmdComando.ExecuteNonQuery();
            }
            finally
            {
                FecharConexao();
            }
            return resultado;
        }
        public object ExecutaComandoComSimplesRetorno(string comandoSQL, Dictionary<string, object> parametros)
        {
            object resultado;
            if (string.IsNullOrEmpty(comandoSQL))
                throw new ArgumentException("O parametro nao pode ser nulo ou vazio");
            try
            {
                AbrirConexao();
                var cmdComando = CriarComando(comandoSQL, parametros);
                resultado = cmdComando.ExecuteScalar();
            }
            finally
            {
                FecharConexao();
            }
            return resultado;
        }

        public List<Dictionary<string, string>> ExecutaComandoComRetorno(string comandoSQL,Dictionary<string, object> parametros)
        {
            List<Dictionary<string,string>> linhas = null;
            if (string.IsNullOrEmpty(comandoSQL))
                throw new ArgumentException("O parametro nao pode ser nulo ou vazio");
            try
            {
                AbrirConexao();
                var cmdComando = CriarComando(comandoSQL, parametros);
                using (var reader =cmdComando.ExecuteReader())
                {
                    linhas = new List<Dictionary<string, string>>();
                    while (reader.Read())
                    {
                        var linha = new Dictionary<string, string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var nomeDaColuna = reader.GetName(i);
                            var valorDaColuna = reader.IsDBNull(i) ? null : reader.GetString(i);
                            linha.Add(nomeDaColuna,valorDaColuna);
                        }
                        linhas.Add(linha);
                    }
                }
            }
            finally
            {
                FecharConexao();
            }
            return linhas;
        } 
        private MySqlCommand CriarComando(string comandoSQL, Dictionary<string, object> parametros)
        {
            var cmdComando = conexao.CreateCommand();
            cmdComando.CommandText = comandoSQL;
            AdicionarParametros(cmdComando, parametros);
            return cmdComando;
        }

        private  static void AdicionarParametros(MySqlCommand cmdComando, Dictionary<string, object> parametros)
        {
            if (parametros == null)
               return;
            foreach (var item in parametros)
            {
                var parametro = cmdComando.CreateParameter();
                parametro.ParameterName = item.Key;
                parametro.Value = item.Value ?? DBNull.Value;
                cmdComando.Parameters.Add(parametro);
            }
        }
    }
}