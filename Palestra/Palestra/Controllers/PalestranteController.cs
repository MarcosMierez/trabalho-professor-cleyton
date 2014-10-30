using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Palestra.Models;
using Palestra.Aplicacao;

namespace Palestra.Controllers
{
    public class PalestranteController : Controller
    {
        private readonly PalestranteAplicacao appPalestrante;
        public PalestranteController()
        {
            appPalestrante = new PalestranteAplicacao();
        }

        public ActionResult Index()
        {

            var listaPalestrante = appPalestrante.Listar();
            return View(listaPalestrante);
        }
        public ActionResult Cadastrar()
        {
            return View(new Palestrante());
        }
        [HttpPost]
        public ActionResult Cadastrar(Palestrante palestrante)
        {
            if (ModelState.IsValid)
            {
                appPalestrante.Inserir(palestrante);
               return RedirectToAction("Index", "Palestrante");
            }
            return View(palestrante);
        }//cadastrar sala
        public ActionResult Editar(string id)
        {
            var palestrante = appPalestrante.ListarPorId(id);
            return View(palestrante);
        }
        [HttpPost]
        public ActionResult Editar(Palestrante  palestrante)
        {
            if (ModelState.IsValid)
            {
                appPalestrante.Alterar(palestrante);
                return RedirectToAction("Index", "Palestrante");
            }
            return View(palestrante);
        }
        public ActionResult Delete(string id)
        {
            var palestrante = appPalestrante.ListarPorId(id);
            return View(palestrante);
        }
        [HttpPost]
        public ActionResult Delete(Palestrante palestrante)
        {
            appPalestrante.Excluir(palestrante.ID);
            return RedirectToAction("Index", "Palestrante");
        }

    }
}