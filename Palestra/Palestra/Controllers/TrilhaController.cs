using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Palestra.Models;
using Palestra.Aplicacao;

namespace Palestra.Controllers
{
    [Authorize]
    public class TrilhaController : Controller
    {
        private readonly TrilhaAplicacao appTrilha;

        public TrilhaController()
        {
            appTrilha = new TrilhaAplicacao();
        }
      [AllowAnonymous]
        public ActionResult Index()
        {
            var listaTrilhas = appTrilha.Listar();
            return View(listaTrilhas);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Cadastrar()
        {
            return View(new Trilha());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Cadastrar(Trilha trilha)
        {
            if (ModelState.IsValid)
            {
                this.Flash("Trilha adicionada com sucesso");
                appTrilha.Inserir(trilha);
                return RedirectToAction("Index", "Trilha");
            }
            this.Flash("Preencha todos os dados",LoggerEnum.Warning);
            return View(trilha);
        }//cadastrar sala
        [Authorize(Roles = "Admin")]
        public ActionResult Editar(string id)
        {

            var trilha = appTrilha.ListarPorId(id);
            return View(trilha);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Editar(Trilha trilha)
        {
            if (ModelState.IsValid)
            {
                this.Flash("Trilha modificada com sucesso");
                appTrilha.Alterar(trilha);
                return RedirectToAction("Index", "Trilha");
            }
            this.Flash("Preencha todos os campos");
            return View(trilha);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            var trilha = appTrilha.ListarPorId(id);
            return View(trilha);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(Trilha trilha)
        {
            appTrilha.Excluir(trilha.ID);
            this.Flash("Trilha removida com sucesso",LoggerEnum.Success);
            return RedirectToAction("Index", "Trilha");
        }
    }
}