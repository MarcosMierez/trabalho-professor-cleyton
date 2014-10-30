using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Palestra.Models;
using Palestra.Repositorio;

namespace Palestra.Aplicacao
{
    public class SalaAplicacao
    {
        private readonly Contexto contexto;

        public SalaAplicacao()
        {
            contexto = new Contexto();
        }
        public List<Sala> Listar()
        {
            var salas = new List<Sala>();
            const string strQuery = "SELECT ID , Nome, Numero FROM Sala";
            var linhas = contexto.ExecutaComandoComRetorno(strQuery, null);
            foreach (var linha in linhas)
            {
                var tempSala = new Sala
                {
                    ID = linha["ID"],
                    Nome = linha["Nome"],
                    Numero =linha["Numero"]
                };
                salas.Add(tempSala);
            }
            return salas;
        }
        public int Inserir(Sala sala)
        {
            const string strQuery = "insert into Sala (Id,Nome,Numero) values (@Id,@Nome,@Numero)";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",sala.ID},
                {"Nome",sala.Nome},
                {"Numero",sala.Numero}
            };
            return contexto.ExecutaComando(strQuery, parametros);
        }
        public int Alterar(Sala sala)
        {
            const string strQuery = "UPDATE  Sala set Nome = @Nome , Numero = @Numero Where Id = @Id";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",sala.ID},
                {"Nome",sala.Nome},
                {"Numero",sala.Numero}
            };
            return contexto.ExecutaComando(strQuery, parametros);
        }
        public int Excluir(string id)
        {
            const string strQuery = "delete from Sala Where ID = @ID";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",id}
            };
            return contexto.ExecutaComando(strQuery, parametros);
        }
        public Sala ListarPorId(string id)
        {
            const string strQuery = "SELECT Id , Nome ,Numero FROM Sala where Id = @ID";
            var parametros = new Dictionary<string, object>()
            {
                {"ID",id}
            };
            var linhas = contexto.ExecutaComandoComRetorno(strQuery, parametros);
            var sala = new Sala
            {
                ID = linhas[0]["Id"],
                Nome = linhas[0]["Nome"],
                Numero = linhas[0]["Numero"]
            };
            return sala;
        }


}
}