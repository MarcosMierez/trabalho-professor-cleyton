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
    public class SalaController : Controller
    {
        private readonly SalaAplicacao appSala;

        public SalaController()
        {
            appSala = new SalaAplicacao();
        }
        public ActionResult Index()
        {
            var listaSalas = appSala.Listar();
            return View(listaSalas);
        }
        public ActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Cadastrar(Sala sala)
        {
            if (ModelState.IsValid)
            {
                appSala.Inserir(sala);
                RedirectToAction("Index", "Sala");
            }
            return View();
        }//cadastrar sala
        public ActionResult Editar(string id)
        {
            var sala = appSala.ListarPorId(id);
            if (sala == null)
                return HttpNotFound();
            return View(sala);
        }
        [HttpPost]
        public ActionResult Editar(Sala sala)
        {
            if (ModelState.IsValid)
            {
                appSala.Alterar(sala);
            }
            return View();
        }

        public ActionResult Detalhe(string id)
        {
            appSala.ListarPorId(id);
            return View();
        }

        public ActionResult Delete(string id)
        {
            appSala.Excluir(id);
            return View();
        }

        public JsonResult Validador(string nome)
        {
            return Json("Funfooo!!!");
        }
    }
}