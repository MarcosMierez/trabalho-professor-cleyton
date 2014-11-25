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
                    Email = linha["Email"]
                };
                usuarios.Add(tempUsuario);
            }
            return usuarios;
        }
        public int Inserir(Usuario usuario)
        {
            const string strQuery = "insert into Usuario (Id,Nome,Senha,Email,Permissao) values (@Id,@Nome,@Senha,@Email,@Permissao)";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",usuario.ID},
                {"Nome",usuario.Nome},
                {"Senha",usuario.Senha},
                {"Email",usuario.Email},
                {"Permissao",carregaPermissoes(usuario.Permissao)}
            };
            return contexto.ExecutaComando(strQuery, parametros);
        }      
        public int Alterar(Usuario usuario)
        {
            const string strQuery = "UPDATE  Usuario set Nome = @Nome , Email = @Email,Permissao = @Permissao Where Id = @Id";
            var parametros = new Dictionary<string, object>()
            {
                {"Id",usuario.ID},
                {"Nome",usuario.Nome},
                {"Email",usuario.Email},
                {"Permissao",carregaPermissoes(usuario.Permissao)}
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
                Permissao = carregaPermissoes(linhas[0]["Permissao"])
            };
            return usuario;
        }
        public Usuario Logar(string Login, string Senha)
        {
            const string strQuery = "SELECT Id , Nome ,Email ,Permissao FROM Usuario where Email = @Email and Senha = @Senha";
            var parametros = new Dictionary<string, object>()
            {
                {"Email",Login},
                {"Senha",Senha}
            };
            var linhas = contexto.ExecutaComandoComRetorno(strQuery, parametros);

            if (!linhas.Any())
                return new Usuario();

            var tempPermissao = linhas[0]["Permissao"].Split(',').ToList();
            var usuario = new Usuario
            {
                ID = linhas[0]["Id"],
                Nome = linhas[0]["Nome"],
                Email = linhas[0]["Email"],
                Permissao = tempPermissao
            };
            return usuario;
        }

        public string checaSenha(string ID, string Senha)
        {
            const string strQuery = "select ID , Senha from Usuario Where ID = @ID and Senha =@Senha";
            var parametros = new Dictionary<string, object>()
            {
                {"ID",ID},
                {"Senha",Senha}
            };
            var linhas = contexto.ExecutaComandoComRetorno(strQuery, parametros);
            if (linhas.Count != 0)
            {
                string variavelSenha = linhas[0]["Senha"];
                string variavelId = linhas[0]["ID"];
                return "valida";
            }
            return "invalida";

        }
        public string mudaSenha(string id, string Novasenha)
        {
            const string strQuery = "UPDATE  Usuario set Senha= @Novasenha where ID =@ID";
            var parametros = new Dictionary<string, object>()
            {
                {"Novasenha",Novasenha},
                {"ID",id}
            };
            var x = contexto.ExecutaComando(strQuery, parametros);
            if (x == 1)
                return "valida";
                return "invalida";
        }
        private static string carregaPermissoes(List<string> permissoes)
        {
            if (!permissoes.Any())
            {
                return string.Empty;
            }
            var tempPermissao = "";
            foreach (var permissao in permissoes)
            {
                tempPermissao += permissao + ",";
            }
            return tempPermissao.Remove(tempPermissao.Length-1);
        }
        private static List<string> carregaPermissoes(string permissoes)
        {
            return permissoes.Split(',').ToList();
        }
    }
}