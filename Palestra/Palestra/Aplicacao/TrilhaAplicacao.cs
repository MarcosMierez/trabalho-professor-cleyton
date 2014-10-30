using Palestra.Models;
using Palestra.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Palestra.Aplicacao
{
    public class TrilhaAplicacao
    {
        private readonly Contexto contexto;

        public TrilhaAplicacao()
        {
            contexto = new Contexto();
        }
        public List<Trilha> Listar()
        {
            var trilhas = new List<Trilha>();
            const string strQuery = "SELECT ID , Nome FROM Trilha order by Nome";
            var linhas = contexto.ExecutaComandoComRetorno(strQuery, null);
            foreach (var linha in linhas)
            {
                var tempTrilha = new Trilha
                {
                    ID = linha["ID"],
                    Nome = linha["Nome"]
                };
                trilhas.Add(tempTrilha);
            }
            return trilhas;
        }
        public int Inserir(Trilha trilha)
        {
            const string strQuery = "insert into Trilha (Id,Nome) values (@Id,@Nome)";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",trilha.ID},
                {"Nome",trilha.Nome}
            };
            return contexto.ExecutaComando(strQuery, parametros);
        }
        public int Alterar(Trilha trilha)
        {
            const string strQuery = "UPDATE  Trilha set Nome = @Nome Where Id = @Id";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",trilha.ID},
                {"Nome",trilha.Nome}
            };
            return contexto.ExecutaComando(strQuery, parametros);
        }
        public int Excluir(string id)
        {
            const string strQuery = "delete from Trilha Where ID = @ID";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",id}
            };
            return contexto.ExecutaComando(strQuery, parametros);
        }
        public Trilha ListarPorId(string id)
        {
            const string strQuery = "SELECT Id , Nome FROM Trilha where Id = @ID";
            var parametros = new Dictionary<string, object>()
            {
                {"ID",id}
            };
            var linhas = contexto.ExecutaComandoComRetorno(strQuery, parametros);
            var trilha = new Trilha
            {
                ID = linhas[0]["Id"],
                Nome = linhas[0]["Nome"]
            };
            return trilha;
        }
    }
}