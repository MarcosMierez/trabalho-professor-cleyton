using System.Runtime.InteropServices;
using System.Web.Helpers;
using System.Web.Management;
using Palestra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Palestra.Aplicacao;

namespace Palestra.Controllers
{
    [Authorize]
    public class PalestController : Controller
    {
        private readonly PalestraAplicacao appPalestras;
        private readonly PalestranteAplicacao appPalestrantes;
        private readonly TrilhaAplicacao appTrilhas;
        private readonly SalaAplicacao appSalas;
        public PalestController()
        {
            appPalestras = new PalestraAplicacao();
            appPalestrantes = new PalestranteAplicacao();
            appTrilhas = new TrilhaAplicacao();
            appSalas = new SalaAplicacao();
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            var lista = appPalestras.ListarOtimizado();
            return View(lista);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Cadastrar()
        {    MontaSelectList(new Palestras());
            return View(new Palestras());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Cadastrar(Palestras palestra)
        {
            if (ModelState.IsValid)
            {
                appPalestras.Inserir(palestra);
                this.Flash("Palestra cadastrada com sucessso");
               return RedirectToAction("Index", "Palest");
            }
            this.Flash("Favor preenha todos os campos", LoggerEnum.Error);
            MontaSelectList(palestra);
            return View(palestra);
        }//cadastrar sala
        [Authorize(Roles = "Admin")]
        public ActionResult Editar(string id)
        {
            var palestraTemp = appPalestras.ListarPorId(id);
            MontaSelectList(palestraTemp);
            return View(palestraTemp);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Editar(Palestras palestras)
        {
            var palestra = appPalestras.Alterar(palestras);
            this.Flash("Palestra alterada com sucessso");
            return RedirectToAction("Index", "Palest");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            var tempPalestras = appPalestras.ListarPorId(id);
            return View(tempPalestras);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(Palestras palestras)
        {
            appPalestras.Excluir(palestras.ID);
            this.Flash("Palestra removida com sucessso");
            return RedirectToAction("Index", "Palest");
        }
        private void MontaSelectList(Palestras palestra)
        {
            var palestrantes = appPalestrantes.Listar();
            ViewBag.palestrantes = new SelectList(palestrantes, "ID", "Nome",palestra.PalestranteID);

            var salas = appSalas.Listar();
            ViewBag.salas = new SelectList(salas, "ID", "Nome",palestra.SalaID);

            var trilhas = appTrilhas.Listar();
            ViewBag.trilhas = new SelectList(trilhas, "ID", "Nome",palestra.TrilhaID);
        }
    }
}