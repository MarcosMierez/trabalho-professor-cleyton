using Palestra.Aplicacao;
using Palestra.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Palestra.Controllers
{
    public class AcessoController : Controller
    {
        private readonly UsuarioAplicacao appUsuario;
        public AcessoController()
        {
            appUsuario = new UsuarioAplicacao();
        }
        public ActionResult Index()
        {
            var usuario = Seguranca.Usuario();
            return View(usuario);
        }
        public ActionResult Login(){
            if (User.Identity.IsAuthenticated)
            {
                this.Flash("Esta pagina é somente para admistradores", LoggerEnum.Error);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Login, string Senha)
        {
            var usuario = appUsuario.Logar(Login, Senha);

            if (!string.IsNullOrEmpty(usuario.Email))
            {
                Seguranca.GerearSessaoDeUsuario(usuario);
                this.Flash("Bem Vindo "+usuario.Nome);
                return RedirectToAction("Index", "Home");
            }
            this.Flash("Usuario ou senha Incorretos", LoggerEnum.Warning);
            return View();
        }
        public ActionResult SingOut()
        {
            Seguranca.DestruirSessaoDeUsuario();
            this.Flash("Usuario Deslogado", LoggerEnum.Info);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult MudarSenha()
        {
            return View();
        }
        public JsonResult verificaSenha(string senha)
        {
            var usuarioTemp = Seguranca.Usuario();
            
            return Json((string)appUsuario.checaSenha(usuarioTemp.ID,senha));
        }
        public JsonResult ConfirmarMudancadeSenha(string novaSenha)
        {

            return Json((string)appUsuario.mudaSenha(Seguranca.Usuario().ID,novaSenha));         
        }
    }
}