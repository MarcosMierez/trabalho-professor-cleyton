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
        public ActionResult Index()
        {
            var lista = appPalestras.ListarOtimizado();
            return View(lista);
        }
        public ActionResult Cadastrar()
        {
            var palestrantes = appPalestrantes.Listar();
            ViewBag.palestrantes = new SelectList(palestrantes, "ID", "Nome");

            var salas = appSalas.Listar();
            ViewBag.salas = new SelectList(salas, "ID", "Nome");

           var trilhas = appTrilhas.Listar();
           ViewBag.trilhas = new SelectList(trilhas, "ID", "Nome");
            return View(new Palestras());
        }
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
            var palestrantes = appPalestrantes.Listar();
            ViewBag.palestrantes = new SelectList(palestrantes, "ID", "Nome");

            var salas = appSalas.Listar();
            ViewBag.salas = new SelectList(salas, "ID", "Nome");

            var trilhas = appTrilhas.Listar();
            ViewBag.trilhas = new SelectList(trilhas, "ID", "Nome");
            return View(palestra);
        }//cadastrar sala
        public ActionResult Editar(string id)
        {
            var palestrantes = appPalestrantes.Listar();
            ViewBag.palestrantes = new SelectList(palestrantes, "ID", "Nome",id);

            var salas = appSalas.Listar();
            ViewBag.salas = new SelectList(salas, "ID", "Nome",id);

            var trilhas = appTrilhas.Listar();
            ViewBag.trilhas = new SelectList(trilhas, "ID", "Nome",id);
            var palestraTemp = appPalestras.ListarPorId(id);

            return View(palestraTemp);
        }
        [HttpPost]
        public ActionResult Editar(Palestras palestras)
        {
            var palestra = appPalestras.Alterar(palestras);
            this.Flash("Palestra alterada com sucessso");
            return RedirectToAction("Index", "Palest");
        }
        public ActionResult Delete(string id)
        {
            var tempPalestras = appPalestras.ListarPorId(id);
            return View(tempPalestras);
        }   
        [HttpPost]
        public ActionResult Delete(Palestras palestras)
        {
            appPalestras.Excluir(palestras.ID);
            this.Flash("Palestra removida com sucessso");
            return RedirectToAction("Index", "Palest");
        }
    }
}