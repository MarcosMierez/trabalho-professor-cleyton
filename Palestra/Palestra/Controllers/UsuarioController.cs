using Palestra.Aplicacao;
using Palestra.Helpers;
using Palestra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Palestra.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioAplicacao appUsuario;
        
        public UsuarioController()
        {
            appUsuario = new UsuarioAplicacao();
        }
        public ActionResult Index()
        {
            var listaUsers = appUsuario.Listar();
            return View(listaUsers);
        }
        public ActionResult Cadastrar()
        {
            return View(new Usuario());
        }
        [HttpPost]
        public ActionResult Cadastrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                appUsuario.Inserir(usuario);
                this.Flash("Salvo com sucesso");
               return RedirectToAction("Index", "Usuario");
            }
            this.Flash("Favor preenha todos os campos", LoggerEnum.Error);
            return View(usuario);
        }
        public ActionResult Editar(string id)
        {
            var usuario = appUsuario.ListarPorId(id);
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Editar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                appUsuario.Alterar(usuario);
                this.Flash("Usuario alterado com sucesso");
                return RedirectToAction("Index", "Usuario");
            }
            
            return View(usuario);
        }
        public ActionResult Delete(string id)
        {
            var usuario = appUsuario.ListarPorId(id);
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Delete(Usuario usuario)
        {
            appUsuario.Excluir(usuario.ID);
            this.Flash("usuario removido com sucesso");

            return RedirectToAction("Index","Usuario");
        }

    }
}