using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Palestra.Models;
using Palestra.Repositorio;

namespace Palestra.Aplicacao
{
    public class UsuarioAplicacao
    {
        private readonly Contexto contexto;

        public UsuarioAplicacao()
        {
            contexto = new Contexto();
        }
        public List<Usuario> Listar()
        {
            var usuarios = new List<Usuario>();
            const string strQuery = "SELECT ID , Nome, Email FROM Usuario";
            var linhas = contexto.ExecutaComandoComRetorno(strQuery, null);
            foreach (var linha in linhas)
            {
                var tempUsuario = new Usuario
                {
                    ID = linha["ID"],
                    Nome = linha["Nome"],
                    Email =linha["Email"]
                };
                usuarios.Add(tempUsuario);
            }
            return usuarios;
        }
        public int Inserir(Usuario usuario)
        {
            const string strQuery = "insert into Usuario (Id,Nome,Email,Permissao) values (@Id,@Nome,@Email,@Permissao)";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",usuario.ID},
                {"Nome",usuario.Nome},
                {"Email",usuario.Email},
                {"Permissao",usuario.Permissao}
            };
            return contexto.ExecutaComando(strQuery, parametros);
        }
        public int Alterar(Usuario usuario)
        {
            const string strQuery = "UPDATE  Usuario set Nome = @Nome , Email = @Email Where Id = @Id";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",usuario.ID},
                {"Nome",usuario.Nome},
                {"Numero",usuario.Email},
                {"Permissao",usuario.Permissao}
            };
            return contexto.ExecutaComando(strQuery, parametros);
        }
        public int Excluir(string id)
        {
            const string strQuery = "delete from Usuario Where ID = @ID";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",id}
            };
            return contexto.ExecutaComando(strQuery, parametros);
        }
        public Usuario ListarPorId(string id)
        {
            const string strQuery = "SELECT Id , Nome ,Email ,Permissao FROM Usuario where Id = @ID";
            var parametros = new Dictionary<string, object>()
            {
                {"ID",id}
            };
            var linhas = contexto.ExecutaComandoComRetorno(strQuery, parametros);
            
            if (!linhas.Any())
                return new Usuario();
            
            var usuario = new Usuario
            {
                ID = linhas[0]["Id"],
                Nome = linhas[0]["Nome"],
                Email = linhas[0]["Email"],
                Permissao =linhas[0]["Permissao"]
            };
            return usuario;
        }


}
}