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
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Login, string Senha)
        {
            var usuario = appUsuario.Logar(Login, Senha);

            if (!string.IsNullOrEmpty(usuario.Email))
            {
                Seguranca.GerearSessaoDeUsuario(usuario);
                this.Flash("Bem Vindo ");
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
    }
}