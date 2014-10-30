using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Palestra.Models;
using Palestra.Aplicacao;

namespace Palestra.Controllers
{
    public class TrilhaController : Controller
    {
        private readonly TrilhaAplicacao appTrilha;

       public TrilhaController()
        {
            appTrilha = new TrilhaAplicacao();
        }
        public ActionResult Index()
        {
            var listaTrilhas = appTrilha.Listar();
            return View(listaTrilhas);
        }
        public ActionResult Cadastrar()
        {
            return View(new Trilha());
        }
        [HttpPost]
        public ActionResult Cadastrar(Trilha trilha)
        {
            if (ModelState.IsValid)
            {
                appTrilha.Inserir(trilha);
               return RedirectToAction("Index", "Trilha");
            }
            return View(trilha);
        }//cadastrar sala
        public ActionResult Editar(string id)
        {

            var trilha = appTrilha.ListarPorId(id);
            return View(trilha);
        }
        [HttpPost]
        public ActionResult Editar(Trilha trilha)
        {
            if (ModelState.IsValid)
            {
                appTrilha.Alterar(trilha);
                return RedirectToAction("Index", "Trilha");
            }
            return View(trilha);
        }
        public ActionResult Delete(string id)
        {
            var trilha = appTrilha.ListarPorId(id);
            return View(trilha);
        }
        [HttpPost]
        public ActionResult Delete(Trilha trilha)
        {
            appTrilha.Excluir(trilha.ID);
            return RedirectToAction("Index","Trilha");
        }
    }
}