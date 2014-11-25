using System.Runtime.InteropServices;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Web.Management;
using Palestra.Aplicacao;
using Palestra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Palestra.Controllers
{
    [Authorize]
    public class SalaController : Controller
    {
        private readonly SalaAplicacao appSala;

        public SalaController()
        {
            appSala = new SalaAplicacao();
        }
       [AllowAnonymous]
        public ActionResult Index()
        {
            var listaSalas = appSala.Listar();
            return View(listaSalas);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Cadastrar()
        {
            return View(new Sala());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Cadastrar(Sala sala)
        {
            if (ModelState.IsValid)
            {
                appSala.Inserir(sala);
                this.Flash("Salvo com sucesso");
                return RedirectToAction("Index", "Sala");
            }
            this.Flash("Favor preenha todos os campos", LoggerEnum.Error);
            return View(sala);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Editar(string id)
        {
            var sala = appSala.ListarPorId(id);
            return View(sala);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Editar(Sala sala)
        {
            if (ModelState.IsValid)
            {
                appSala.Alterar(sala);
                this.Flash("Sala alterada com sucesso");
                return RedirectToAction("Index", "Sala");
            }

            return View(sala);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            var sala = appSala.ListarPorId(id);
            return View(sala);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(Sala sala)
        {
            appSala.Excluir(sala.ID);
            this.Flash("Sala removida com sucesso");

            return RedirectToAction("Index", "Sala");
        }

    }
}