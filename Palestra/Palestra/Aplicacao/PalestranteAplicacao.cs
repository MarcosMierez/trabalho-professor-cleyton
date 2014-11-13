using Palestra.Models;
using Palestra.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Palestra.Aplicacao
{
    public class PalestranteAplicacao
    {
        private readonly Contexto contexto;

        public PalestranteAplicacao()
        {
            contexto = new Contexto();
        }
        public List<Palestrante> Listar()
        {
            var palestrantes = new List<Palestrante>();
            const string strQuery = "SELECT ID , Nome,Bio,Twitter,Foto FROM Palestrante order by Nome";
            var linhas = contexto.ExecutaComandoComRetorno(strQuery, null);
            foreach (var linha in linhas)
            {
                var tempPalestrante = new Palestrante
                {
                    ID = linha["ID"],
                    Nome = linha["Nome"],
                    Bio=linha["Bio"],
                    Twitter=linha["Twitter"],
                    Foto=linha["Foto"]
                };
                palestrantes.Add(tempPalestrante);
            }
            return palestrantes;
        }
        public int Inserir(Palestrante palestrante)
        {
            const string strQuery = "insert into Palestrante (Id,Nome,Bio,Twitter,Foto) values (@Id,@Nome,@Bio,@Twitter,@Foto)";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",palestrante.ID},
                {"Nome",palestrante.Nome},
                {"Bio",palestrante.Bio},
                {"Twitter",palestrante.Twitter},
                {"Foto",palestrante.Foto}

            };
            return contexto.ExecutaComando(strQuery, parametros);
        }
        public int Alterar(Palestrante palestrante)
        {
            const string strQuery = "UPDATE  Palestrante set Nome = @Nome, Bio = @Bio ,Twitter = @Twitter ,Foto = @Foto  Where Id = @Id";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",palestrante.ID},
                {"Nome",palestrante.Nome},
                {"Bio",palestrante.Bio},
                {"Twitter",palestrante.Twitter},
                {"Foto",palestrante.Foto}
            };
            return contexto.ExecutaComando(strQuery, parametros);
        }
        public int Excluir(string id)
        {
            const string strQuery = "delete from Palestrante Where ID = @ID";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",id}
            };
            return contexto.ExecutaComando(strQuery, parametros);
        }
        public Palestrante ListarPorId(string id)
        {
            const string strQuery = "SELECT Id , Nome ,Bio , Twitter, Foto FROM Palestrante where Id = @ID";
            var parametros = new Dictionary<string, object>()
            {
                {"ID",id}
            };
            var linhas = contexto.ExecutaComandoComRetorno(strQuery, parametros);
            if (!linhas.Any())
                return new Palestrante();
            var palestrante = new Palestrante
            {
                ID = linhas[0]["Id"],
                Nome = linhas[0]["Nome"],
                Bio = linhas[0]["Bio"],
                Twitter = linhas[0]["Twitter"],
                Foto = linhas[0]["Foto"]
            };
            return palestrante;
        }
    }
}