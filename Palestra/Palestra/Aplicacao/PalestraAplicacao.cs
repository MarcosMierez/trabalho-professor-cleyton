﻿using Palestra.Models;
using Palestra.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Palestra.Aplicacao
{
    public class PalestraAplicacao
    {
        private readonly Contexto contexto;
        #region QueryParaTRazerSalas
        const string strQueryBase =
            " SELECT p.ID,p.Nome as nomePalestra, p.Titulo , p.Codigo , p.Descricao , p.Nivel , p.Horario , p.palestranteID , p.trilhaID, p.salaID," +
            " pp.Nome as nomePalestrante,pp.Bio,pp.Twitter,pp.Foto," +
            " t.Nome as nomeTrilha," +
            " s.Nome as nomeSala,s.Numero" +
            " FROM palestras p, sala s, trilha t,palestrante pp where p.palestranteID = pp.ID and p.trilhaID = t.ID and p.salaID = s.ID";
        #endregion
        
        public PalestraAplicacao()
        {
            contexto = new Contexto();
        }
        public List<Palestras> ListarOtimizado()
        {
            var palestras = new List<Palestras>();
            
            var linhas = contexto.ExecutaComandoComRetorno(strQueryBase, null);
            foreach (var linha in linhas)
            {
                var tempPalestra = SetaPalestrasParaAAplicacao(linha);
                palestras.Add(tempPalestra);
            }
            return palestras;

        }

        private static Palestras SetaPalestrasParaAAplicacao(Dictionary<string, string> linha)
        {
            var tempPalestra = new Palestras
            {
                ID = linha["ID"],
                Nome = linha["nomePalestra"],
                Titulo = linha["Titulo"],
                Codigo = linha["Codigo"],
                Descricao = linha["Descricao"],
                Nivel = linha["Nivel"],
                Horario = DateTime.Parse(linha["Horario"]),
                Sala = new Sala() { ID = linha["salaID"], Nome = linha["nomeSala"], Numero = linha["Numero"] },
                Palestrante = new Palestrante() { ID = linha["palestranteID"], Nome = linha["nomePalestrante"], Bio = linha["Bio"], Twitter = linha["Twitter"], Foto = linha["Foto"] },
                Trilha = new Trilha() { ID = linha["trilhaID"], Nome = linha["nomeTrilha"] }
            };
            return tempPalestra;
        }
        public int Inserir(Palestras palestra)
        {
            const string strQuery = "insert into Palestras (Id,Nome,Titulo,Codigo,Descricao,Horario,Nivel,PalestranteID,SalaID,TrilhaID) values (@Id,@Nome,@Titulo,@Codigo,@Descricao,@Horario,@Nivel,@PalestranteID,@SalaID,@TrilhaID)";
            var parametros = SetaDadosdaPalestra(palestra);
            return contexto.ExecutaComando(strQuery, parametros);
        }
        public int Alterar(Palestras palestra)
        {
            const string strQuery = "UPDATE  Palestras set Titulo = @Titulo, Codigo = @Codigo, Descricao = @Descricao, Nivel = @Nivel, Horario = @Horario, PalestranteId = @PalestranteID, SalaID = @SalaID, TrilhaID = @TrilhaID  Where Id = @Id";
            var parametros = SetaDadosdaPalestra(palestra);
            return contexto.ExecutaComando(strQuery, parametros);
        }        
        public int Excluir(string id)
        {
            const string strQuery = "delete from Palestras Where ID = @ID";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",id}
            };
            return contexto.ExecutaComando(strQuery, parametros);
        }
        public Palestras ListarPorId(string id)
        {
            const string strQuery = "SELECT ID , Nome,Titulo,Codigo,Descricao,Nivel,Horario,PalestranteID,SalaID,TrilhaID FROM Palestras where ID = @ID";
            var parametros = new Dictionary<string, object>()
            {
                {"ID",id}
            };
            var linhas = contexto.ExecutaComandoComRetorno(strQuery, parametros);
            if (!linhas.Any())
                return new Palestras();
            var palestra = SetaDadosPalestrasPorId(linhas);
            return palestra;
        }
        private Palestras SetaDadosPalestrasPorId(List<Dictionary<string, string>> linhas)
        {
            var palestra = new Palestras
            {
                ID = linhas[0]["ID"],
                Nome = linhas[0]["Nome"],
                Titulo = linhas[0]["Titulo"],
                Codigo = linhas[0]["Codigo"],
                Descricao = linhas[0]["Descricao"],
                Nivel = linhas[0]["Nivel"],
                Horario = DateTime.Parse(linhas[0]["Horario"]),
                PalestranteID = linhas[0]["PalestranteID"],
                SalaID = linhas[0]["SalaID"],
                TrilhaID = linhas[0]["TrilhaID"]
            };
            return palestra;
        }
        private  Dictionary<string, object> SetaDadosdaPalestra(Palestras palestra)
        {
            var parametros = new Dictionary<string, object>()
            {
                {"Id",palestra.ID},
                {"Nome",palestra.Nome},
                {"Titulo",palestra.Titulo},
                {"Codigo",palestra.Codigo},
                {"Descricao",palestra.Descricao},
                {"Nivel",palestra.Nivel},
                {"Horario",palestra.Horario},
                {"PalestranteID",palestra.PalestranteID},
                {"SalaID",palestra.SalaID},
                {"TrilhaID",palestra.TrilhaID}

            };
            return parametros;
        }
        public List<Palestras> SalasComPalestras(string id)
        {
            var palestras = new List<Palestras>();
            var parametros = new Dictionary<string, object>()
            {
                {"ID",id}
            };
            var linhas = contexto.ExecutaComandoComRetorno(strQueryBase+" AND s.ID = @ID", parametros);

            foreach (var linha in linhas)
            {
                var tempPalestra = SetaPalestrasParaAAplicacao(linha);
                palestras.Add(tempPalestra);
            }
            return palestras;
        }
        public List<Palestras> PalestrantesComPalestras(string id)
        {
            var palestras = new List<Palestras>();
            var parametros = new Dictionary<string, object>()
            {
                {"ID",id}
            };
            var linhas = contexto.ExecutaComandoComRetorno(strQueryBase + " AND p.ID = @ID", parametros);

            foreach (var linha in linhas)
            {
                var tempPalestra = SetaPalestrasParaAAplicacao(linha);
                palestras.Add(tempPalestra);
            }
            return palestras;
        }
        public List<Palestras> TrilhasComPalestras(string id)
        {
            var palestras = new List<Palestras>();
            var parametros = new Dictionary<string, object>()
            {
                {"ID",id}
            };
            var linhas = contexto.ExecutaComandoComRetorno(strQueryBase + " AND t.ID = @ID", parametros);

            foreach (var linha in linhas)
            {
                var tempPalestra = SetaPalestrasParaAAplicacao(linha);
                palestras.Add(tempPalestra);
            }
            return palestras;
        }
    }
}